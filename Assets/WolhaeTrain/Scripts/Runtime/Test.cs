using UnityAtoms;
using UnityEngine;

public class Test : MonoBehaviour {
	public int DecreaseTime = 1;
	public int DecreaseAmount = 5;

	public CharacterStatVariable Stat;


	private float _timer;

	private void Update() {
		_timer += Time.deltaTime;
		if (_timer > DecreaseTime) {
			_timer = 0f;
			Tick();
		}
	}

	private void Tick() {
		Stat.Value = Stat.Value with {Hunger = Stat.Value.Hunger - DecreaseAmount};
	}
}
