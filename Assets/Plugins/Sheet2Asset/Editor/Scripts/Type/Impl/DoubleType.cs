using System;

namespace Sheet2Asset.Type {
	[Type(typeof(double))]
	public class DoubleType : IType {
		public object DefaultValue => 0.0d;

		public object Read(string value) {
			if (string.IsNullOrEmpty(value))
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);

			if (!double.TryParse(value, out var result)) {
				throw new Exception("Parse Field => " + value + " To " + GetType().Name);
			}
			return result;
		}

		public string Write(object value) {
			return value.ToString();
		}
	}
}
