﻿using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.IES_Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public float B0ch { get => b0ch; set => b0ch = value; }
        public float Bch { get => bch; set => bch = value; }
        public float G0ch { get => g0ch; set => g0ch = value; }
        public float Gch { get => gch; set => gch = value; }
        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }
        public ACLineSegment(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                ACLineSegment x = (ACLineSegment)obj;
                return (x.b0ch == this.b0ch &&
                        x.bch == this.bch &&
                        x.g0ch == this.g0ch &&
                        x.gch == this.gch &&
                        x.r == this.r &&
                        x.r0 == this.r0 &&
                        x.x == this.x &&
                        x.x0 == this.x0);
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
                case ModelCode.ACLINESEGMENT_B0:
                case ModelCode.ACLINESEGMENT_B:
                case ModelCode.ACLINESEGMENT_G0:
                case ModelCode.ACLINESEGMENT_G:
                case ModelCode.ACLINESEGMENT_R:
                case ModelCode.ACLINESEGMENT_R0:
                case ModelCode.ACLINESEGMENT_X:
                case ModelCode.ACLINESEGMENT_X0:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.ACLINESEGMENT_B0:
                    prop.SetValue(b0ch);
                    break;
                case ModelCode.ACLINESEGMENT_B:
                    prop.SetValue(bch);
                    break;
                case ModelCode.ACLINESEGMENT_G0:
                    prop.SetValue(g0ch);
                    break;
                case ModelCode.ACLINESEGMENT_G:
                    prop.SetValue(gch);
                    break;
                case ModelCode.ACLINESEGMENT_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.ACLINESEGMENT_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.ACLINESEGMENT_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.ACLINESEGMENT_X0:
                    prop.SetValue(x0);
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
                case ModelCode.ACLINESEGMENT_B0:
                    b0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_B:
                    bch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_G0:
                    g0ch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_G:
                    gch = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.ACLINESEGMENT_X0:
                    x0 = property.AsFloat();
                    break;
                default:
                    base.SetProperty(property);
                    break;
            }
        }
    }
}

