using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private long connectivityNode = 0;
        private long conductingEquipment = 0;

        public long ConnectivityNode { get => connectivityNode; set => connectivityNode = value; }
        public long ConductingEquipment { get => conductingEquipment; set => conductingEquipment = value; }
       
        public Terminal(long globalId)
            : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (x.connectivityNode == this.connectivityNode &&
                    x.conductingEquipment == this.conductingEquipment);
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
                case ModelCode.TERMINAL_CN:
                case ModelCode.TERMINAL_CE:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CN:
                    property.SetValue(connectivityNode);
                    break;
                case ModelCode.TERMINAL_CE:
                    property.SetValue(conductingEquipment);
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
                case ModelCode.TERMINAL_CN:
                    connectivityNode = property.AsReference();
                    break;

                case ModelCode.TERMINAL_CE:
                    conductingEquipment = property.AsReference();
                    break;
                // ne radi se za listu
                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion IAccess implementation

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (connectivityNode != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CN] = new List<long>();
                references[ModelCode.TERMINAL_CN].Add(connectivityNode);
            }
            if (conductingEquipment != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CE].Add(conductingEquipment);
            }

            base.GetReferences(references, refType);
        }
    }
}
