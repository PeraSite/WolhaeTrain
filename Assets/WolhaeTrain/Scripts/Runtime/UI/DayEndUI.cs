using Cysharp.Threading.Tasks;
using PixelCrushers;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class DayEndUI : MonoBehaviour {
	[Header("이벤트")]
	public VoidEvent EndDayEvent;
	public VoidEvent NextDayEvent;

	[Header("UI")]
	public UIPanel Panel;

	private void OnEnable() {
		EndDayEvent.Register(OnDayEnd);
	}

	private void OnDisable() {
		EndDayEvent.Unregister(OnDayEnd);
	}

	private void OnDayEnd() {
		Panel.Open();
	}

	public void InvokeNextDayEvent() {
		InvokeNextDayEventAsync().Forget();
	}

	private async UniTaskVoid InvokeNextDayEventAsync() {
		await SaveSystem.sceneTransitionManager.LeaveScene();
		Panel.Close();
		NextDayEvent.Raise();
		await SaveSystem.sceneTransitionManager.EnterScene();
	}
}
