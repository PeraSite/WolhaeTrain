using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class AlertUI : MonoBehaviour {
	[Header("설정")]
	public float AnimationTime = 0.3f;
	public float AlertShowTime = 1f;

	public float ShowY = 30;
	public float HideY = -90;

	[Header("이벤트")]
	public StringEvent AlertEvent;

	[Header("UI")]
	public RectTransform Panel;

	public TextMeshProUGUI AlertText;

	private bool _isShowing;

	private void OnEnable() {
		AlertEvent.Register(OnAlert);
	}

	private void OnDisable() {
		AlertEvent.Unregister(OnAlert);
	}

	private void OnAlert(string msg) {
		if (_isShowing) {
			Panel.DOKill(true);
		}
		var sequence = DOTween.Sequence(Panel);
		AlertText.text = msg;
		sequence.AppendCallback(() => _isShowing = true);
		sequence.Append(Panel.DOAnchorPosY(ShowY, AnimationTime));
		sequence.AppendInterval(AlertShowTime);
		sequence.Append(Panel.DOAnchorPosY(HideY, AnimationTime));
		sequence.AppendCallback(() => _isShowing = false);
	}
}
