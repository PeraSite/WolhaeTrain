using System.Linq;
using Aarthificial.Reanimation;
using Sirenix.Utilities;
using UnityEngine;

public class AIController : MonoBehaviour {
	public float MoveSpeed = 1f;
	public float CheckDistance = 1f;
	public float StateUpdateTime = 3f;
	public LayerMask CheckMask;

	private float _timer;
	private int _moveInput;

	private Collider2D _collider;
	private Reanimator _reanimator;
	private Rigidbody2D _rigidbody;

	private void Awake() {
		_collider = GetComponent<Collider2D>();
		_reanimator = GetComponent<Reanimator>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

	private void Start() {
		_timer = Random.Range(0, StateUpdateTime);
	}

	private void Update() {
		_timer += Time.deltaTime;
		if (_timer >= StateUpdateTime) {
			_timer = Random.Range(0, StateUpdateTime);
			UpdateState();
		}
		UpdateAnimation();
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
				_reanimator.Flip = false;
				break;
			case < 0:
				_reanimator.Set("isMoving", 1);
				_reanimator.Flip = true;
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


	private RaycastHit2D[] hits = new RaycastHit2D[5];

	private bool CanGo() {
		var center = _collider.bounds.center;
		var flipX = _moveInput < 0;
		var target = center + new Vector3(flipX ? -CheckDistance : CheckDistance, 0, 0);
		Physics2D.LinecastNonAlloc(center, target, hits, CheckMask);

		foreach (var col in hits.Select(h => h.collider)) {
			if (col.SafeIsUnityNull()) continue;
			if (col == _collider) continue;
			Debug.Log($"{gameObject.name} hits {col.gameObject.name}");
			return false;
		}
		return true;
	}
}
