using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PeraCore.Runtime;
using PixelCrushers;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class DayEndUI : MonoBehaviour {
	[Header("이벤트")]
	public VoidEvent EndDayEvent;

	public CharacterStatEvent ExploreSelectedEvent;

	public VoidEvent NextDayEvent;

	[Header("퀘스트")]
	public QuestDatabase QuestDatabase;

	public IntPairEvent QuestSelectedEvent;

	[Header("UI")]
	public UIPanel Panel;

	public TextMeshProUGUI Summary;

	[Header("캐릭터")]
	public List<CharacterStatVariable> Characters;

	public List<TextMeshProUGUI> EffectText;


	[Header("아이템")]
	public UsableResourceValueList UsableList;

	private bool _hasExplored;
	private CharacterStat _lastExplore;

	[Header("스탯 변수")]
	public IntVariable FuelVariable;

	public IntVariable CleanVariable;

	private void OnEnable() {
		EndDayEvent.Register(OnEndDayRequest);
		QuestSelectedEvent.Register(OnQuestSelected);
		ExploreSelectedEvent.Register(OnExploreSelected);
	}

	private void OnDisable() {
		EndDayEvent.Unregister(OnEndDayRequest);
		QuestSelectedEvent.Unregister(OnQuestSelected);
		ExploreSelectedEvent.Unregister(OnExploreSelected);
	}

	private void OnExploreSelected(CharacterStat obj) {
		_lastExplore = obj;
		_hasExplored = true;
	}

	private void OnQuestSelected(IntPair pair) {
		var (questID, selectedIndex) = pair;
		var quest = QuestDatabase.FirstOrDefault(q => q.Value.ID == questID);
		if (quest == null) return;
		Summary.text += quest.Value.Selections[selectedIndex].ResultText + "\n";
	}


	private void OnEndDayRequest() {
		if (_hasExplored) {
			var percent = _lastExplore.Type is CharacterType.Dad or CharacterType.Son ? 30 : 20;
			if (Random.Range(0, 100) <= percent) { //탐험 성공
				var usable = UsableList.Random();
				var text = "";

				text += usable.Fuel > 0 ? $"연료 {usable.Fuel}" : "";
				text += usable.Clean > 0 ? $"청결 {usable.Clean}" : "";
				text += usable.Hunger > 0 ? $"모든 가족 배부름 {usable.Hunger}" : "";
				text += usable.Mental > 0 ? $"모든 가족 멘탈 {usable.Mental}" : "";

				if (usable.Fuel > 0) FuelVariable.Value += usable.Fuel;
				if (usable.Clean > 0) CleanVariable.Value += usable.Clean;
				if (usable.Hunger > 0)
					Characters.ForEach(statVar => {
						statVar.Value = statVar.Value with {
							Hunger = statVar.Value.Hunger + usable.Hunger
						};
					});

				if (usable.Mental > 0)
					Characters.ForEach(statVar => {
						statVar.Value = statVar.Value with {
							Mental = statVar.Value.Mental + usable.Mental
						};
					});

				Summary.text += $"{_lastExplore.Type.GetName()}이(가) 탐험을 떠나서 {usable.Name}을(를) 발견했어! {text} 증가";
			} else {
				Summary.text += $"{_lastExplore.Type.GetName()}이(가) 탐험을 떠났지만 아무 것도 얻지 못했어.\n";
			}
		} else {
			Summary.text += "아무도 탐험을 가지 않았어.\n";
		}
		Characters.ForEach((stat, index) => {
			var effectText = EffectText[index];
			effectText.text = string.Join("\n", stat.Value.Effects.Select(e => e.GetName()));
		});
		Panel.Open();
	}

	public void InvokeNextDayEvent() {
		InvokeNextDayEventAsync().Forget();
	}

	private async UniTaskVoid InvokeNextDayEventAsync() {
		await SaveSystem.sceneTransitionManager.LeaveScene();
		_hasExplored = false;
		Panel.Close();
		NextDayEvent.Raise();
		Summary.text = "";
		await SaveSystem.sceneTransitionManager.EnterScene();
	}
}
