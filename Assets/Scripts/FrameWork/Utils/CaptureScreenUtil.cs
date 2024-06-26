using UnityEngine;

public class CaptureScreenUtil
{
    /// <summary>
    /// 截屏，全屏     要放在 WaitForEndOfFrame 后
    /// </summary>
    public static Texture2D CaptureFullScreen()
    {
        var rect = new Rect(0, 0, Screen.width, Screen.height);
        return CaptureRect(rect);
    }

    /// <summary>
    /// 截屏幕某个区域   要放在 WaitForEndOfFrame 后
    /// </summary>
    public static Texture2D CaptureRect(Rect rect)
    {
        var tex = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);
        tex.ReadPixels(rect, 0, 0 , false);
        tex.Apply();

        return tex;
    }
    
    /// <summary>
    /// 截图根据某个摄像机
    /// </summary>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static RenderTexture CaptureWithCamera(Camera camera)
    {
        var currentRT = RenderTexture.active;
        
        //RenderTexture
        var width = Screen.width;
        var height = Screen.height;
        if (height > 720)
        {
            width = (int)(width * 720f / height);
            height = 720;
        }
        var rt = RenderTexture.GetTemporary(width, height);
        camera.targetTexture = rt;
        camera.RenderDontRestore();
        RenderTexture.active = rt;

        //渲染恢复
        camera.targetTexture = null;
        RenderTexture.active = currentRT;

        return rt;
    }

    public static RenderTexture CaptureWithCamera(Camera camera,Vector2 v2)
    {
        var currentRT = RenderTexture.active;

        //RenderTexture
        var rt = RenderTexture.GetTemporary((int)v2.x, (int)v2.y, 32);
        camera.targetTexture = rt;
        camera.RenderDontRestore();
        RenderTexture.active = rt;

        //渲染恢复
        camera.targetTexture = null;
        RenderTexture.active = currentRT;

        return rt;
    }
}
