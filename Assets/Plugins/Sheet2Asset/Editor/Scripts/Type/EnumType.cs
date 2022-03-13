using System;
using System.Reflection;

namespace Sheet2Asset.Type {
	public class EnumType {
		public System.Type Type { get; set; }
		public Assembly Assembly { get; set; }
		public string NameSpace { get; set; }
		public string EnumName { get; set; }

		public object Read(string value) {
			return Enum.Parse(Type, value);
		}

		public string Write(object value) {
			return value.ToString();
		}
	}
}
