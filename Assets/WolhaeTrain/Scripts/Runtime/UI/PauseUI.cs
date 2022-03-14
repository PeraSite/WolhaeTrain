using System;
using PixelCrushers;
using UnityEngine;

public class PauseUI : MonoBehaviour {

	public string buttonName;

	public UIPanel PausePanel;

	private void Update() {
		if (InputDeviceManager.IsButtonDown(buttonName)) {
			PausePanel.Toggle();
		}
	}
}
