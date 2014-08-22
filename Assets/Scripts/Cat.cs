using UnityEngine;
using LuaInterface;

[ExecuteInEditMode]
public class Cat : LWFObject
{
	public string sortingLayer;
	public int sortingOrder;

	Lua L;

	void Start()
	{
		var script = Resources.Load("cat/cat_script") as TextAsset;
		L = new Lua();
		L.DoString(script.text);
		Load("cat/cat",
			sortingLayerName:sortingLayer, sortingOrder:sortingOrder,
			luaState:L.luaState);
		ScaleForHeight((int)Camera.main.orthographicSize * 2);
	}
}
