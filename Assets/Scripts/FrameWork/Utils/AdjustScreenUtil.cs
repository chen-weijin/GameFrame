using UnityEngine;

namespace NGame
{
    public class AdjustScreenUtil
    {
        public static int ResizeWidth = 1920;       //设计的屏幕宽
        public static int ResizeHeight = 1080;      //设计的屏幕高

        public static float TrueWidth { get; private set; }     //适配后，真实的屏幕宽
        public static float TrueHeight { get; private set; }    //适配后，真实的屏幕高

        public static float Camera2DOrthoSize => TrueHeight / 2 / 100;  //2D摄像机的orthographicSize，这里Pixels Per Unit当是默认100，所以除以100

        public float OffsetWidth => TrueWidth - ResizeWidth;        //偏差值
        public float OffsetHeight => TrueHeight - ResizeHeight;

        public float OffsetWidthHalf => (TrueWidth - ResizeWidth) / 2;	    //偏差值一半
        public float OffsetHeightHalf => (TrueHeight - ResizeHeight) / 2;
    
    
	
        public static float ScaleValue { get; private set; }        //适配时的缩放值
	
        public static int OffsetX { get; private set; }		//刘海屏的适配，横屏，左右往中间靠
        
        public const int AdjustMoveWidth = 114; //适配时，要移动的像素宽度

        public static int AdjustNeedMoveWidth => IsNeedAdjust() ? AdjustMoveWidth : 0;//根据是否为刘海屏，给出要移动的像素

        private static bool _isInit;

        /// <summary>
        /// 会重置分辨率，这里会重复调用
        /// </summary>
        public static void Init(bool force = false)
        {
            if (!force)
            {
                if (_isInit) return;
            }

            _isInit = true;
        
            var scaleWidth = (float) ResizeWidth / Screen.width;
            var scaleHeight = (float) ResizeHeight / Screen.height;
            if (scaleWidth > scaleHeight)
            {
                TrueWidth = ResizeWidth;
                TrueHeight = Screen.height * scaleWidth;
                ScaleValue = scaleWidth;
            }
            else
            {
                TrueWidth = Screen.width * scaleHeight;
                TrueHeight = ResizeHeight;
                ScaleValue = scaleHeight;
            }

            OffsetX = Screen.width / (float)Screen.height <= 2.15f ? 0 : AdjustMoveWidth;
        }

        /// <summary>
        /// 是否为 刘海屏，需要适配的
        /// </summary>
        public static bool IsNeedAdjust()
        {
            /*
#if !UNITY_EDITOR
            if (Application.platform != RuntimePlatform.Android && Application.platform != RuntimePlatform.IPhonePlayer)
            {
                return false;
            }
#endif
            if (Screen.width > ResizeWidth)
            {
                return Screen.width / (float)Screen.height >= 2f;
            }
            else
            {
                return false;
            }
            */
            return Screen.width / (float)Screen.height > 2;
        }

        public static float GetAdjust()
        {
            if (IsNeedAdjust())
            {
                return (float)AdjustMoveWidth / Screen.width;
            }
            return 0;
        }

        public static float GetAdjust(float f)
        {
            return (float)f / Screen.width;
        }

        /// <summary>
        /// 是否为宽屏
        /// </summary>
        public static bool IsWidth()
        {
            return IsNeedAdjust();
        }

    }
}


