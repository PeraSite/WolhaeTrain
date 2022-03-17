using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class NextDayManager : MonoBehaviour {
	[Header("이벤트")]
	public VoidEvent NextDayEvent;

	[Header("변수")]
	public IntVariable DayVariable;
	public IntVariable FuelVariable;
	public IntVariable CleanVariable;

	[Header("일일 자원 감소 수치")]
	[SuffixLabel("Per day", true)]
	public int FuelDecreaseAmount = 10;

	[SuffixLabel("Per day", true)]
	public int CleanDecreaseAmount = 5;

	private void OnEnable() {
		NextDayEvent.Register(OnNextDay);
	}

	private void OnDisable() {
		NextDayEvent.Unregister(OnNextDay);
	}

	private void OnNextDay() {
		DayVariable.Add(1);
		FuelVariable.Value = Mathf.Clamp(FuelVariable.Value - FuelDecreaseAmount, 0, 100);
		CleanVariable.Value = Mathf.Clamp(CleanVariable.Value - CleanDecreaseAmount, 0, 100);
	}
}
