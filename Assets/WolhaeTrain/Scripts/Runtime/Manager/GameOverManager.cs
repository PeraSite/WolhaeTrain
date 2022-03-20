using PixelCrushers;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	[Header("엔딩")]
	public EndingDataEvent EndingEvent;

	public EndingDataVariable EndingVariable;

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
		EndingEvent.Register(OnEndingRequest);
	}

	private void OnEndingRequest(EndingData data) {
		Debug.Log($"Ending request:" + data.Title);
		SceneManager.LoadScene(EndingScene);
		EndingVariable.Value = data;
	}

	private void OnDisable() {
		FuelChangedEvent.Unregister(OnFuelChanged);
		CleanChangedEvent.Unregister(OnCleanChanged);
		EndingEvent.Unregister(OnEndingRequest);
	}

	private void OnFuelChanged(int newFuel) {
		if (newFuel <= 0) {
			EndingEvent.Raise(FuelEnding.Value);
		}
	}

	private void OnCleanChanged(int newClean) {
		if (newClean <= 0) {
			EndingEvent.Raise(CleanEnding.Value);
		}
	}
}
