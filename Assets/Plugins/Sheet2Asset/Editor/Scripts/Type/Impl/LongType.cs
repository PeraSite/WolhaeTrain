using System;

namespace Sheet2Asset.Type {
	[Type(typeof(long))]
	public class LongType : IType {
		public object DefaultValue => 0;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			if (long.TryParse(value, out var result)) {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return result;
		}

		public string Write(object value) {
			return value.ToString();
		}
	}
}
