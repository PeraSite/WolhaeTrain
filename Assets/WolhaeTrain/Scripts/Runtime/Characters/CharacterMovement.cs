using Aarthificial.Reanimation;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
	public float MoveSpeed = 1f;
	public float StateUpdateTime = 3f;
	public bool FlipSprite;

	public Transform Sprite;
	public Vector2 DefaultSpriteOffset;
	public Vector2 MovingSpriteOffset;

	private float _timer;
	private int _moveInput;

	private Reanimator _reanimator;
	private Rigidbody2D _rigidbody;

	private void Awake() {
		_reanimator = GetComponent<Reanimator>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start() {
		_timer = Random.Range(0, StateUpdateTime);
	}

	private void Update() {
		_timer += Time.deltaTime;
		if (_timer >= StateUpdateTime) {
			_timer = 0;
			UpdateState();
		}
		UpdateAnimation();
		UpdateOffset();
	}

	private void UpdateOffset() {
		Sprite.localPosition = _moveInput != 0 ? MovingSpriteOffset : DefaultSpriteOffset;
	}

	private void FixedUpdate() {
		Move();
	}

	private void UpdateAnimation() {
		switch (_moveInput) {
			case 0:
				_reanimator.Set("isMoving", 0);
				break;
			case > 0:
				_reanimator.Set("isMoving", 1);
				_reanimator.Flip = FlipSprite;
				break;
			case < 0:
				_reanimator.Set("isMoving", 1);
				_reanimator.Flip = !FlipSprite;
				break;
		}
	}

	private void Move() {
		_rigidbody.MovePosition(_rigidbody.position + new Vector2(_moveInput, 0) * MoveSpeed * Time.fixedDeltaTime);
	}

	private void UpdateState() {
		var shouldMove = Random.value > 0.5f;
		if (shouldMove) {
			var left = Random.value > 0.5f;
			_moveInput = left ? -1 : 1;
		} else {
			_moveInput = 0;
		}
	}
}
