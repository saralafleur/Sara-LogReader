using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Sara.Common.Extension;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Service.FileServiceNS;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service
{
    public static class NetworkService
    {



        /// <summary>
        /// Using the message.Source the system will try to locate the other endpoint
        /// Returns the first match
        /// </summary>
        public static void FindEndPoint(NetworkMessageInfo message)
        {
            var found = false;
            foreach (var file in XmlDal.CacheModel.Files)
            {
                // Loop through each defined Map to find a match
                foreach (var map in XmlDal.DataModel.NetworkMappingModel.EnabledNetworkMaps)
                {
                    // Only look at TargetTypes that are MappingDataType.FileValue
                    foreach (var mapCriteria in map.Criteria.Where(mapCriteria => mapCriteria.TargetType == MappingDataType.FileValue))
                    {
                        // Loop through all of the FileValue(s) in the FileData
                        foreach (var fileValue in file.FileValues)
                        {
                            // Loop through all the SourceValue(s) in the Source
                            foreach (var sourceValue in message.Source.Values)
                            {
                                if (fileValue.Name == mapCriteria.TargetName &&
                                    fileValue.Value == sourceValue.Value)
                                {
                                    found = true;
                                    break;
                                }
                            }
                            if (found) break;
                        }

                        if (found) break;
                    }
                }
            }
        }

        private static long _checkingCount = 0;
        private static readonly object CheckingLockObject = new object();
        private static readonly object BuildingNetworkMessagesLockObject = new object();
        /// <summary>
        /// Common method used to check if the Network Messages are current, if not they will be updated and a StatusUpdate
        /// will occur.
        /// <remarks>
        /// This method contains Locks and will block, so make sure you call it from a Thread and not the Main UI Thread!
        /// </remarks>
        /// </summary>
        public static void CheckNetworkMessages(FileData file, Action<IStatusModel> statusUpdate, string source)
        {
            var status = $"Building {source} Network Messages";
            var statusDetail = $"{file.SourceType} - {file.Count} lines";
            if (statusUpdate != null)
            {
                lock (CheckingLockObject)
                {
                    if (Interlocked.Read(ref _checkingCount) > 0)
                        statusUpdate(StatusModel.Update(status, statusDetail + Environment.NewLine + "Another process is building the network messages..."));

                    if (file.IsCached_Lazy_NetworkMessages) return;
                    Interlocked.Increment(ref _checkingCount);
                }
            }

            try
            {
                // Only allow 1 Thread to build the same file at a time.
                lock (BuildingNetworkMessagesLockObject)
                {
                    try
                    {
                        if (statusUpdate != null)
                        {
                            statusUpdate(StatusModel.StartBackground);
                            statusUpdate(StatusModel.StartStopWatch);
                            statusUpdate(StatusModel.Update(status, statusDetail));
                        }
                        FileService.BuildNetworkMessages(file);
                    }
                    finally
                    {
                        if (statusUpdate != null)
                        {
                            statusUpdate(StatusModel.EndBackground);
                            statusUpdate(StatusModel.Completed);
                        }
                    }
                }
            }
            finally
            {
                Interlocked.Decrement(ref _checkingCount);
            }
        }


        /// <summary>
        /// Returns a list of Files that are within the date range of the FileData Start & End and contains the FileData Ip
        /// </summary>
        public static IEnumerable<FileData> GetNetworkFilesByNode(NodeBase node)
        {
            if (node.SourceType == Keywords.NO_FILE)
                return new List<FileData>();
            
            if (node.SourceStart == DateTime.MinValue || node.SourceEnd == DateTime.MinValue)
                return new List<FileData>();

            var source = new DateTimeExt.DateTimeRange
            {
                Start = node.SourceStart,
                End = node.SourceEnd
            };

            try
            {
                return (from file in XmlDal.CacheModel.Files
                        let target = new DateTimeExt.DateTimeRange
                        {
                            Start = file.Start,
                            End = file.End
                        }
                        where file.Ip == node.Ip && source.Intersects(target)
                        select file).ToList();
            }
            catch
            {
                return new List<FileData>();
                // Do Nothing
            }
        }
    }
}
