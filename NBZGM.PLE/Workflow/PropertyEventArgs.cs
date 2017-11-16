using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Taizhou.PLE.WorkflowLib
{
	public class PropertyEventArgs<TKey, TValue> : EventArgs
	{
		public TKey Key { get; set; }

		public TValue Value { get; set; }

		public PropertyEventArgs() { }

		public PropertyEventArgs(TKey key, TValue value)
		{
			this.Key = key;
			this.Value = value;
		}
	}
}
