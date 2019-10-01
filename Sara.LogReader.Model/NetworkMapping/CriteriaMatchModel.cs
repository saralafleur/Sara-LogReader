using System;
using System.Collections.Generic;

namespace Sara.LogReader.Model.NetworkMapping
{
    public class CriteriaMatchModel
    {
        public List<CriteriaMatch> Matches { get; set; }
        public TimeSpan? DateTimeDifference { get; set; }

        public CriteriaMatchModel()
        {
            Matches = new List<CriteriaMatch>();
            DateTimeDifference = new TimeSpan(0);
        }
    }
}