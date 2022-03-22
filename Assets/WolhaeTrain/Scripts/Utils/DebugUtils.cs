using UnityEngine;

public static class DebugUtils {
	public static void Log(object obj) {
		if (Debug.isDebugBuild || Application.isEditor) {
			Debug.Log(obj);
		}
	}
}
