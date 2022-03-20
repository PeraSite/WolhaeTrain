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
	public QuestDatabase QuestDatabase;

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

		Debug.Log("QuestDatabase" + string.Join(",", QuestDatabase.Select(q => q.Value.Title)));
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
				!IsActiveQuest(q)
				&& !IsClearedQuest(q)
				&& q.Value.CheckConditions()
				&& Random.Range(0, 100) <= q.Value.SpawnProbability
		).ToList();

		for (var i = 0; i < Random.Range(1, MaxNormalQuestAmount + 1); i++) {
			var normal = possible.Where(q => !q.Value.IsStory).RandomOrNull();
			if (normal != null) {
				MakeQuestActive(normal.Value);
			}
		}

		if (Random.Range(0, 100) <= StoryQuestProbability) {
			var story = possible.Where(q => q.Value.IsStory).RandomOrNull();
			if (story != null)
				MakeQuestActive(story.Value);
		}
	}

	private void OnQuestSelected(IntPair pair) {
		var (questID, selectIndex) = pair;
		var quest = QuestDatabase.FirstOrDefault(q => q.Value.ID == questID);
		if (quest == null) return;
		if (!IsActiveQuest(quest)) return;
		Debug.Log("User select " + quest.Value.Title + " to " + selectIndex);
		MakeQuestClear(quest.Value);
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


	private bool IsActiveQuest(QuestConstant quest) {
		return ActiveQuests.Contains(quest.Value);
	}

	private bool IsClearedQuest(QuestConstant quest) {
		return ClearedQuest.Contains(quest.Value);
	}
}
