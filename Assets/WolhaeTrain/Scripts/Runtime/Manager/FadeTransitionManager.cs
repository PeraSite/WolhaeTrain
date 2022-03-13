using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using PixelCrushers;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class FadeTransitionManager : SceneTransitionManager {
	public CanvasGroup Fade;
	public float AnimationTime = 0.3f;

	public BoolVariable IsFading;

	public override IEnumerator LeaveScene() => UniTask.ToCoroutine(ShowFade);

	public override IEnumerator EnterScene() => UniTask.ToCoroutine(HideFade);

	private async UniTask ShowFade() {
		IsFading.SetValue(true);
		Fade.gameObject.SetActive(true);
		Fade.alpha = 0f;
		await Fade.DOFade(1f, AnimationTime).AsyncWaitForCompletion();
		IsFading.SetValue(false);
	}

	private async UniTask HideFade() {
		IsFading.SetValue(true);
		await Fade.DOFade(0f, AnimationTime).AsyncWaitForCompletion();
		Fade.gameObject.SetActive(false);
		IsFading.SetValue(false);
	}
}
