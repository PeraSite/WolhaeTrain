using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteOutline : MonoBehaviour {
	public bool ActivateOnEnable;

	private SpriteRenderer _spriteRenderer;

	private static readonly int OutlineColorID = Shader.PropertyToID("_OutlineColor");
	private static readonly int OutlineThicknessID = Shader.PropertyToID("_OutlineThickness");

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
		_spriteRenderer.material.EnableKeyword("OUTBASE_ON");
	}

	[ButtonGroup]
	public void DeactivateOutline() {
		_spriteRenderer.material.DisableKeyword("OUTBASE_ON");
	}
}
