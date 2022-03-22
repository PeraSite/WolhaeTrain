using TMPro;
using UnityAtoms;
using UnityEngine;

public class EndingUI : MonoBehaviour {
	public EndingDataEvent LastEndingChanged;

	public TextMeshProUGUI Title;
	public TextMeshProUGUI Description;

	private void OnEnable() {
		LastEndingChanged.Register(OnEnding);
	}

	private void OnDisable() {
		LastEndingChanged.Unregister(OnEnding);
	}

	private void OnEnding(EndingData data) {
		DebugUtils.Log($"Showing ending:" + data.Title);
		Title.text = data.Title;
		Description.text = data.Description;
	}

	public void ReturnToMain() {

	}
}
