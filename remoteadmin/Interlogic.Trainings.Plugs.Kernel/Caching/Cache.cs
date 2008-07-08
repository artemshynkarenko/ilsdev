using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Interlogic.Trainings.Plugs.Kernel.Caching
{
	public class Cache
	{
		private static Hashtable _cache = Hashtable.Synchronized(new Hashtable());

		public static void Set(object key, object value)
		{
			_cache[key] = value;
		}

		public static object Get(object key)
		{
			return _cache[key];
		}
	}
}
