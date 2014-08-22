using UnityEngine;
using LuaInterface;
using LWF;

public class CharacterProgramObject : LWF.Renderer
{
	private LWFObject m_parent;
	private GameObject m_cube;

	public CharacterProgramObject(
		LWFObject parent, LWF.LWF lwf, int width, int height) : base(lwf)
	{
		m_parent = parent;
		m_cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		m_cube.transform.parent = m_parent.gameObject.transform;
		m_cube.transform.localScale =
			new Vector3(width / 100.0f, height / 100.0f, 1);
	}

	public override void Destruct()
	{
		GameObject.Destroy(m_cube);
	}

	public override void Update(Matrix matrix, ColorTransform colorTransform)
	{
		var m = new Matrix4x4();
		m_parent.factory.ConvertMatrix(ref m, matrix);
		m_cube.transform.position = new Vector3(m[0, 3], m[1, 3], 0);

		var mc = new UnityEngine.Color();
		var ac = new UnityEngine.Color();
		m_parent.factory.ConvertColorTransform(ref mc, ref ac, colorTransform);
		m_cube.GetComponent<MeshRenderer>().material.color = mc;
	}
}

public class ProgramObjectTest : LWFObject
{
	void Start()
	{
		Load("ProgramObjectTest/ProgramObjectTest");
		FitForHeight((int)Camera.main.orthographicSize * 2);

		SetProgramObjectConstructor(
			"character", CharacterProgramObjectConstructor);
	}

	private LWF.Renderer CharacterProgramObjectConstructor(
		ProgramObject programObject, int objectId, int width, int height)
	{
		return new CharacterProgramObject(
			this, programObject.lwf, width, height);
	}
}
