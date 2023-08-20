using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class ConnectivityNode : IdentifiedObject
    {
        private string description;
        private long connectivityNodeContainer = 0;
        private List<long> terminals = new List<long>();

        public string Description { get => description; set => description = value; }
        public long ConnectivityNodeContainer { get => connectivityNodeContainer; set => connectivityNodeContainer = value; }
        public List<long> Terminals { get => terminals; set => terminals = value; }
        public ConnectivityNode(long globalId)
            : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNode x = (ConnectivityNode)obj;
                return (x.description == this.description &&
                    x.connectivityNodeContainer == this.connectivityNodeContainer &&
                    CompareHelper.CompareLists(x.terminals, this.terminals, true));
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

        public override bool HasProperty(ModelCode property)
        {
            switch (property)
            {
                case ModelCode.CONNECTIVITYNODE_DESCRIPTION:
                case ModelCode.CONNECTIVITYNODE_CNC:
                case ModelCode.CONNECTIVITYNODE_TERMINALS:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONNECTIVITYNODE_DESCRIPTION:
                    property.SetValue(description);
                    break;
                case ModelCode.CONNECTIVITYNODE_CNC:
                    property.SetValue(connectivityNodeContainer);
                    break;
                case ModelCode.CONNECTIVITYNODE_TERMINALS:
                    property.SetValue(terminals);
                    break;
                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONNECTIVITYNODE_DESCRIPTION:
                    description = property.AsString();
                    break;

                case ModelCode.CONNECTIVITYNODE_CNC:
                    connectivityNodeContainer = property.AsReference();
                    break;
                // ne radi se za listu
                default:
                    base.SetProperty(property);
                    break;
            }
        }

        public override bool IsReferenced { get { return terminals.Count > 0 || base.IsReferenced; } }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (terminals != null && terminals.Count > 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNECTIVITYNODE_TERMINALS] = terminals.GetRange(0, terminals.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.TERMINAL_CN: // model code klase koju cu dodavati u listu
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
                case ModelCode.TERMINAL_CN:
                    if (terminals.Contains(globalId))
                        terminals.Remove(globalId);
                    else
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }
    }
}
