using System;
using System.Collections.Generic;

namespace Sheet2Asset.Type {
	[Type(typeof(List<double>))]
	public class DoubleListType : IType {
		public object DefaultValue => null;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);


			var list = new List<double>();
			if (value == "[]") return list;

			var array = ReadUtil.GetBracketValueToArray(value);
			if (array != null) {
				foreach (var data in array)
					list.Add(double.Parse(data));
			} else {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return list;
		}

		public string Write(object value) {
			var list = value as List<double>;
			return WriteUtil.SetValueToBracketArray(list);
		}
	}
}
