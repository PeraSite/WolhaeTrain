using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace PeraCore.Runtime {
	public class CustomScriptableObject : SerializedScriptableObject {
		public IEnumerable<ValueDropdownItem> FilterType(object value) => ReflectionUtil.FilterType(value);
	}
}
