using System.Collections.Generic;
using System.Linq;
using Sara.LogReader.Common;

namespace Sara.LogReader.Model.NetworkMapping
{
    public enum MappingDataType
    {
        FileValue,
        EventValue
    }

    public class MapCriteria
    {
        public MapCriteria()
        {
            Enabled = true; // Default
            Operator = Keywords.EQUAL; // Default
        }
        public MappingDataType SourceType { get; set; }
        public string SourceName { get; set; }
        public MappingDataType TargetType { get; set; }
        public string TargetName { get; set; }
        public double? TimeConditionMs { get; set; }
        public bool UseSourceValue { get; set; }
        public bool UseTargetValue { get; set; }
        public string SourceValue { get; set; }
        public string TargetValue { get; set; }
        public bool Enabled { get; set; }
        public string Operator { get; set; }

        public override string ToString()
        {
            var sourceValueText = string.Empty;
            var targetValueText = string.Empty;
            var timeCondition = string.Empty;

            if (UseSourceValue)
                sourceValueText = $"[{SourceValue}]";

            if (UseTargetValue)
                targetValueText = $"[{TargetValue}]";

            if (TimeConditionMs != null)
                timeCondition = $" tc:{TimeConditionMs.Value}";

            return string.Format("{0}{1}:{2}{3} {8} {4}:{5}{6}{7}",
                Enabled ? "" : "Disabled---",
                SourceType, SourceName, sourceValueText,
                TargetType, TargetName, targetValueText,
                timeCondition, Operator);
        }

        public MapCriteria Copy()
        {
            return new MapCriteria
            {
                SourceType = SourceType,
                SourceName = SourceName,
                TargetType = TargetType,
                TargetName = TargetName,
                TimeConditionMs = TimeConditionMs,
                UseSourceValue = UseSourceValue,
                UseTargetValue = UseTargetValue,
                SourceValue = SourceValue,
                TargetValue = TargetValue,
                Enabled = Enabled,
                Operator = Operator
            };
        }
    }

    public class NetworkMap
    {
        public NetworkMap()
        {
            Criteria = new List<MapCriteria>();
            Enabled = true; // Default
        }

        public int NetworkMapId { get; set; }
        public string Name { get; set; }
        public string RegularExpression { get; set; }
        public List<MapCriteria> Criteria { get; set; }
        public int Priority { get; set; }
        /// <summary>
        /// Only use if prior Maps have no results
        /// </summary>
        public bool OnlyUseFallThrough { get; set; }

        public bool Enabled { get; set; }

        public override string ToString()
        {
            return $"{(Enabled ? "" : "Disabled ")}{NetworkMapId} - {Name}";
        }

        public NetworkMap Clone()
        {
            var criteriaCopy = Criteria.Select(mapCriteria => mapCriteria.Copy()).ToList();

            return new NetworkMap
            {
                NetworkMapId = XmlDal.DataModel.GetUniqueNetworkMapId(),
                Name = $"Copy of {Name}",
                RegularExpression = RegularExpression,
                Criteria = criteriaCopy,
                Priority = Priority + 1,
                OnlyUseFallThrough = OnlyUseFallThrough
            };
        }

        public void Copy(NetworkMap item)
        {
            Name = item.Name;
            RegularExpression = item.RegularExpression;
            Criteria = item.Criteria;
            Priority = item.Priority;
            OnlyUseFallThrough = OnlyUseFallThrough;
        }
    }
}
