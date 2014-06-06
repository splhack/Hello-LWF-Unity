using UnityEngine;
using LuaInterface;

[ExecuteInEditMode]
public class Cat : LWFObject
{
	Lua L;

	void Start()
	{
		var script = Resources.Load("cat/cat_script") as TextAsset;
		L = new Lua();
		L.DoString(script.text);
		Load("cat/cat", "cat/", luaState:L.luaState);
		ScaleForHeight((int)Camera.main.orthographicSize * 2);
	}
}
