using TMPro;
using UnityAtoms;
using UnityEngine;

public class EndingUI : MonoBehaviour {
	public EndingDataEvent EndingEvent;

	public TextMeshProUGUI Title;
	public TextMeshProUGUI Description;

	private void OnEnable() {
		EndingEvent.Register(OnEnding);
	}

	private void OnDisable() {
		EndingEvent.Unregister(OnEnding);
	}

	private void OnEnding(EndingData data) {
		Title.text = data.Title;
		Description.text = data.Description;
	}
}
