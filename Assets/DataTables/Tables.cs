using System;
using System.Collections;

namespace Tables
{
	public class Avatar : LookupData<int>
	{
		public int id;
		public float scale;
		public string name;
		public string url;
		public int GetKey() { return id ; }
	}
}

namespace Tables
{
	public class UITemplate : LookupData<int>
	{
		public int id;
		public string sName;
		public string sPkgName;
		public string sCompName;
		public string sDesc;
		public int GetKey() { return id ; }
	}
}
