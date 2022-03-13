using System;

namespace Sheet2Asset.Type {
	[Type(typeof(byte))]
	public class ByteType : IType {
		public object DefaultValue => 0;

		public object Read(string value) {
			if (byte.TryParse(value, out var result))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			return result;
		}

		public string Write(object value) {
			return value.ToString();
		}
	}
}
