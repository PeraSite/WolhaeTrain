using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Bed : MonoBehaviour {

	public QuestValueList ActiveQuest;

	public VoidEvent EndDayEvent;

	public void TryEndDay() {
		if (ActiveQuest.Count > 0) {
			Debug.Log("퀘스트를 다 깨야합니다.");
			return;
		}

		EndDayEvent.Raise();
	}
}
