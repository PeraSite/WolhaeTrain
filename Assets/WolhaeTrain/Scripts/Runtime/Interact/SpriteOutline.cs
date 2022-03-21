using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOutline : MonoBehaviour {
	public bool ActivateOnEnable;

	private SpriteRenderer _spriteRenderer;

	private static readonly int OutlinePixelWidth = Shader.PropertyToID("_OutlinePixelWidth");
	private static readonly int OutlineAlpha = Shader.PropertyToID("_OutlineAlpha");

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnEnable() {
		if (ActivateOnEnable) ActivateOutline();
	}

	private void OnDisable() {
		DeactivateOutline();
	}

	[ButtonGroup]
	public void ActivateOutline() {
		_spriteRenderer.material.SetFloat(OutlinePixelWidth, 1.0f);
		_spriteRenderer.material.SetFloat(OutlineAlpha, 1.0f);
	}

	[ButtonGroup]
	public void DeactivateOutline() {
		_spriteRenderer.material.SetFloat(OutlinePixelWidth, 0.0f);
		_spriteRenderer.material.SetFloat(OutlineAlpha, 0.0f);
	}
}
