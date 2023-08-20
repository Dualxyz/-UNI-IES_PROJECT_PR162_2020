﻿using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using FTN.Services.NetworkModelService.DataModel.Wires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTN.Services.NetworkModelService.DataModel.IES_Projects
{
    public class SeriesCompensator : ConductingEquipment
    {
        private float r;
        private float r0;
        private float x;
        private float x0;

        public float R { get => r; set => r = value; }
        public float R0 { get => r0; set => r0 = value; }
        public float X { get => x; set => x = value; }
        public float X0 { get => x0; set => x0 = value; }
        public SeriesCompensator(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                SeriesCompensator x = (SeriesCompensator)obj;
                return (x.r == this.r &&
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
                case ModelCode.SERIESCOMPENSATOR_R:
                case ModelCode.SERIESCOMPENSATOR_R0:
                case ModelCode.SERIESCOMPENSATOR_X:
                case ModelCode.SERIESCOMPENSATOR_X0:
                    return true;

                default:
                    return base.HasProperty(property);
            }
        }

        public override void GetProperty(Property prop)
        {
            switch (prop.Id)
            {
                case ModelCode.SERIESCOMPENSATOR_R:
                    prop.SetValue(r);
                    break;
                case ModelCode.SERIESCOMPENSATOR_R0:
                    prop.SetValue(r0);
                    break;
                case ModelCode.SERIESCOMPENSATOR_X:
                    prop.SetValue(x);
                    break;
                case ModelCode.SERIESCOMPENSATOR_X0:
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
                case ModelCode.SERIESCOMPENSATOR_R:
                    r = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_R0:
                    r0 = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_X:
                    x = property.AsFloat();
                    break;
                case ModelCode.SERIESCOMPENSATOR_X0:
                    x0 = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }
    }
}
