using UnityEngine;

public class ParallaxElement : MonoBehaviour {
	public float EffectAmount = 1f;
	public float Multiplier = 1f;
	public bool Inverted;

	private Transform _transform;
	private float _startPosition;

	[SerializeField]
	private float _length;

	private void Start() {
		_transform = GetComponent<Transform>();
		_startPosition = _transform.position.x;
	}

	private void Update() {
		var effectAmount = EffectAmount * Multiplier * Time.deltaTime;
		if (Inverted) effectAmount *= -1;

		var newX = _transform.position.x + effectAmount;

		if (newX >= _startPosition + _length || newX <= _startPosition - _length) {
			_transform.position = new Vector3(_startPosition, 0);
		} else {
			_transform.position = new Vector3(newX, 0f, 0f);
		}
	}
}
