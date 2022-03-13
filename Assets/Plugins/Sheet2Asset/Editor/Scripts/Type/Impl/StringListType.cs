using System.Collections.Generic;
using System.Linq;

namespace Sheet2Asset.Type {
	[Type(typeof(List<string>))]
	public class StringListType : IType {
		public object DefaultValue => string.Empty;

		public object Read(string value) {
			var array = ReadUtil.GetBracketValueToArray(value);
			return array.ToList();
		}

		public string Write(object value) {
			return WriteUtil.SetValueToBracketArray<List<string>, string>(value);
		}
	}
}
