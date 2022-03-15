using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShadowCaster2DController : MonoBehaviour {
	private SpriteRenderer _spriteRenderer;
	private ShadowCaster2D _shadowCaster;
	private PolygonCollider2D _polyCollider;

	private static readonly FieldInfo _shapePathField =
		typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);

	private static readonly FieldInfo _shapeHash =
		typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);

	[Button]
	public void UpdateCollider() {
		CheckComponents();
		UpdateShadowFromPoints(_spriteRenderer.sprite.vertices);
	}

	[Button]
	public void UpdateFromCollider() {
		CheckComponents();
		UpdateShadowFromPoints(_polyCollider.points);
	}

	private void CheckComponents() {
		if (_shadowCaster.SafeIsUnityNull()) {
			_spriteRenderer = GetComponent<SpriteRenderer>();
			_shadowCaster = GetComponent<ShadowCaster2D>();
			_polyCollider = GetComponent<PolygonCollider2D>();
		}
	}

	private void UpdateShadowFromPoints(Vector3[] points) {
		// Set the shadow path
		_shapePathField.SetValue(_shadowCaster, points);

		unchecked {
			var hashCode = (int) 2166136261 ^ _shapePathField.GetHashCode();
			hashCode = hashCode * 16777619 ^ (points.GetHashCode());

			_shapeHash.SetValue(_shadowCaster, hashCode);
		}
	}

	private void UpdateShadowFromPoints(Vector2[] points) {
		UpdateShadowFromPoints(Vector2ToVector3(points));
	}

	private static Vector3[] Vector2ToVector3(IReadOnlyList<Vector2> points2D) {
		var points3D = new Vector3[points2D.Count];

		for (var i = 0; i < points2D.Count; i++) {
			points3D[i] = points2D[i];
		}

		return points3D;
	}
}
