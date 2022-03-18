using System;
using TMPro;
using UnityAtoms;
using UnityEngine;

public class IngameUI : MonoBehaviour {
	public QuestValueList QuestDatabase;

	public QuestEvent ActiveQuestAddEvent;
	public QuestEvent ActiveQuestRemoveEvent;

	public TextMeshProUGUI QuestText;

	public void OnEnable() {
		QuestText.text = "";

		ActiveQuestAddEvent.Register(OnActiveQuestAdd);
		ActiveQuestRemoveEvent.Register(OnActiveQuestRemove);
	}

	public void OnDisable() {
		ActiveQuestAddEvent.Unregister(OnActiveQuestAdd);
		ActiveQuestRemoveEvent.Unregister(OnActiveQuestRemove);
	}


	private void OnActiveQuestAdd(Quest quest) {
		QuestText.text += quest.Title + "\n";
	}

	private void OnActiveQuestRemove(Quest quest) {
		QuestText.text = QuestText.text.Replace(quest.Title + "\n", "");
	}
}
