using Sirenix.OdinInspector;
using UnityEngine.Events;

public class Interactable : SerializedMonoBehaviour {
	public UnityEvent MouseEnter;
	public UnityEvent MouseExit;
	public UnityEvent MouseClick;

	public bool IsHovered { get; private set; }

	public void OnMouseEnter() {
		if (IsHovered) return;

		MouseEnter.Invoke();
		IsHovered = true;
	}

	public void OnMouseExit() {
		MouseExit.Invoke();
		IsHovered = false;
	}

	public void OnMouseDown() {
		MouseClick.Invoke();
	}
}
