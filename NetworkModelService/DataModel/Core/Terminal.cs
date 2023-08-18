using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Terminal : IdentifiedObject
    {
        private long connectivityNode = 0;      //From ConnectivityNode (0, 1)
        private long conductingEquipment = 0;   //From ConductingEquipment (1, 1)
        public Terminal(long globalId) : base(globalId)
        {
        }

        public long ConnectivityNode
        {
            get { return connectivityNode; }
            set { connectivityNode = value; }
        }

        public long ConductingEquipment
        {
            get { return conductingEquipment; }
            set { conductingEquipment = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Terminal x = (Terminal)obj;
                return (x.connectivityNode == this.connectivityNode && x.conductingEquipment == this.conductingEquipment);
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

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                case ModelCode.TERMINAL_CN:
                case ModelCode.TERMINAL_CE:
                    return true;

                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.TERMINAL_CN:
                    prop.SetValue(connectivityNode);
                    break;

                case ModelCode.TERMINAL_CE:
                    prop.SetValue(conductingEquipment);
                    break;

                default:
                    base.GetProperty(prop);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.TERMINAL_CN:
                    connectivityNode = property.AsLong();
                    break;

                case ModelCode.TERMINAL_CE:
                    conductingEquipment = property.AsLong();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        //Prima ConnectivityNode(0, 1) i ConductingEquipment(1,1) 
        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (conductingEquipment != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CE] = new List<long>();
                references[ModelCode.TERMINAL_CE].Add(conductingEquipment);
            }

            if (connectivityNode != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.TERMINAL_CN] = new List<long>();
                references[ModelCode.TERMINAL_CN].Add(connectivityNode);
            }

            base.GetReferences(references, refType);
        }

        #endregion IReference implementation
    }
}
