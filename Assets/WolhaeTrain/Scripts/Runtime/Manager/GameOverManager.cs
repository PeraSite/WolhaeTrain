using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class GameOverManager : MonoBehaviour {
	[Header("엔딩 이벤트")]
	public EndingDataEvent EndingEvent;

	[Header("연료")]
	public IntEvent FuelChangedEvent;

	public EndingData FuelEnding;

	[Header("청결")]
	public IntEvent CleanChangedEvent;

	public EndingData CleanEnding;


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
			EndingEvent.Raise(FuelEnding);
		}
	}

	private void OnCleanChanged(int newClean) {
		if (newClean <= 0) {
			EndingEvent.Raise(CleanEnding);
		}
	}
}
