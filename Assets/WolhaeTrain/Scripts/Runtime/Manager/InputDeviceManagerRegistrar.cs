using PixelCrushers;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDeviceManagerRegistrar : MonoBehaviour {
	private static bool isRegistered;

	private PlayerControl _playerControl;

	private void Awake() {
		_playerControl = new PlayerControl();
	}

	private void OnEnable() {
		if (!isRegistered) {
			isRegistered = true;
			_playerControl.Enable();
			foreach (var inputAction in _playerControl) {
				InputDeviceManager.RegisterInputAction(inputAction.name, inputAction);
			}
		}
	}

	private void OnDisable() {
		if (isRegistered) {
			isRegistered = false;
			_playerControl.Disable();
			foreach (var inputAction in _playerControl) {
				InputDeviceManager.UnregisterInputAction(inputAction.name);
			}
		}
	}
}
