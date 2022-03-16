using System;
using System.Linq;
using System.Web.WebPages;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class StatusUI : MonoBehaviour {
	public StatusUIUpdateEvent StatusUIUpdateEvent;

	public GameObject Panel;

	public TextMeshProUGUI Hunger;
	public TextMeshProUGUI Mental;
	public TextMeshProUGUI Effect;

	private Camera _cam;
	private CharacterType _lastType;

	private void Start() {
		_cam = Camera.main;
	}

	private void OnEnable() {
		StatusUIUpdateEvent.Register(UpdateStatus);
	}

	private void OnDisable() {
		StatusUIUpdateEvent.Unregister(UpdateStatus);
	}

	private void UpdateStatus(StatusUIUpdatePayload payload) {
		if (payload.ShowPanel) {
			Panel.SetActive(true);

			if (payload.Stat != null) {
				if (_lastType != CharacterType.None && payload.Stat.Type != _lastType) return;

				_lastType = payload.Stat.Type;
				Hunger.text = payload.Stat.Hunger.ToString();
				Mental.text = payload.Stat.Mental.ToString();
				var effectText = string.Join(", ", payload.Stat.Effects.Select(GetEffectName));
				Effect.text = effectText.IsEmpty() ? "없음" : effectText;
			}
			var screenPoint = _cam.WorldToScreenPoint(payload.Position);
			Panel.transform.position = screenPoint;
		} else {
			Panel.SetActive(false);
			_lastType = CharacterType.None;
		}
	}

	private string GetEffectName(StatusEffect effect) {
		return effect switch {
			StatusEffect.Exhaust => "탈진",
			StatusEffect.Cold => "감기",
			StatusEffect.Infect => "감염",
			StatusEffect.Hurt => "상처",
			StatusEffect.Crazy => "미침",
			_ => throw new ArgumentOutOfRangeException(nameof(effect), effect, null)
		};
	}
}
