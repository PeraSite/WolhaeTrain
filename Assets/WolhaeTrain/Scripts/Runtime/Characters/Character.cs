using System.Linq;
using Aarthificial.Reanimation;
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

	private Reanimator _reanimator;
	private bool _isShowing;
	private Transform _transform;

	private void Awake() {
		_transform = GetComponent<Transform>();
		_reanimator = GetComponent<Reanimator>();
	}

	private void OnEnable() {
		CharacterStatChangedEvent.Register(OnStatChanged);
	}

	private void OnDisable() {
		CharacterStatChangedEvent.Unregister(OnStatChanged);
	}

	private void OnStatChanged(CharacterStat stat) {
		if (!_isShowing) return;
		StatusUIUpdateEvent.Raise(new StatusUIUpdatePayload {
			ShowPanel = true,
			Position = _transform.position + UIOffset,
			Stat = stat
		});
	}

	private enum EffectStates {
		NORMAL = 0,
		DIRTY = 1,
		HURT = 2,
		SICK = 3
	}

	private void UpdateAnimation() {
		var stat = Stat.Value;
		if (stat.Effects.Contains(StatusEffect.Infect)
		    || stat.Effects.Contains(StatusEffect.Hurt)) {
			_reanimator.Set("effects", (int) EffectStates.HURT);
		} else if (stat.Effects.Contains(StatusEffect.Crazy)) {
			_reanimator.Set("effects", (int) EffectStates.DIRTY);
		} else if (stat.Effects.Contains(StatusEffect.Cold)
		           || stat.Effects.Contains(StatusEffect.Exhaust)) {
			_reanimator.Set("effects", (int) EffectStates.SICK);
		} else {
			_reanimator.Set("effects", 0);
		}
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
		UpdateAnimation();
		UpdateStatusUIPosition();
	}

	private void UpdateStatusUIPosition() {
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
