using UnityEngine;

public class ParallaxElement : MonoBehaviour {
	public float EffectAmount = 1f;
	public float Multiplier = 1f;
	public bool Inverted;

	private Transform _transform;
	private float _startPosition;
	private float _length;

	void Start() {
		_transform = GetComponent<Transform>();
		_startPosition = _transform.position.x;
		_length = GetComponent<SpriteRenderer>().bounds.size.x;
	}

	void Update() {
		var moveAmount = new Vector3(EffectAmount * Multiplier * Time.deltaTime, 0);
		if (Inverted) moveAmount *= -1;
		_transform.position += moveAmount;

		var newX = _transform.position.x;
		if (newX > _startPosition + _length || newX < _startPosition - _length) {
			_transform.position = new Vector3(_startPosition, 0);
		}
	}
}
