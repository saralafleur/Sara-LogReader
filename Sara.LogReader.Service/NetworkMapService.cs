using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Sara.Common.DateTimeNS;
using Sara.Common.Extension;
using Sara.Logging;
using Sara.LogReader.Common;
using Sara.LogReader.Model;
using Sara.LogReader.Model.FileNS;
using Sara.LogReader.Model.NetworkMapping;
using Sara.LogReader.Model.Property;
using Sara.WinForm.CRUD;
using Sara.WinForm.Notification;

namespace Sara.LogReader.Service
{
    public static class NetworkMapService
    {
        #region CRUD
        public static NetworkMapCacheData GetModel()
        {
            return XmlDal.DataModel.NetworkMappingModel;
        }
        public static NetworkMapModel Add()
        {
            var model = new NetworkMap();
            return Add(model);
        }
        public static NetworkMapModel Add(NetworkMap model)
        {
            return new NetworkMapModel
            {
                Mode = InputMode.Add,
                SaveEvent = Save,
                Item = model
            };
        }
        public static NetworkMapModel Edit(int networkMapId)
        {
            var model = XmlDal.DataModel.GetNetworkMap(networkMapId);
            return new NetworkMapModel
            {
                Mode = InputMode.Edit,
                SaveEvent = Save,
                Item = model
            };
        }
        public static NetworkMapModel Delete(int networkMapId)
        {
            var model = new NetworkMapModel
            {
                Mode = InputMode.Delete,
                Item = XmlDal.DataModel.GetNetworkMap(networkMapId)
            };
            Save(model);
            return model;
        }
        public static void Save(NetworkMapModel model)
        {
            Log.WriteEnter(typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
            var dataModel = XmlDal.DataModel;

            switch (model.Mode)
            {
                case InputMode.Add:
                    model.Item.NetworkMapId = XmlDal.DataModel.GetUniqueNetworkMapId();
                    dataModel.NetworkMappingModel.NetworkMaps.Add(model.Item);
                    break;
                case InputMode.Edit:
                    var item = dataModel.GetNetworkMap(model.Item.NetworkMapId);
                    item.Copy(model.Item);
                    break;
                case InputMode.Delete:
                    dataModel.NetworkMappingModel.NetworkMaps.Remove(model.Item);
                    break;
            }

            XmlDal.Save();
            XmlDal.DataModel.NetworkMapCacheDataController.Invalidate();
        }
        #endregion CRUD

        /// <summary>
        /// Returns a list of FileData that successfully match the Network Maps
        /// Note: This is only performing a FileData Value(s) match and does not look at EventPattern Value(s)
        /// </summary>
        public static List<NetworkMapFile> GetNetworkMapFiles(LineArgs model)
        {
            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, true);
            try
            {
                var sourceFile = XmlDal.CacheModel.GetFile(model.Path);
                if (sourceFile == null)
                {
                    Log.WriteError("sourceFile should never be null!",typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
                    return new List<NetworkMapFile>();
                }

                var source = sourceFile.GetNetworkMessage(model.iLine);
                if (source == null)
                {
                    Log.WriteError("source should never be null!",typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
                    return new List<NetworkMapFile>();
                }

                return GetNetworkMapFiles(model.Line, source);
            }
            finally
            {
                stopwatch.Stop(100);
            }
        }

        /// <summary>
        /// This method was designed to be used by BuildFileNetworkMessagesLoop directly.
        /// The FileData Network Messages do not exists yet, thus we need to pass in the source directly. - Sara
        /// </summary>
        public static List<NetworkMapFile> GetNetworkMapFiles(string line, NetworkMessageInfo source)
        {
            var files = new List<NetworkMapFile>();

            foreach (var networkMap in XmlDal.DataModel.NetworkMappingModel.EnabledNetworkMaps)
            {
                if (!RegularExpression.HasMatch(line, networkMap.RegularExpression))
                    continue;

                var criteriaFiltered =
                    networkMap.Criteria.Where(
                        n =>
                            (n.TargetType == MappingDataType.FileValue || n.SourceName == Keywords.DATETIME) &&
                            n.Enabled).ToList();

                foreach (var file in XmlDal.CacheModel.Files)
                {
                    bool? found = null;

                    foreach (var criteria in criteriaFiltered)
                    {
                        found = IsFileMatch(criteria, source, file);
                        //found = IsMatch(criteria, source, null, file.Path).IsMatch;

                        if (found != null && (bool)!found) break;
                    }

                    if (found != null && (bool)found)
                    {
                        var foundFile = false;
                        foreach (var item in files)
                        {
                            if (item.File.Path != file.Path) continue;

                            item.NetworkMapId.Add(networkMap.NetworkMapId);
                            foundFile = true;
                            break;
                        }

                        if (!foundFile)
                        {
                            files.Add(new NetworkMapFile
                            {
                                File = file,
                                NetworkMapId = new List<int> { networkMap.NetworkMapId }
                            });
                        }
                    }
                }
            }
            return files;
        }

        private static bool? IsFileMatch(MapCriteria criteria, NetworkMessageInfo source, FileData file)
        {
            string sourceValue;

            switch (criteria.SourceType)
            {
                case MappingDataType.FileValue:
                    sourceValue = source.Source.FindFileValue(criteria.SourceName);
                    break;
                case MappingDataType.EventValue:
                    sourceValue = source.Source.FindEventValue(criteria.SourceName);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (sourceValue == null)
                return null;

            switch (criteria.SourceName.ToUpper())
            {
                // Condition DateTime
                case Keywords.DATETIME_UPPER:
                    DateTime sourceDateTime;
                    if (DateTimeExt.TryParseWithTimeZoneRemoval(sourceValue, out sourceDateTime))
                        return sourceDateTime >= file.Start && sourceDateTime <= file.End;
                    return false;
                default:
                    switch (criteria.Operator)
                    {
                        case Keywords.EQUAL:
                            return sourceValue == file.FindFileValue(criteria.TargetName);
                        case Keywords.NOT_EQUAL:
                            return sourceValue != file.FindFileValue(criteria.TargetName);
                    }
                    break;
            }
            return null;
        }

        public static CriteriaMatchModel GetCriteriaMatches(int networkMapId, LineArgs source, LineArgs target, bool noNetworkMessages)
        {
            var matches = new List<CriteriaMatch>();

            var map = XmlDal.DataModel.GetNetworkMap(networkMapId);

            var sourceMessage = XmlDal.CacheModel.GetFile(source.Path).GetNetworkMessage(source.iLine);
            if (sourceMessage == null)
            {
                Log.WriteError("sourceMessage should never be null!!!!",typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
                return new CriteriaMatchModel();
            }
            var targetMessage = XmlDal.CacheModel.GetFile(target.Path).GetNetworkMessage(target.iLine);

            foreach (var criteria in map.Criteria)
            {
                var match = IsMatch(criteria, sourceMessage, targetMessage, target.Path);

                if (criteria.UseSourceValue)
                    match.SourceValue = $"=={match.CriteriaSourceValue} [{match.SourceValue}]";
                if (criteria.UseTargetValue)
                    match.TargetValue = $"=={match.CriteriaTargetValue} [{match.TargetValue}]";
                if (criteria.TimeConditionMs != null)
                    match.TargetValue = $"{match.TargetValue} tc:{criteria.TimeConditionMs}";

                matches.Add(new CriteriaMatch
                {
                    Enabled = criteria.Enabled,
                    IsMatch = match.IsMatch,
                    Operator = criteria.Operator,
                    SourceName = criteria.SourceName,
                    SourceType = criteria.SourceType.ToString(),
                    SourceValue = match.SourceValue,
                    TargetName = criteria.TargetName,
                    TargetType = criteria.TargetType.ToString(),
                    TargetValue = match.TargetValue
                });
            }

            LogPropertyBaseModel targetProperty = null;
            if (!noNetworkMessages)
                targetProperty = PropertyService.GetProperty(target);
            var sourceProperty = PropertyService.GetProperty(source);
            var dateTimeDifference = CalculateDateTimeDifference(targetProperty, sourceProperty);

            return new CriteriaMatchModel
            {
                DateTimeDifference = dateTimeDifference,
                Matches = matches
            };
        }

        /// <summary>
        /// Returns the difference between the target and source using the property "DATETIME"
        /// Returns null if both target and source do not have a "DATETIME" property
        /// </summary>
        private static TimeSpan? CalculateDateTimeDifference(DynamicObjectType targetProperty, DynamicObjectType sourceProperty)
        {
            if (targetProperty == null)
                return null;

            var firstOrDefault = sourceProperty.Properties.FirstOrDefault(prop => prop.Name == Keywords.DATETIME);
            if (firstOrDefault != null)
            {
                var sourceDateTimeString =
                    firstOrDefault.Value.ToString();
                DateTime sourceDateTime;
                if (!DateTimeExt.TryParseWithTimeZoneRemoval(sourceDateTimeString, out sourceDateTime))
                    return null;

                var dynamicProperty = targetProperty.Properties.FirstOrDefault(prop => prop.Name == Keywords.DATETIME);
                if (dynamicProperty != null)
                {
                    var targetDateTimeString =
                        dynamicProperty.Value.ToString();
                    DateTime targetDateTime;
                    if (!DateTimeExt.TryParseWithTimeZoneRemoval(targetDateTimeString, out targetDateTime))
                        return null;

                    return sourceDateTime - targetDateTime;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the properties for the current item
        /// </summary>
        public static List<SimpleProperty> GetCurrentLineValues(LineArgs item)
        {
            var property = PropertyService.GetProperty(item);
            var result = property.Properties.Select(prop => new SimpleProperty
            {
                Name = prop.Name,
                Value = (string)prop.Value ?? "",
                Type = "EventPattern"
            }).ToList();

            result = result.OrderBy(n => n.Name).ToList();

            var file = XmlDal.CacheModel.GetFile(item.Path);
            var result2 = file.FileValues.Where(n => n.FileInfo).Select(fileInfo => new SimpleProperty
            {
                Name = fileInfo.Name,
                Value = fileInfo.Value ?? "",
                Type = "FileData"
            }).ToList();

            result2 = result2.OrderBy(n => n.Name).ToList();
            result.AddRange(result2);

            return result;
        }

        /// <summary>
        /// Returns a list of EventPattern and FileData values
        /// </summary>
        public static List<SimpleProperty> GetPropertyValues(LineArgs model)
        {
            var result = new List<SimpleProperty>();
            var file = XmlDal.CacheModel.GetFile(model.Path);

            var unload = false;
            try
            {
                if (!file.IsLoaded)
                {
                    file.Load(true);
                    unload = true;
                }

                var property = PropertyService.GetProperty(model);

                result.AddRange(property.Properties.Select(prop => new SimpleProperty
                {
                    Name = prop.Name,
                    Value = (string)prop.Value ?? "",
                    Type = "EventPattern"
                }));

                result = result.OrderBy(n => n.Name).ToList();
                var result2 = file.FileValues.Where(n => n.FileInfo).Select(item => new SimpleProperty
                {
                    Name = item.Name,
                    Value = item.Value ?? "",
                    Type = "FileData"
                }).ToList();

                result2 = result2.OrderBy(n => n.Name).ToList();
                result.AddRange(result2);

                return result;
            }
            finally
            {
                if (unload)
                    file.UnLoad();
            }
        }

        private class GetNetworkMessagesArgs
        {
            public LineArgs Source { get; set; }
            public Action<NetworkTargets> Callback { get; set; }
        }

        /// <summary>
        /// Using the Callback, returns a list of Target NetworkMessages based on the Source LineFS
        /// Runs on a background Thread
        /// </summary>
        public static void GetNetworkMessagesBySourceLine(LineArgs source, Action<NetworkTargets> callback)
        {
            ThreadPool.QueueUserWorkItem(GetNetworkMessagesBySourceLineAsync, new GetNetworkMessagesArgs { Source = source, Callback = callback });
        }

        private static void GetNetworkMessagesBySourceLineAsync(object state)
        {
            var args = state as GetNetworkMessagesArgs;
            if (args == null)
            {
                Log.WriteError("state must be of type GetNetworkMessagesArgs!",typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
                return;
            }
            var result = GetNetworkMessagesBySourceLine(args.Source);
            args.Callback(result);
        }

        /// <summary>
        /// Takes the source and finds the matching Netork Messages
        /// If the Source FileData has not cached it's Network Messages, then this method will Cache them
        /// If the Target FileData's have not cached their Network Messages, then this method will cache them
        /// If the Source Network Message has not cached it's Target's then this method will cache them
        /// Once all the cache items are out of the way, the system will call the Callback with the Source Network Message Target's 
        /// </summary>
        /// <remarks>
        /// This method contains Locks and will block.  Make sure you do not call this on the Main UI Thread!
        /// </remarks>
        public static NetworkTargets GetNetworkMessagesBySourceLine(LineArgs source)
        {

            var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name, true);
            try
            {
                var sourceFile = XmlDal.CacheModel.GetFile(source.Path);


                NetworkService.CheckNetworkMessages(sourceFile, source.StatusUpdate, "Source");

                // Load the Source Network Message
                var sourceMessage = sourceFile.GetNetworkMessage(source.iLine);
                if (sourceMessage == null)
                {
                    Log.Write("netSource should never be null!",typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
                    return new NetworkTargets();
                }

                lock (sourceMessage.InternalTargets)
                {
                    // Make sure the Source Network Message Target's are cached
                    if (!sourceMessage.IsCached_Targets)
                    {
                        sourceMessage.InternalTargets.Clear();
                        var targetFiles = GetNetworkMapFiles(source);
                        foreach (var map in targetFiles)
                        {
                            NetworkService.CheckNetworkMessages(map.File, source.StatusUpdate, "Target");

                            // There is no need to show more then 50 Network Messages, since there should only be 1!
                            if (sourceMessage.InternalTargets.Count >= 50) break;

                            var result = GetNetworkMessageByTargetFile(source, sourceMessage, map.File);
                            sourceMessage.InternalTargets.AddRange(result.Select(arg => new SourceLocator
                            {
                                Path = arg.Item.Source.FilePath,
                                iLine = arg.Item.Source.iLine,
                                NetworkMapId = arg.NetworkMapId
                            }));
                        }
                        sourceMessage.HasTargetFiles = targetFiles.Count > 0;
                        sourceMessage.IsCached_Targets = true;
                    }
                }
                return new NetworkTargets { HasTargetFiles = sourceMessage.HasTargetFiles, Targets = sourceMessage.Targets };
            }
            finally
            {
                stopwatch.Stop(200);
            }
        }

        /// <summary>
        /// Returns a list of NetworkMessageInfoModels that map to the Source NetworkMessageInfo for the given Target FileData
        /// </summary>
        public static IEnumerable<NetworkMessageInfoModel> GetNetworkMessageByTargetFile(LineArgs sourceArgs, NetworkMessageInfo sourceMsg, FileData targetFile)
        {
            try
            {
                sourceArgs.StatusUpdate(StatusModel.StartStopWatch);
                sourceArgs.StatusUpdate(StatusModel.Update("Linking Network Messages", ""));

                lock (targetFile.Network)
                {
                    var stopwatch = new Stopwatch(MethodBase.GetCurrentMethod().Name);
                    try
                    {
                        var result = new List<NetworkMessageInfoModel>();
                        foreach (var map in GetNetworkMapsByLine(sourceArgs.Line).OrderBy(n => n.Priority))
                        {
                            if (result.Count > 0 && map.OnlyUseFallThrough)
                                continue;

                            // If there is NO EventPattern Value(s) criteria, then add all Network Messages for the given FileData
                            if (!map.Criteria.Exists(n => n.TargetType == MappingDataType.EventValue && n.Enabled))
                            {
                                foreach (var message in targetFile.Network.NetworkMessages)
                                {
                                    AddMessage(result, message, map);
                                }
                            }
                            else
                                foreach (var targetMsg in targetFile.Network.NetworkMessages)
                                {
                                    bool? success = null;

                                    foreach (var criteria in map.Criteria.Where(n => n.TargetType == MappingDataType.EventValue && n.Enabled))
                                    {
                                        success = IsMatch(criteria, sourceMsg, targetMsg, targetFile.Path).IsMatch;
                                        if (success != null && (bool)!success)
                                            break;
                                    }
                                    if (success != null && (bool)success)
                                    {
                                        AddMessage(result, targetMsg, map);
                                    }
                                }
                        }

                        return result;
                    }
                    finally
                    {
                        stopwatch.Stop(500);
                    }
                }
            }
            finally
            {
                sourceArgs.StatusUpdate(StatusModel.Completed);
            }
        }

        public class CriteriaMatchResult
        {
            public bool? IsMatch { get; set; }
            public string SourceValue { get; set; }
            public string TargetValue { get; set; }
            public string CriteriaSourceValue { get; set; }
            public string CriteriaTargetValue { get; set; }
        }

        /// <summary>
        /// Returns True or False depending on if the Source and Target match based on the Criteria.
        /// If the Source or Target are missing fields required by the Criteria then returns NULL
        /// </summary>
        public static CriteriaMatchResult IsMatch(MapCriteria criteria, NetworkMessageInfo source, NetworkMessageInfo target, string targetPath)
        {
            string sourceValue;
            string targetValue;
            switch (criteria.SourceName)
            {
                case Keywords.DATETIME:

                    DateTime sourceDateTime;
                    if (!DateTimeExt.TryParseWithTimeZoneRemoval(source.Source.FindEventValue(Keywords.DATETIME), out sourceDateTime))
                        return new CriteriaMatchResult { IsMatch = false, SourceValue = "", TargetValue = "" };

                    var start = sourceDateTime.AddMilliseconds(criteria.TimeConditionMs.GetValueOrDefault() * -1);
                    var end = sourceDateTime.AddMilliseconds(criteria.TimeConditionMs.GetValueOrDefault());

                    DateTime targetDateTime;
                    if (!DateTimeExt.TryParseWithTimeZoneRemoval(GetTargetValue(target, targetPath, Keywords.DATETIME), out targetDateTime))
                        return new CriteriaMatchResult { IsMatch = false, SourceValue = sourceDateTime.ToString(Keywords.DATETIMEFORMAT), TargetValue = "" };

                    return new CriteriaMatchResult
                    {
                        IsMatch = (targetDateTime >= start && targetDateTime <= end),
                        SourceValue = sourceDateTime.ToString(Keywords.DATETIMEFORMAT),
                        TargetValue = targetDateTime.ToString(Keywords.DATETIMEFORMAT)
                    };
                default:
                    switch (criteria.SourceType)
                    {
                        case MappingDataType.FileValue:
                            sourceValue =
                                XmlDal.CacheModel.GetFile(source.Source.FilePath).FindFileValue(criteria.SourceName);
                            break;
                        case MappingDataType.EventValue:
                            sourceValue = source.Source.FindEventValue(criteria.SourceName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    switch (criteria.TargetType)
                    {
                        case MappingDataType.FileValue:
                            targetValue =
                                XmlDal.CacheModel.GetFile(targetPath).FindFileValue(criteria.TargetName);
                            break;
                        case MappingDataType.EventValue:
                            targetValue = GetTargetValue(target, targetPath, criteria.TargetName);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    bool? isMatch = null;
                    if (criteria.UseSourceValue && criteria.UseTargetValue)
                    {
                        switch (criteria.Operator)
                        {
                            case Keywords.EQUAL:
                                isMatch = (String.Equals(criteria.SourceValue, sourceValue, StringComparison.CurrentCultureIgnoreCase)) &&
                                          (String.Equals(criteria.TargetValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                            case Keywords.NOT_EQUAL:
                                isMatch = (!String.Equals(criteria.SourceValue, sourceValue, StringComparison.CurrentCultureIgnoreCase)) &&
                                          (String.Equals(criteria.TargetValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                        }
                    }
                    else if (criteria.UseSourceValue && !criteria.UseTargetValue)
                    {
                        switch (criteria.Operator)
                        {
                            case Keywords.EQUAL:
                                isMatch = (String.Equals(criteria.SourceValue, sourceValue, StringComparison.CurrentCultureIgnoreCase)) &&
                                          (String.Equals(sourceValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                            case Keywords.NOT_EQUAL:
                                isMatch = (!String.Equals(criteria.SourceValue, sourceValue, StringComparison.CurrentCultureIgnoreCase)) &&
                                          (String.Equals(sourceValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                        }
                    }
                    else if (!criteria.UseSourceValue && criteria.UseTargetValue)
                    {
                        switch (criteria.Operator)
                        {
                            case Keywords.EQUAL:
                                isMatch = (String.Equals(criteria.TargetValue, targetValue, StringComparison.CurrentCultureIgnoreCase)) &&
                                          (String.Equals(sourceValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                            case Keywords.NOT_EQUAL:
                                isMatch = (!String.Equals(criteria.TargetValue, targetValue, StringComparison.CurrentCultureIgnoreCase)) &&
                                          (String.Equals(sourceValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                        }
                    }
                    else if (!criteria.UseSourceValue && !criteria.UseTargetValue)
                    {
                        switch (criteria.Operator)
                        {
                            case Keywords.EQUAL:
                                isMatch = (String.Equals(sourceValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                            case Keywords.NOT_EQUAL:
                                isMatch = (!String.Equals(sourceValue, targetValue, StringComparison.CurrentCultureIgnoreCase));
                                break;
                        }
                    }

                    if (isMatch != null)
                    {
                        return new CriteriaMatchResult
                        {
                            IsMatch = isMatch.Value,
                            SourceValue = sourceValue,
                            TargetValue = targetValue,
                            CriteriaSourceValue = criteria.SourceValue,
                            CriteriaTargetValue = criteria.TargetValue
                        };
                    }

                    break;
            }
            return new CriteriaMatchResult
            {
                IsMatch = false,
                SourceValue = sourceValue,
                TargetValue = targetValue
            };
        }

        /// <summary>
        /// Returns a PatterValue or a FileValue depending on if the target is null or not
        /// </summary>
        private static string GetTargetValue(NetworkMessageInfo target, string targetPath, string name)
        {
            if (target != null)
                return target.Source.FindEventValue(name);

            var targetFile = XmlDal.CacheModel.GetFile(targetPath);
            if (targetFile != null) return targetFile.FindFileValue(name);

            Log.WriteError("Target FileData should not be null!",typeof(NetworkMapService).FullName, MethodBase.GetCurrentMethod().Name);
            return "<ERROR>";
        }

        /// <summary>
        /// Adds a Network Message.  If the Network Message already exists then NetworkMapId is set to null.
        /// </summary>
        private static void AddMessage(ICollection<NetworkMessageInfoModel> result, NetworkMessageInfo message, NetworkMap map)
        {
            var match = result.FirstOrDefault(n => n.Item.Duplicate(message));
            if (match != null)
            {
                if (match.NetworkMapId != map.NetworkMapId)
                    match.NetworkMapId = null;
            }
            else
                result.Add(new NetworkMessageInfoModel
                {
                    Item = message,
                    NetworkMapId = map.NetworkMapId
                });
        }

        /// <summary>
        /// Returns a list of Network Maps that apply to the sourceLine
        /// </summary>
        private static IEnumerable<NetworkMap> GetNetworkMapsByLine(string sourceLine)
        {
            return XmlDal.DataModel.NetworkMappingModel.EnabledNetworkMaps.Where(networkMap => RegularExpression.HasMatch(sourceLine, networkMap.RegularExpression)).ToList();
        }

        public static void Copy(int networkMapId)
        {
            var copy = XmlDal.DataModel.GetNetworkMap(networkMapId).Clone();
            XmlDal.DataModel.NetworkMappingModel.NetworkMaps.Add(copy);
            XmlDal.Save();
        }

        public static List<CriteriaMatch> GetRecommendedMatches(LineArgs source, LineArgs target)
        {
            var result = new List<CriteriaMatch>();

            var sourceMessage = XmlDal.CacheModel.GetFile(source.Path).GetNetworkMessage(source.iLine);

            if (sourceMessage == null)
            {
                return new List<CriteriaMatch>();
            }

            var targetMessage = XmlDal.CacheModel.GetFile(target.Path).GetNetworkMessage(target.iLine);

            if (targetMessage == null)
                return result;

            foreach (var sourceValues in sourceMessage.Source.Values)
            {
                foreach (var targetValues in targetMessage.Source.Values)
                {
                    if (sourceValues.Name == targetValues.Name || (sourceValues.Value == targetValues.Value && !string.IsNullOrEmpty(sourceValues.Value) && !string.IsNullOrEmpty(targetValues.Value)))
                    {
                        var model = new CriteriaMatch
                        {
                            SourceName = sourceValues.Name,
                            SourceType = "N/A",
                            SourceValue = sourceValues.Value,
                            TargetName = targetValues.Name,
                            TargetType = "N/A",
                            TargetValue = targetValues.Value
                        };

                        var exist = result.Any(item =>
                            String.Equals(item.SourceName, model.SourceName, StringComparison.CurrentCultureIgnoreCase) &&
                            String.Equals(item.SourceType, model.SourceType, StringComparison.CurrentCultureIgnoreCase) &&
                            String.Equals(item.SourceValue, model.SourceValue, StringComparison.CurrentCultureIgnoreCase) &&
                            String.Equals(item.TargetName, model.TargetName, StringComparison.CurrentCultureIgnoreCase) &&
                            String.Equals(item.TargetType, model.TargetType, StringComparison.CurrentCultureIgnoreCase) &&
                            String.Equals(item.TargetValue, model.TargetValue, StringComparison.CurrentCultureIgnoreCase));
                        if (!exist)
                            result.Add(model);
                    }
                }
            }

            return result;
        }
    }
}