using Cysharp.Threading.Tasks;
using PixelCrushers;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class NextDayManager : MonoBehaviour {
	public IntVariable DayVariable;
	public VoidEvent NextDayEvent;

	private void OnEnable() {
		NextDayEvent.Register(OnNextDay);
	}

	private void OnDisable() {
		NextDayEvent.Unregister(OnNextDay);
	}

	private void OnNextDay() {
		DayVariable.Add(1);
	}

}
