using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class SimpleLWF : LWFObject
{
	public string lwfPath;

	void Start()
	{
		string texPath = Path.GetDirectoryName(lwfPath) + "/";
		Load(lwfPath, texPath);
		ScaleForHeight((int)Camera.main.orthographicSize * 2);
	}
}
