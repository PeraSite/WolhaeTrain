using System;
using System.Text;

namespace Sheet2Asset.Type {
	[Type(typeof(byte[]))]
	public class ByteArrayType : IType {
		public object DefaultValue => null;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			var bytes = Encoding.Default.GetBytes(value);
			return bytes;
		}

		public string Write(object value) {
			return Encoding.Default.GetString(value as byte[] ?? Array.Empty<byte>());
		}
	}
}
