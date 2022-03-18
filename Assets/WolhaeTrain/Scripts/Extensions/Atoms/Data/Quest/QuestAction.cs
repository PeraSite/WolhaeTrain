using UnityEngine;

public interface IQuestAction {
	void Execute();
}

public class LogQuestAction : IQuestAction {
	public string Message;

	public void Execute() {
		Debug.Log(Message);
	}
}
