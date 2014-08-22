using UnityEngine;
using System.IO;

[ExecuteInEditMode]
public class SimpleLWF : LWFObject
{
	public string lwfPath;
	public string sortingLayer;
	public int sortingOrder;

	void Start()
	{
		Load(lwfPath, sortingLayerName:sortingLayer, sortingOrder:sortingOrder);
		ScaleForHeight((int)Camera.main.orthographicSize * 2);
	}
}
