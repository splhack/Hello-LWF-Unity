using UnityEngine;

[ExecuteInEditMode]
public class Main : MonoBehaviour
{
	void Awake()
	{
		LWFObject.SetBitmapFontLoader(
			dataLoader:(name) => {return (Resources.Load("BitmapFont/MTLmr3m") as TextAsset).bytes;},
			textureLoader:(name) => {return Resources.Load("BitmapFont/MTLmr3m_texture") as Texture2D;},
			textureUnloader:(texture) => {
				if (!Application.isEditor)
					Texture2D.Destroy(texture);
			}
		);
	}
}
