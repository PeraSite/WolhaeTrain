using System;
using UnityEngine;

public class OnlyActiveDebug : MonoBehaviour {
	private void Start() {
		if (!Debug.isDebugBuild) Destroy(gameObject);
	}
}
