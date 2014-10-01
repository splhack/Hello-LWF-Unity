using UnityEngine;
using System;
using System.Collections.Generic;
using LWF;

using CollisionCallback = System.Action<UnityEngine.Collision2D>;
using TriggerCallback = System.Action<UnityEngine.Collider2D>;

public class ColliderWrapper : MonoBehaviour
{
	CollisionCallback mOnCollisionEnter2D;
	CollisionCallback mOnCollisionExit2D;
	CollisionCallback mOnCollisionStay2D;
	TriggerCallback mOnTriggerEnter2D;
	TriggerCallback mOnTriggerExit2D;
	TriggerCallback mOnTriggerStay2D;

	public void Set(float radius,
		CollisionCallback onCollisionEnter2D,
		CollisionCallback onCollisionExit2D,
		CollisionCallback onCollisionStay2D,
		TriggerCallback onTriggerEnter2D,
		TriggerCallback onTriggerExit2D,
		TriggerCallback onTriggerStay2D)
	{
		var rigidBody = gameObject.AddComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0;

		var collider = gameObject.AddComponent<CircleCollider2D>();
		collider.isTrigger = true;
		collider.center = new Vector2(radius, -radius);
		collider.radius = radius;

		mOnCollisionEnter2D = onCollisionEnter2D;
		mOnCollisionExit2D = onCollisionExit2D;
		mOnCollisionStay2D = onCollisionStay2D;
		mOnTriggerEnter2D = onTriggerEnter2D;
		mOnTriggerExit2D = onTriggerExit2D;
		mOnTriggerStay2D = onTriggerStay2D;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (mOnCollisionEnter2D != null)
			mOnCollisionEnter2D(coll);
	}

	void OnCollisionExit2D(Collision2D coll)
	{
		if (mOnCollisionExit2D != null)
			mOnCollisionExit2D(coll);
	}

	void OnCollisionStay2D(Collision2D coll)
	{
		if (mOnCollisionStay2D != null)
			mOnCollisionStay2D(coll);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (mOnTriggerEnter2D != null)
			mOnTriggerEnter2D(other);
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (mOnTriggerExit2D != null)
			mOnTriggerExit2D(other);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (mOnTriggerStay2D != null)
			mOnTriggerStay2D(other);
	}
}

public class ColliderHelper : LWF.Renderer
{
	private LWFObject m_parent;
	private GameObject m_gameObject;

	public ColliderHelper(LWFObject parent, LWF.LWF lwf, float radius,
		CollisionCallback onCollisionEnter2D = null,
		CollisionCallback onCollisionExit2D = null,
		CollisionCallback onCollisionStay2D = null,
		TriggerCallback onTriggerEnter2D = null,
		TriggerCallback onTriggerExit2D = null,
		TriggerCallback onTriggerStay2D = null) : base(lwf)
	{
		m_parent = parent;
		m_gameObject = new GameObject("ColliderHelper");
		m_gameObject.transform.parent = m_parent.gameObject.transform;
		m_gameObject.transform.localPosition = Vector3.zero;
		m_gameObject.transform.localScale = Vector3.one;
		m_gameObject.transform.localRotation = Quaternion.identity;

		var collider = m_gameObject.AddComponent<ColliderWrapper>();
		collider.Set(radius,
			onCollisionEnter2D, onCollisionExit2D, onCollisionStay2D,
			onTriggerEnter2D, onTriggerExit2D, onTriggerStay2D);
	}

	public override void Destruct()
	{
		GameObject.Destroy(m_gameObject);
	}

	public override void Update(Matrix matrix, ColorTransform colorTransform)
	{
		var m = new Matrix4x4();
		m_parent.factory.ConvertMatrix(ref m, matrix);
		m_gameObject.transform.localPosition = m.GetColumn(3);
		m_gameObject.transform.localScale = new Vector3(
			m.GetColumn(0).magnitude, m.GetColumn(1).magnitude,
			m.GetColumn(2).magnitude);
		m_gameObject.transform.localRotation =
			Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
	}
}

public class CollisionTest : LWFObject
{
	void Start()
	{
		Load("CollisionTest/CollisionTest");
		FitForHeight((int)Camera.main.orthographicSize * 2);

		SetProgramObjectConstructor("collider1",
				(programObject, objectId, width, height) => {
			return new ColliderHelper(this, programObject.lwf, width / 2.0f,
				onTriggerEnter2D:(other) => {
					Debug.Log("hit");
					programObject.parent.GotoAndPlay("jump");
				});
		});
	}
}
