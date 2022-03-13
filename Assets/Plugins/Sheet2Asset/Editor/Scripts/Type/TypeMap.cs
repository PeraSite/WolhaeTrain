using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sheet2Asset.Type;
using Sirenix.Utilities;
using UnityEngine;

public static class TypeMap {
	public static bool Initialized;

	public static Dictionary<Type, IType> Map { get; } = new Dictionary<Type, IType>();

	public static void Init() {
		if (Initialized) {
			return;
		}

		var subClasses = GetAllSubclassOf(typeof(IType));
		foreach (var data in subClasses) {
			if (data.IsInterface)
				continue;
			var instance = Activator.CreateInstance(data);
			var att = CustomAttributeExtensions.GetCustomAttribute<TypeAttribute>(instance.GetType());

			if (att == null) {
				throw new Exception("Can't find TypeAttribute at " + instance.GetType().GetNiceName());
			}

			if (!Map.ContainsKey(att.type)) {
				Map[att.type] = (IType) instance;
			}
		}
		Initialized = true;
	}

	public static IEnumerable<Type> GetAllSubclassOf(Type parent) {
		var type = parent;
		var types = AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(s => s.GetTypes())
			.Where(p => type.IsAssignableFrom(p));
		return types;
	}

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void Reset() {
		Initialized = false;
		Map.Clear();
	}
}
