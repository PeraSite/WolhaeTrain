using System;

namespace Sheet2Asset.Type {
	[Type(typeof((int, int)))]
	public class IntTupleX2Type : IType {
		public object DefaultValue => null;

		public object Read(string value) {
			var array = ReadUtil.GetBracketValueToArray(value);
			if (array.Length == 0 || array.Length == 1 || array.Length > 2) {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return (array[0], array[1]);
		}

		public string Write(object value) {
			var (item1, item2) = ((int, int)) value;
			return $"[{item1},{item2}]";
		}
	}
}
