using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheet2Asset.Type {
	[Type(typeof(List<int>))]
	public class IntListType : IType {
		public object DefaultValue => null;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);


			var list = new List<int>();
			if (value == "[]") return list;

			var array = ReadUtil.GetBracketValueToArray(value);
			if (array != null) {
				list.AddRange(array.Select(int.Parse));
			} else {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return list;
		}

		public string Write(object value) {
			var list = value as List<int>;
			return WriteUtil.SetValueToBracketArray(list);
		}
	}
}
