using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheet2Asset.Type {
	[Type(typeof(List<char>))]
	public class CharListType : IType {
		public object DefaultValue => null;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			var list = new List<char>();
			if (value == "[]") return list;

			var array = ReadUtil.GetBracketValueToArray(value);
			if (array != null) {
				list.AddRange(array.Select(char.Parse));
			} else {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return list;
		}

		public string Write(object value) {
			var list = value as List<char>;
			return WriteUtil.SetValueToBracketArray(list);
		}
	}
}
