using System;

namespace Sheet2Asset.Type {
	[Type(typeof(short))]
	public class ShortType : IType {
		public object DefaultValue => 0;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			if (!short.TryParse(value, out var result)) {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return result;
		}

		public string Write(object value) {
			return value.ToString();
		}
	}
}
