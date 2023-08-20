using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class ConnectivityNodeContainer : PowerSystemResource
    {
        private List<long> connectivityNodes = new List<long>();
        public ConnectivityNodeContainer(long globalId)
            : base(globalId)
        {
        }

        public List<long> ConnectivityNodes { get => connectivityNodes; set => connectivityNodes = value; }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ConnectivityNodeContainer x = (ConnectivityNodeContainer)obj;
                return (CompareHelper.CompareLists(x.connectivityNodes, this.connectivityNodes, true));
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
                case ModelCode.CONNECTIVITYNODECONTAINER_NODES:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.CONNECTIVITYNODECONTAINER_NODES:
                    property.SetValue(connectivityNodes);
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

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        // da li imam nesto u listi referenci
        public override bool IsReferenced { get { return connectivityNodes.Count > 0 || base.IsReferenced; } }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (connectivityNodes != null && connectivityNodes.Count > 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CONNECTIVITYNODECONTAINER_NODES] = connectivityNodes.GetRange(0, connectivityNodes.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CONNECTIVITYNODE_CNC: // model code klase koju cu dodavati u listu
                    connectivityNodes.Add(globalId);
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
                case ModelCode.CONNECTIVITYNODE_CNC:
                    if (connectivityNodes.Contains(globalId))
                        connectivityNodes.Remove(globalId);
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
