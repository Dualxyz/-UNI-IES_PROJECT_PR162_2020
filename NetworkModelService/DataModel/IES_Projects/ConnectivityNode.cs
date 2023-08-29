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


        #region IAccess implementation

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.CONNECTIVITY_NODE_DESCRIPTION:
                case ModelCode.CONNECTIVITY_NODE_TERMINALS:
                case ModelCode.CONNECTIVITY_NODE_CNC:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.CONNECTIVITY_NODE_DESCRIPTION:
                    prop.SetValue(description);
                    break;

                case ModelCode.CONNECTIVITY_NODE_TERMINALS:
                    prop.SetValue(terminals);
                    break;

                case ModelCode.CONNECTIVITY_NODE_CNC:
                    prop.SetValue(connectivityNodeContainer);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        //[Q]ERROR??
        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        //private List<long> terminals = new List<long>(); //From Terminal (0, N)
        //private long connectivityNodeContainer = 0; //From ConnectivityNodeCon

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return terminals.Count > 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (terminals != null && terminals.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNECTIVITY_NODE_TERMINALS] = terminals.GetRange(0, terminals.Count);
            }

            if (connectivityNodeContainer != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNECTIVITY_NODE_CNC] = new List<long>();
                references[ModelCode.CONNECTIVITY_NODE_CNC].Add(connectivityNodeContainer);
            }


            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CONNECTIVITY_NODE_TERMINALS:
                    terminals.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CONNECTIVITY_NODE_TERMINALS:

                    if (terminals.Contains(globalId))
                    {
                        terminals.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}
