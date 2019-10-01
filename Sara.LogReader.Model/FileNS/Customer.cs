using System.Collections.Generic;

namespace Sara.LogReader.Model.FileNS
{
    public class Customer
    {
        public string Name { get; set; }
        /// <summary>
        /// List of Nodes that are associated with this customer
        /// </summary>
        public List<Node> Nodes { get; set; }
    }
}
