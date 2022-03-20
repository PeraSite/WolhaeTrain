using System.Collections.Generic;
using System.Linq;
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


	[Header("퀘스트")]
	public QuestDatabase QuestDatabase;

	public IntPairEvent QuestSelectedEvent;

	public int SaveSlot = 1;

	private List<(Quest, int)> _lastSelection = new();

	private void OnEnable() {
		NextDayEvent.Register(OnNextDay);
		QuestSelectedEvent.Register(OnQuestSelect);
	}

	private void OnQuestSelect(IntPair pair) {
		var (questID, selectedIndex) = pair;
		var quest = QuestDatabase.FirstOrDefault(q => q.Value.ID == questID);
		if (quest == null) return;
		_lastSelection.Add((quest.Value, selectedIndex));
	}

	private void OnDisable() {
		NextDayEvent.Unregister(OnNextDay);
		QuestSelectedEvent.Unregister(OnQuestSelect);
	}

	private void OnNextDay() {
		DayVariable.Add(1);
		FuelVariable.Value = Clamp100(FuelVariable.Value - FuelDecreaseAmount);
		CleanVariable.Value = Clamp100(CleanVariable.Value - CleanDecreaseAmount);
		foreach (var stat in Stats) {
			var willAddedEffects = new HashSet<StatusEffect>(stat.Value.Effects);

			//탈진
			if (stat.Value.Hunger <= 20) {
				if (Random.Range(0, 100) <= 30) {
					willAddedEffects.Add(StatusEffect.Exhaust);
				}
			}

			//감기
			if (CleanVariable.Value <= 20) {
				if (Random.Range(0, 100) <= 30) {
					willAddedEffects.Add(StatusEffect.Cold);
				}
			}

			//감염
			if (CleanVariable.Value <= 10 && stat.Value.Mental <= 10) {
				if (Random.Range(0, 100) <= 5) {
					willAddedEffects.Add(StatusEffect.Infect);
				}
			}

			//미침
			if (stat.Value.Mental <= 10) {
				if (Random.Range(0, 100) <= 20) {
					willAddedEffects.Add(StatusEffect.Crazy);
				}
			}

			stat.Value = stat.Value with {
				Type = stat.Value.Type,
				Hunger = Clamp100(stat.Value.Hunger - HungerDecreaseAmount),
				Mental = Clamp100(stat.Value.Mental - MentalDecreaseAmount),
				Effects = willAddedEffects.ToArray()
			};
		}
		GenerateNewQuestEvent.Raise();

		foreach (var pair in _lastSelection) {
			var (quest, selectedIndex) = pair;
			var selection = quest.Selections[selectedIndex];
			FuelVariable.Subtract(selection.Fuel);
			CleanVariable.Subtract(selection.Clean);

			var stat = Stats[(int) quest.Talker - 1];
			stat.Value = stat.Value with {
				Hunger = Clamp100(stat.Value.Hunger - selection.Hunger),
				Mental = Clamp100(stat.Value.Mental - selection.Mental),
			};

			selection.Actions.ForEach(a => a.Execute());
		}
		_lastSelection.Clear();

		SaveSystem.SaveToSlot(SaveSlot);
	}

	private static int Clamp100(int value) => Mathf.Clamp(value, 0, 100);
}
