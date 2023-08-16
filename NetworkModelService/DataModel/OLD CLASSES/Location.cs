using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using FTN.Common;

namespace FTN.Services.NetworkModelService.DataModel.Core
{	
	public class Location : IdentifiedObject
	{
		private string corporateCode = string.Empty;
		private string category = string.Empty;
		private List<long> powerSystemResources = new List<long>();				

		public Location(long globalId) : base(globalId) 
		{
		}
		
		public List<long> PowerSystemResources
		{
			get
			{
				return powerSystemResources;
			}

			set
			{
				powerSystemResources = value;
			}
		}

		public string CorporateCode
		{
			get { return corporateCode; }
			set { corporateCode = value; }
		}

		public string Category
		{
			get { return category; }
			set { category = value; }
		}
	
		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Location x = (Location)obj;
				return (x.category == this.category && x.corporateCode == this.corporateCode && CompareHelper.CompareLists(x.powerSystemResources, this.powerSystemResources, true));
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
