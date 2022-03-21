using System;
using PixelCrushers;
using UnityEngine;

public class PauseUI : MonoBehaviour {

	public string buttonName;

	public UIPanel PausePanel;
	public GameObject QuestPanel;

	private void Update() {
		if (QuestPanel.activeSelf) return;
		if (InputDeviceManager.IsButtonDown(buttonName)) {
			PausePanel.Toggle();
		}
	}
}
