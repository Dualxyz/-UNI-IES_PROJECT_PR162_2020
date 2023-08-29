using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class ACLineSegment : Conductor
    {
        private float b0ch;
        private float bch;
        private float g0ch;
        private float gch;
        private float r;
        private float r0;
        private float x;
        private float x0;

        public ACLineSegment(long globalId)
            : base(globalId)
        {
        }

        # region GET AND SET
        public float B0ch
        {
            get { return b0ch; }
            set { b0ch = value; }
        }

        public float Bch
        {
            get { return bch; }
            set { bch = value; }
        }
        public float G0ch
        {
            get { return g0ch; }
            set { g0ch = value; }
        }
        public float Gch
        {
            get { return gch; }
            set { gch = value; }
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

        public float X
        {
            get { return x; }
            set { x = value; }
        }

        public float X0
        {
            get { return x0; }
            set { x0 = value; }
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ACLineSegment x = (ACLineSegment)obj;
                return (x.b0ch == this.b0ch
                    && x.bch == this.bch
                    && x.g0ch == this.g0ch
                    && x.gch == this.gch
                    && x.r == this.r
                    && x.r0 == this.r0
                    && x.x == this.x
                    && x.x0 == this.x0);
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
                case ModelCode.AC_LINE_SEGMENT_BCH:
                case ModelCode.AC_LINE_SEGMENT_B0CH:
                case ModelCode.AC_LINE_SEGMENT_G0CH:
                case ModelCode.AC_LINE_SEGMENT_GCH:
                case ModelCode.AC_LINE_SEGMENT_R:
                case ModelCode.AC_LINE_SEGMENT_R0:
                case ModelCode.AC_LINE_SEGMENT_X:
                case ModelCode.AC_LINE_SEGMENT_X0:

                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.AC_LINE_SEGMENT_BCH:
                    property.SetValue(bch);
                    break;

                case ModelCode.AC_LINE_SEGMENT_B0CH:
                    property.SetValue(b0ch);
                    break;

                case ModelCode.AC_LINE_SEGMENT_G0CH:
                    property.SetValue(g0ch);
                    break;

                case ModelCode.AC_LINE_SEGMENT_GCH:
                    property.SetValue(gch);
                    break;

                case ModelCode.AC_LINE_SEGMENT_R:
                    property.SetValue(r);
                    break;

                case ModelCode.AC_LINE_SEGMENT_R0:
                    property.SetValue(r0);
                    break;

                case ModelCode.AC_LINE_SEGMENT_X:
                    property.SetValue(x);
                    break;

                case ModelCode.AC_LINE_SEGMENT_X0:
                    property.SetValue(x0);
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
                case ModelCode.AC_LINE_SEGMENT_BCH:
                    bch = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_B0CH:
                    b0ch = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_G0CH:
                    g0ch = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_GCH:
                    gch = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_R:
                    r = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_R0:
                    r0 = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_X:
                    x = property.AsFloat();
                    break;

                case ModelCode.AC_LINE_SEGMENT_X0:
                    x0 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
        #endregion
    }
}
