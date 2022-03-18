using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace PeraCore.Runtime {
	public static class ReflectionUtil {
		public static IEnumerable<Type> GetAllTypeImplements<T>() {
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(type => typeof(T).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);
		}

		public static IEnumerable<Type> GetAllTypeImplements(Type targetType) {
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(type => targetType.IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface);
		}

		public static IEnumerable<ValueDropdownItem> FilterType(object value) {
			if (value == null) return new List<ValueDropdownItem>();
			var valueType = value.GetType();
			var genericArguments = valueType.GetGenericArguments();


			if (genericArguments.Length > 0) {
				Debug.Log(valueType.GetNiceName() +":" + string.Join(",", genericArguments.SelectMany(GetAllTypeImplements).Select(t => t.GetNiceName())));
				return genericArguments.SelectMany(GetAllTypeImplements).Select(type => new ValueDropdownItem("test", type));
			}

			var baseType = valueType.BaseType;
			return GetAllTypeImplements(baseType).Select(type => new ValueDropdownItem("test", type));
		}
	}
}
