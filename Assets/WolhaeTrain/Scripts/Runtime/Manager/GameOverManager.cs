using PixelCrushers;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	[Header("엔딩")]
	public EndingDataEvent EndingEvent;

	public string EndingScene;

	[Header("연료")]
	public IntEvent FuelChangedEvent;

	public EndingDataConstant FuelEnding;

	[Header("청결")]
	public IntEvent CleanChangedEvent;

	public EndingDataConstant CleanEnding;


	public void OnEnable() {
		FuelChangedEvent.Register(OnFuelChanged);
		CleanChangedEvent.Register(OnCleanChanged);
	}

	private void OnDisable() {
		FuelChangedEvent.Unregister(OnFuelChanged);
		CleanChangedEvent.Unregister(OnCleanChanged);
	}

	private void OnFuelChanged(int newFuel) {
		if (newFuel <= 0) {
			SceneManager.LoadScene(EndingScene);
			EndingEvent.Raise(FuelEnding.Value);
		}
	}

	private void OnCleanChanged(int newClean) {
		if (newClean <= 0) {
			SceneManager.LoadScene(EndingScene);
			EndingEvent.Raise(CleanEnding.Value);
		}
	}
}
