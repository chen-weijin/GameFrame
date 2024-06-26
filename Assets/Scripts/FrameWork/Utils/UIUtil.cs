using UnityEngine;

namespace NGame
{
    public class UIUtil
    {
        public static Vector2 defaultScreenSize = new Vector2(1920,1080);

        #region 图片模糊

        private static Material _blurMat;  //模糊用

        /// <summary>
        /// 截图，并模糊
        /// </summary>
        //public static RenderTexture CreateBlurTex()
        //{
        //    var tex = CaptureScreenUtil.CaptureFullScreen();    //截图
        //    var timeStr = Time.time.ToString("F1");
        //    tex.name = $"_SGame_Mask_Capture_{timeStr}";

        //    var rt = UIUtil.BlurTex(tex);   //模糊
        //    rt.name = $"_SGame_Mask_Capture_Blur_{timeStr}";

        //    Object.Destroy(tex);

        //    return rt;
        //}

        /// <summary>
        /// 模糊一张图
        /// </summary>
        /// <param name="tex">原图</param>
        /// <param name="blur">模糊力度</param>
        /// <param name="blurCount">模糊几次</param>
        /// <param name="downSample">按屏幕分辨率，降低采样</param>
        //public static RenderTexture BlurTex(Texture tex, float blur = 1f, int blurCount = 1, int downSample = 4)
        //{
        //    if (_blurMat == null)
        //    {
        //        _blurMat = UnityEngine.Rendering.CoreUtils.CreateEngineMaterial(Shader.Find("UI/UI_Gaussian_Blur"));
        //        _blurMat.SetFloat("_Blur", blur);
        //    }

        //    var tempRT = RenderTexture.GetTemporary(tex.width / downSample, tex.height / downSample);
        //    tempRT.name = "_SGame_Blur";
        //    var tempRT2 = RenderTexture.GetTemporary(tempRT.width, tempRT.height);
        //    tempRT2.name = "_SGame_Blur_Temp";

        //    Graphics.Blit(tex, tempRT);

        //    for (int i = 0; i < blurCount; i++)
        //    {
        //        Graphics.Blit(tempRT, tempRT2, _blurMat, 0);
        //        tempRT.Release();

        //        Graphics.Blit(tempRT2, tempRT, _blurMat, 1);
        //        tempRT2.Release();
        //    }

        //    RenderTexture.ReleaseTemporary(tempRT2);

        //    return tempRT;
        //}

        #endregion

        // 改变一个RectTransform组件的pivot值，但是保持其实际位置不变
        public static void ChangePivot(RectTransform rectTransform, float newPivotX, float newPivotY)
        {
            float originalPivotX = rectTransform.pivot.x;
            float originalPivotY = rectTransform.pivot.y;
            // 在某些特定布局下，Unity会在设置Pivot时自动调整LocalPosition以试图保持UI对象不被挪动，但这个“贴心”设定充满意外惊喜需要排除
            Vector3 originalLocalPosition = rectTransform.localPosition;
            rectTransform.pivot = new Vector2(newPivotX, newPivotY);
            rectTransform.localPosition = originalLocalPosition;

            rectTransform.transform.position += new Vector3((newPivotX - originalPivotX) * rectTransform.rect.width, (newPivotY - originalPivotY) * rectTransform.rect.height, 0);
        }
    }
}
