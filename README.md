Hello-LWF-Unity
===================

An example of LWF for Unity

What is LWF?
------------

LWF http://gree.github.io/lwf/

> LWF is an animation engine which can play animation data converted from **FLASH contents** in HTML5, **Unity**, Cocos2d-x, iOS UIKit, and more. LWF is designed to make game development easy and fun.

It means that LWF allows you to make animation using Adobe Flash for your Unity Application.

The example
-----------

You'll see a Flash movie which is embedded on Unity.

How to use Adobe Flash for making animation
-------------------------------------------

Please take a look at [LWF Presentation](http://gree.github.io/lwf/presentation20121115) and [LWF Production Guide](http://gree.github.io/lwf-demo/pdf/FLASHforLWFproductionguideline.pdf).

Install LWFS https://github.com/gree/lwfs to convert Adobe Flash data into LWF data. It automatically converts in ~/Desktop/LWFS_work folder and shows the data on Web browser.

Bitmap font
-----------

This project uses [Bitmap font](https://github.com/splhack/Hello-LWF-Unity/tree/master/Assets/Resources/BitmapFont). Please refer [the document](https://github.com/gree/unity-bitmapfontrenderer/tree/master/sample/Assets/Resources/BitmapFont) to generate your own Bitmap font. You may need to [set Bitmap font loader](https://github.com/splhack/Hello-LWF-Unity/blob/5db71c9e5a4e29f731a028d429f7219f8bb5142f/Assets/Scripts/Main.cs#L8-L15) ahead of using LWF.

Lua scripting
-------------

This project also enables Lua scripting of LWF with [KopiLua](http://gfootweb.webspace.virginmedia.com/KLI-bin/).

Define LWF_USE_LUA in [Assets/smcs.rsp](https://github.com/splhack/Hello-LWF-Unity/blob/master/Assets/smcs.rsp).

Load Lua script and pass luaState to luaState argument of LWFObject.Load method.

    var script = Resources.Load(Lua_script_filename) as TextAsset;
    L = new Lua();
    L.DoString(script.text);
    Load(lwfPath, luaState:L.luaState);

You can also write Lua script in Flash actionscript panel like [LWF for HTML5 Embedded JavaScript](https://github.com/gree/lwf/wiki/Blog-%231).

For instance,

    /* lua
        self:gotoAndPlay("run")
    */


    on (press) { /* lua
        self.cat:gotoAndPlay("jump")
    */ }

TBU
