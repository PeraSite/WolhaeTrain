using UnityEngine;

public class MainMenuUI : MonoBehaviour {
	public void QuitGame() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}
}
