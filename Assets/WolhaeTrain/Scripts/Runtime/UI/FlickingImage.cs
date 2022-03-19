using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class FlickingImage : MonoBehaviour {
	private Image _image;

	[MinMaxSlider(0, 10)]
	public Vector2 WaitTime;

	public float AnimationTime;

	private float _flickTimer = 10;

	private void Awake() {
		_image = GetComponent<Image>();
		_flickTimer = RandomWaitTime();
	}

	private void OnDisable() {
		_image.DOKill();
	}

	private void Update() {
		_flickTimer -= Time.deltaTime;
		if (_flickTimer <= 0) {
			Tick();
			_flickTimer = RandomWaitTime();
		}
	}

	private void Tick() {
		var seq = DOTween.Sequence(_image);
		seq.Append(_image.DOFade(0f, AnimationTime));
		seq.Append(_image.DOFade(1f, AnimationTime));
	}

	private float RandomWaitTime() => Random.Range(WaitTime.x, WaitTime.y);
}
