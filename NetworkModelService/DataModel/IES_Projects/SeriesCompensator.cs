using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class SeriesCompensator : ConductingEquipment
    {
        private float r;
        private float r0;
        private float f;
        private float f0;

        public SeriesCompensator(long globalId) : base(globalId)
        {
        }
        public float R
        {
            get { return r; }
            set { r = value; }
        }

        public float R0
        {
            get { return r0; }
            set { r0 = value; }
        }

        public float F
        {
            get { return f; }
            set { f = value; }
        }

        public float F0
        {
            get { return f0; }
            set { f0 = value; }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeriesCompensator x = (SeriesCompensator)obj;
                return (x.r ==  this.r && x.r0 == this.r0 && x.f == this.f && x.f0 == this.f0);
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
                case ModelCode.SERIES_COMPENSATOR_R:
                case ModelCode.SERIES_COMPENSATOR_R0:
                case ModelCode.SERIES_COMPENSATOR_X:
                case ModelCode.SERIES_COMPENSATOR_X0:
                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SERIES_COMPENSATOR_R:
                    property.SetValue(r);
                    break;

                case ModelCode.SERIES_COMPENSATOR_R0:
                    property.SetValue(r0);
                    break;

                    //[Q]DON'T LOOK LOL
                case ModelCode.SERIES_COMPENSATOR_X:
                    property.SetValue(f);
                    break;

                case ModelCode.SERIES_COMPENSATOR_X0:
                    property.SetValue(f0);
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
                case ModelCode.SERIES_COMPENSATOR_R:
                    r = property.AsFloat();
                    break;

                case ModelCode.SERIES_COMPENSATOR_R0:
                    r0 = property.AsFloat();
                    break;

                case ModelCode.SERIES_COMPENSATOR_X:
                    f = property.AsFloat();
                    break;

                case ModelCode.SERIES_COMPENSATOR_X0:
                    f0 = property.AsFloat();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion
    }
}
