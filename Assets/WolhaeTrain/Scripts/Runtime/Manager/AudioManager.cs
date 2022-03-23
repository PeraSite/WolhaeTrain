using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource Source;

	[Header("Clips")]
	public AudioClip Click;

	private void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Source.PlayOneShot(Click, 1);
		}
	}
}
