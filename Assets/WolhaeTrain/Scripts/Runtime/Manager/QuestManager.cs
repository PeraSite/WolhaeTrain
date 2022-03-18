using System.Linq;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

public class QuestManager : MonoBehaviour {
	public QuestValueList QuestDatabase;
	public QuestValueList ActiveQuests;
	public QuestValueList ClearedQuest;

	[Button]
	public void GetNewQuest() {
		var possible = QuestDatabase.Where(q => !IsActiveQuest(q) && !IsClearedQuest(q) && q.CheckConditions());
		Debug.Log(string.Join(",", possible.Select(q => q.Title)));
	}

	public void MakeActiveQuest(Quest quest) {
		if (IsActiveQuest(quest)) return;
		if (IsClearedQuest(quest)) return;

		ActiveQuests.Add(quest);
	}

	public void MakeClearQuest(Quest quest) {
		if (IsClearedQuest(quest)) return;

		ActiveQuests.Remove(quest);
		ClearedQuest.Add(quest);
	}


	[Button]
	private void MakeActiveQuestConstant(QuestConstant quest) {
		MakeActiveQuest(quest.Value);
	}

	[Button]
	private void MakeClearQuestConstant(QuestConstant quest) {
		MakeClearQuest(quest.Value);
	}

	private bool IsActiveQuest(Quest quest) {
		return ActiveQuests.Contains(quest);
	}

	private bool IsClearedQuest(Quest quest) {
		return ClearedQuest.Contains(quest);
	}
}
