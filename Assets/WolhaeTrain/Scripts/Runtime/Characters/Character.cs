﻿using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class Character : MonoBehaviour {
	[Header("이벤트")]
	public CharacterStatEvent CharacterStatChangedEvent;

	public StatusUIUpdateEvent StatusUIUpdateEvent;

	[Header("스탯")]
	public CharacterStatVariable Stat;

	public Vector3 UIOffset;

	private bool _isShowing;
	private Transform _transform;

	private void Awake() {
		_transform = GetComponent<Transform>();
	}

	private void OnEnable() {
		CharacterStatChangedEvent.Register(OnStatChanged);
	}

	private void OnDisable() {
		CharacterStatChangedEvent.Unregister(OnStatChanged);
	}

	private void OnStatChanged(CharacterStat stat) {
		if (stat.Type == Stat.Value.Type)
			Debug.Log(
				$"Character Stat Updated: {stat.Type}: {stat.Hunger}, {stat.Mental}, {string.Join(",", stat.Effects)}");

		if (!_isShowing) return;
		StatusUIUpdateEvent.Raise(new StatusUIUpdatePayload {
			ShowPanel = true,
			Position = _transform.position + UIOffset,
			Stat = stat
		});
	}

	public void OpenStatusUI() {
		StatusUIUpdateEvent.Raise(new StatusUIUpdatePayload {
			ShowPanel = true,
			Position = _transform.position + UIOffset,
			Stat = Stat.Value
		});
		_isShowing = true;
	}

	public void CloseStatusUI() {
		StatusUIUpdateEvent.Raise(new StatusUIUpdatePayload {
			ShowPanel = false
		});
		_isShowing = false;
	}

	private void Update() {
		if (!_isShowing) return;
		StatusUIUpdateEvent.Raise(new StatusUIUpdatePayload {
			ShowPanel = true,
			Position = _transform.position + UIOffset,
		});
	}

	[Button]
	public void AddEffect(StatusEffect Effect) {
		var effects = Stat.Value.Effects.ToHashSet();
		effects.Add(Effect);
		Stat.Value = Stat.Value with {
			Effects = effects.ToArray()
		};
	}
}
