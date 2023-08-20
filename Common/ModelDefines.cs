using System;
using System.Collections.Generic;
using System.Text;

namespace FTN.Common
{
	
	public enum DMSType : short
	{		
		MASK_TYPE							= unchecked((short)0xFFFF),

        CONNECTIVITYNODECONTAINER           = 0x0001,
        CONNECTIVITYNODE                    = 0x0002,
        DCLINESEGMENT                       = 0x0003,
        ACLINESEGMENT                       = 0x0004,
        SERIESCOMPENSATOR                   = 0x0005,
        TERMINAL                            = 0x0006,
    }

    [Flags]
	public enum ModelCode : long
	{
        IDOBJ                           = 0x1000000000000000,
        IDOBJ_GID                       = 0x1000000000000104,
        IDOBJ_ALIAS                     = 0x1000000000000207,
        IDOBJ_MRID                      = 0x1000000000000307,
        IDOBJ_NAME                      = 0x1000000000000407,

        PSR                             = 0x1100000000000000,

        CONNECTIVITYNODECONTAINER       = 0x1110000000010000,
        CONNECTIVITYNODECONTAINER_NODES = 0x1110000000010119,

        CONNECTIVITYNODE                = 0x1200000000020000,
        CONNECTIVITYNODE_DESCRIPTION    = 0x1200000000020107,
        CONNECTIVITYNODE_CNC            = 0x1200000000020209,
        CONNECTIVITYNODE_TERMINALS      = 0x1200000000020319,

        TERMINAL                        = 0x1300000000060000,
        TERMINAL_CN                     = 0x1300000000060409,
        TERMINAL_CE                     = 0x1300000000060509,

        EQUIPMENT                       = 0x1120000000000000,

        CONDUCTING_EQUIPMENT            = 0x1121000000000000,
        CONDUCTING_EQUIPMENT_TERMINALS  = 0x1121000000000119,

        CONDUCTOR                       = 0x1121100000000000,
        CONDUCTOR_LENGTH                = 0x1121100000000105,

        DCLINESEGMENT                   = 0x1121110000030000,

        ACLINESEGMENT                   = 0x1121120000040000,
        ACLINESEGMENT_B0                = 0x1121120000040105,
        ACLINESEGMENT_B                 = 0x1121120000040205,
        ACLINESEGMENT_G0                = 0x1121120000040305,
        ACLINESEGMENT_G                 = 0x1121120000040405,
        ACLINESEGMENT_R                 = 0x1121120000040505,
        ACLINESEGMENT_R0                = 0x1121120000040605,
        ACLINESEGMENT_X                 = 0x1121120000040705,
        ACLINESEGMENT_X0                = 0x1121120000040805,

        SERIESCOMPENSATOR               = 0x1121200000050000,
        SERIESCOMPENSATOR_R             = 0x1121200000050105,
        SERIESCOMPENSATOR_R0            = 0x1121200000050205,
        SERIESCOMPENSATOR_X             = 0x1121200000050305,
        SERIESCOMPENSATOR_X0            = 0x1121200000050405,
    }

    [Flags]
	public enum ModelCodeMask : long
	{
		MASK_TYPE			 = 0x00000000ffff0000,
		MASK_ATTRIBUTE_INDEX = 0x000000000000ff00,
		MASK_ATTRIBUTE_TYPE	 = 0x00000000000000ff,

		MASK_INHERITANCE_ONLY = unchecked((long)0xffffffff00000000),
		MASK_FIRSTNBL		  = unchecked((long)0xf000000000000000),
		MASK_DELFROMNBL8	  = unchecked((long)0xfffffff000000000),		
	}																		
}


