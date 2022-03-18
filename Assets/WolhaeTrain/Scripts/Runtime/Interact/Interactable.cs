using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interactable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public UnityEvent MouseEnter;
	public UnityEvent MouseExit;
	public UnityEvent MouseClick;

	public bool CheckUnityEvent;

	private bool _isHovered;

	public void OnPointerEnter(PointerEventData eventData) {
		if (!CheckUnityEvent) return;

		if (_isHovered)
			return;
		MouseEnter.Invoke();
		_isHovered = true;
	}

	public void OnPointerExit(PointerEventData eventData) {
		if (!CheckUnityEvent) return;

		MouseExit.Invoke();
		_isHovered = false;
	}

	public void OnPointerClick(PointerEventData eventData) {
		if (!CheckUnityEvent) return;

		MouseClick.Invoke();
	}
}
