using System.Collections.Generic;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Dog : MonoBehaviour {
	public GameObject Self;
	public BoolEvent HasDogChanged;
	public VoidEvent NextDayEvent;
	public IntVariable Day;
	public List<CharacterStatVariable> Characters;

	public int MentalAmount = 10;

	private bool _hasDog;

	private void OnEnable() {
		HasDogChanged.Register(OnHasDogChanged);
		NextDayEvent.Register(OnNextDay);
	}

	private void OnDisable() {
		HasDogChanged.Unregister(OnHasDogChanged);
		NextDayEvent.Unregister(OnNextDay);
	}

	private void OnNextDay() {
		if (!_hasDog) return;
		if (Day.Value % 2 == 0) {
			foreach (var character in Characters) {
				character.Value = character.Value with {
					Mental = character.Value.Mental + MentalAmount
				};
			}
		}
	}

	private void OnHasDogChanged(bool hasDog) {
		Self.SetActive(hasDog);
		_hasDog = hasDog;
	}
}
