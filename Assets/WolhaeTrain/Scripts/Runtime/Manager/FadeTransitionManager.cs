using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PixelCrushers;
using Sirenix.OdinInspector;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class FadeTransitionManager : SceneTransitionManager {
	public CanvasGroup Fade;

	[SuffixLabel("Seconds", true)]
	public float AnimationTime = 0.3f;

	[SuffixLabel("Seconds", true)]
	public float BlackTime;

	public BoolVariable IsFading;

	public override IEnumerator LeaveScene() => UniTask.ToCoroutine(() => ShowFade(AnimationTime));

	public override IEnumerator EnterScene() => UniTask.ToCoroutine(() => HideFade(AnimationTime));

	private async UniTask ShowFade(float animationTime) {
		IsFading.SetValue(true);
		Fade.gameObject.SetActive(true);
		Fade.alpha = 0f;
		await Fade.DOFade(1f, animationTime).AsyncWaitForCompletion();
		IsFading.SetValue(false);
	}

	private async UniTask HideFade(float animationTime) {
		if (BlackTime > 0f)
			await UniTask.Delay(TimeSpan.FromSeconds(BlackTime));
		IsFading.SetValue(true);
		await Fade.DOFade(0f, animationTime).AsyncWaitForCompletion();
		Fade.gameObject.SetActive(false);
		IsFading.SetValue(false);
	}
}
