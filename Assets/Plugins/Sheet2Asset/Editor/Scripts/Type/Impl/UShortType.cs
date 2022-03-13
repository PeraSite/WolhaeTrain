using System;

namespace Sheet2Asset.Type {
	[Type(typeof(ushort))]
	public class UShortType : IType {
		public object DefaultValue => 0;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			if (!ushort.TryParse(value, out var result)) {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return result;
		}


		public string Write(object value) {
			return value.ToString();
		}
	}
}
