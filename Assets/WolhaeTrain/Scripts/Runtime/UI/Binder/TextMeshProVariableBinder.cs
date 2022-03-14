using TMPro;
using UnityAtoms;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProVariableBinder : MonoBehaviour {
	public AtomBaseVariable Variable;
	public AtomEventBase Event;

	private TextMeshProUGUI _text;

	private void Awake() {
		_text = GetComponent<TextMeshProUGUI>();
	}

	private void Start() {
		UpdateText();
	}

	private void OnEnable() {
		Event.Register(UpdateText);
	}

	private void OnDisable() {
		Event.Unregister(UpdateText);
	}

	private void UpdateText() {
		_text.text = Variable.BaseValue.ToString();
	}
}
