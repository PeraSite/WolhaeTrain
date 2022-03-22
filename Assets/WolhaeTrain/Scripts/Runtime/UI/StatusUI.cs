using System.Collections.Generic;
using System.Linq;
using System.Web.WebPages;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class StatusUI : SerializedMonoBehaviour {
	public StatusUIUpdateEvent StatusUIUpdateEvent;

	public GameObject Panel;

	public TextMeshProUGUI Name;

	public TextMeshProUGUI Hunger;
	public TextMeshProUGUI Mental;
	public Dictionary<StatusEffect, GameObject> RowData;

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

				Name.text = payload.Stat.Type.GetName();
				Hunger.text = payload.Stat.Hunger.ToString();
				Mental.text = payload.Stat.Mental.ToString();

				RowData.ForEach(pair => {
					var (type, obj) = pair;
					obj.SetActive(payload.Stat.Effects.Contains(type));
				});
			}
			var screenPoint = _cam.WorldToScreenPoint(payload.Position);
			Panel.transform.position = screenPoint;
		} else {
			Panel.SetActive(false);
			_lastType = CharacterType.None;
		}
	}
}
