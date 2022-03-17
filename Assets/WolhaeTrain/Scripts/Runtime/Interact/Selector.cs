using Sirenix.Utilities;
using UnityEngine;

public class Selector : MonoBehaviour {
	private Interactable _lastInteractable;
	private Camera _cam;

	private void Start() {
		_cam = Camera.main;
	}

	private void Update() {
		var hit = Physics2D.GetRayIntersection(_cam.ScreenPointToRay(Input.mousePosition));

		if (hit.collider.SafeIsUnityNull()) {
			if (!_lastInteractable.SafeIsUnityNull()) {
				ExitLastInteractable();
			}
		} else {
			if (hit.collider.TryGetComponent<Interactable>(out var interactable)) {
				if (!_lastInteractable.SafeIsUnityNull()) {
					ExitLastInteractable();
				}
				EnterInteractable(interactable);
			}
		}

		if (!_lastInteractable.SafeIsUnityNull() && Input.GetMouseButtonDown(0)) {
			_lastInteractable.MouseClick.Invoke();
		}
	}

	private void EnterInteractable(Interactable interactable) {
		interactable.MouseEnter.Invoke();
		_lastInteractable = interactable;
	}

	private void ExitLastInteractable() {
		_lastInteractable.MouseExit.Invoke();
		_lastInteractable = null;
	}
}
