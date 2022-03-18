using System.Collections.Generic;
using PixelCrushers;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class NextDayManager : SerializedMonoBehaviour {
	[Header("이벤트")]
	public VoidEvent NextDayEvent;

	public VoidEvent GenerateNewQuestEvent;

	[Header("변수")]
	public IntVariable DayVariable;

	public IntVariable FuelVariable;
	public IntVariable CleanVariable;

	[Header("캐릭터")]
	public List<CharacterStatVariable> Stats = new();

	[Header("일일 자원 감소 수치")]
	[SuffixLabel("Per day", true)]
	public int FuelDecreaseAmount = 10;

	[SuffixLabel("Per day", true)]
	public int CleanDecreaseAmount = 5;

	[SuffixLabel("Per day", true)]
	public int HungerDecreaseAmount = 5;

	[SuffixLabel("Per day", true)]
	public int MentalDecreaseAmount = 5;

	public int SaveSlot = 1;

	private void OnEnable() {
		NextDayEvent.Register(OnNextDay);
	}

	private void OnDisable() {
		NextDayEvent.Unregister(OnNextDay);
	}

	private void OnNextDay() {
		DayVariable.Add(1);
		FuelVariable.Value = Clamp100(FuelVariable.Value - FuelDecreaseAmount);
		CleanVariable.Value = Clamp100(CleanVariable.Value - CleanDecreaseAmount);
		foreach (var stat in Stats) {
			stat.Value = stat.Value with {
				Hunger = Clamp100(stat.Value.Hunger - HungerDecreaseAmount),
				Mental = Clamp100(stat.Value.Mental - MentalDecreaseAmount),
			};
		}
		GenerateNewQuestEvent.Raise();
		SaveSystem.SaveToSlot(SaveSlot);
	}

	private static int Clamp100(int value) => Mathf.Clamp(value, 0, 100);
}
