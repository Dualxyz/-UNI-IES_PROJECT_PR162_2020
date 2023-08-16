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
    }
}
