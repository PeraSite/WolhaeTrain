using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour {
	[Header("이벤트")]
	public VoidEvent GenerateNewQuestEvent;

	public IntPairEvent QuestSelectedEvent;

	public QuestEvent MakeQuestActiveEvent;
	public QuestEvent MakeQuestClearEvent;

	[Header("목록")]
	public QuestValueList QuestDatabase;

	public QuestValueList ActiveQuests;
	public QuestValueList ClearedQuest;

	[Header("확률")]
	[SuffixLabel("%", true)]
	public int MaxNormalQuestAmount = 2;

	public int StoryQuestProbability = 40;

	private void OnEnable() {
		GenerateNewQuestEvent.Register(GenerateNewQuest);
		MakeQuestActiveEvent.Register(MakeQuestActive);
		MakeQuestClearEvent.Register(MakeQuestClear);
		QuestSelectedEvent.Register(OnQuestSelected);

		Debug.Log("QuestDatabase" + string.Join(",", QuestDatabase.Select(q => q.Title)));
		Debug.Log("Active Quests" + string.Join(",", ActiveQuests.Select(q => q.Title)));
		if (ActiveQuests.Count == 0)
			GenerateNewQuest();
	}

	private void OnDisable() {
		GenerateNewQuestEvent.Unregister(GenerateNewQuest);
		MakeQuestActiveEvent.Unregister(MakeQuestActive);
		MakeQuestClearEvent.Unregister(MakeQuestClear);
		QuestSelectedEvent.Unregister(OnQuestSelected);
	}

	[Button]
	public void GenerateNewQuest() {
		var possible = QuestDatabase.Where(
			q =>
				!IsActiveQuest(q) && !IsClearedQuest(q) && q.CheckConditions() &&
				Random.Range(0, 100) <= q.SpawnProbability
		).ToList();

		for (var i = 0; i < Random.Range(1, MaxNormalQuestAmount + 1); i++) {
			var normal = possible.Where(q => !q.IsStory).RandomOrNull();
			if (normal.Title != null) {
				MakeQuestActive(normal);
			}
		}

		if (Random.Range(0, 100) <= StoryQuestProbability) {
			var story = possible.Where(q => q.IsStory).RandomOrNull();
			if (story.Title != null)
				MakeQuestActive(story);
		}
	}

	public void ClearRandom() {
		var quest = ActiveQuests.RandomOrNull();
		if (quest.Title == null) return;

		MakeQuestClear(quest);
	}

	private void OnQuestSelected(IntPair pair) {
		var (questID, selectIndex) = pair;
		var quest = QuestDatabase.First(q => q.ID == questID);
		if (quest.Title == null) return;
		if (!IsActiveQuest(quest)) return;
		Debug.Log("User select " + quest.Title + " to " + selectIndex);
		MakeQuestClear(quest);
	}

	public void MakeQuestActive(Quest quest) {
		if (IsActiveQuest(quest)) return;
		if (IsClearedQuest(quest)) return;
		Debug.Log("New active quest:" + quest.Title);
		ActiveQuests.Add(quest);
	}

	public void MakeQuestClear(Quest quest) {
		if (IsClearedQuest(quest)) return;
		Debug.Log("Quest Cleared: " + quest.Title);
		ActiveQuests.Remove(quest);
		ClearedQuest.Add(quest);
	}


	[Button]
	private void MakeQuestActiveConstant(QuestConstant quest) {
		MakeQuestActive(quest.Value);
	}

	[Button]
	private void MakeQuestClearConstant(QuestConstant quest) {
		MakeQuestClear(quest.Value);
	}

	private bool IsActiveQuest(Quest quest) {
		return ActiveQuests.Contains(quest);
	}

	private bool IsClearedQuest(Quest quest) {
		return ClearedQuest.Contains(quest);
	}
}
