using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageOutline : MonoBehaviour {
	public bool ActivateOnEnable;

	private Image _image;

	private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
	private static readonly int OutlineThicknessID = Shader.PropertyToID("_OutlineThickness");

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
		_image.material.EnableKeyword("OUTBASE_ON");
	}

	[ButtonGroup]
	public void DeactivateOutline() {
		_image.material.DisableKeyword("OUTBASE_ON");
	}
}
