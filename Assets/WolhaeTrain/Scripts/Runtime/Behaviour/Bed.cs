using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Bed : MonoBehaviour {

	public QuestValueList ActiveQuest;

	public VoidEvent EndDayEvent;

	public StringEvent AlertEvent;

	public void TryEndDay() {
		if (ActiveQuest.Count > 0) {
			AlertEvent.Raise("퀘스트를 다 깨야합니다.");
			return;
		}

		EndDayEvent.Raise();
	}
}
