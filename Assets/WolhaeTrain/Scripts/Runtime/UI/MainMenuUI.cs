using PixelCrushers;
using UnityEditor;
using UnityEngine;

public class MainMenuUI : MonoBehaviour {

	public GameObject ContinueButton;

	public void QuitGame() {
#if UNITY_EDITOR
		EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	private void Start() {
		CheckCanContinue();
	}

	private void CheckCanContinue() {
		if (!SaveSystem.HasSavedGameInSlot(1)) {
			ContinueButton.SetActive(false);
		}
	}
}
