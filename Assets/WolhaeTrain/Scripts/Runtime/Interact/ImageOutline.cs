using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageOutline : MonoBehaviour {
	public bool ActivateOnEnable;

	private Image _image;

	private static readonly int OutlinePixelWidth = Shader.PropertyToID("_OutlinePixelWidth");

	private void Awake() {
		_image = GetComponent<Image>();
	}

	private void OnEnable() {
		if (ActivateOnEnable) ActivateOutline();
	}

	private void OnDisable() {
		DeactivateOutline();
	}

	[ButtonGroup]
	public void ActivateOutline() {
		_image.material.SetFloat(OutlinePixelWidth, 1.0f);
	}

	[ButtonGroup]
	public void DeactivateOutline() {
		_image.material.SetFloat(OutlinePixelWidth, 0.0f);
	}
}
