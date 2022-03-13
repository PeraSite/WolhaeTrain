using System;

namespace Sheet2Asset.Type {
	[Type(typeof(char))]
	public class CharType : IType {
		public object DefaultValue => char.MinValue;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			if (!char.TryParse(value, out var result)) {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return result;
		}

		public string Write(object value) {
			return value.ToString();
		}
	}
}
