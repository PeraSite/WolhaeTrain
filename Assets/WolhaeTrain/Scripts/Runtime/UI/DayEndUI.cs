using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using PixelCrushers;
using Sirenix.Utilities;
using TMPro;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class DayEndUI : MonoBehaviour {
	[Header("이벤트")]
	public VoidEvent EndDayEvent;

	public VoidEvent NextDayEvent;

	[Header("퀘스트")]
	public QuestValueList QuestDatabase;

	public IntPairEvent QuestSelectedEvent;

	[Header("UI")]
	public UIPanel Panel;

	public TextMeshProUGUI Summary;

	[Header("캐릭터")]
	public List<CharacterStatVariable> Characters;

	public List<TextMeshProUGUI> EffectText;

	private void OnEnable() {
		EndDayEvent.Register(OnDayEnd);
		QuestSelectedEvent.Register(OnQuestSelected);
	}

	private void OnQuestSelected(IntPair pair) {
		var (questID, selectedIndex) = pair;
		var quest = QuestDatabase.FirstOrDefault(q => q.ID == questID);
		if (quest.Title == null) return;
		Summary.text += quest.Selections[selectedIndex].ResultText + "\n";
	}

	private void OnDisable() {
		EndDayEvent.Unregister(OnDayEnd);
	}

	private void OnDayEnd() {
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
		Panel.Close();
		NextDayEvent.Raise();
		Summary.text = "";
		await SaveSystem.sceneTransitionManager.EnterScene();
	}
}
