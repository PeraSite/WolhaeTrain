using Cysharp.Threading.Tasks;
using PixelCrushers;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class DayEndUI : MonoBehaviour {
	public VoidEvent NextDayEvent;

	public void InvokeNextDayEvent() {
		InvokeNextDayEventAsync().Forget();
	}

	private async UniTaskVoid InvokeNextDayEventAsync() {
		await SaveSystem.sceneTransitionManager.LeaveScene();
		NextDayEvent.Raise();
		await SaveSystem.sceneTransitionManager.EnterScene();
	}
}
