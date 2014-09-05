using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class SimpleLWF : LWFObject
{
	public string lwfPath;

	void Start()
	{
		Load(lwfPath);
		ScaleForHeight((int)Camera.main.orthographicSize * 2);
	}
}
