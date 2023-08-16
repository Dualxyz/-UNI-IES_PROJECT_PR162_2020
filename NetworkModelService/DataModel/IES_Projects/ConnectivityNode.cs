using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class ConnectivityNode : IdentifiedObject
    {
        private string description = String.Empty;
        private List<long> terminals = new List<long>(); //From Terminal (0, N)
        private long connectivityNodeContainer = 0; //From ConnectivityNodeContainer (1, 1)
        public ConnectivityNode(long globalId) : base(globalId)
        {
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public List<long> Terminals
        {
            get { return terminals; }
            set { terminals = value; }
        }
        public long ConnectivityNodeContainer
        {
            get { return connectivityNodeContainer; }
            set { connectivityNodeContainer = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNode x = (ConnectivityNode)obj;
                return (CompareHelper.CompareLists(x.terminals, this.terminals, true) && 
                    x.connectivityNodeContainer == this.connectivityNodeContainer && 
                    x.description == this.description);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
