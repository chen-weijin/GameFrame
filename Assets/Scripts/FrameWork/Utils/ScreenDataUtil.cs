using UnityEngine;

namespace NGame
{
    public class ScreenDataUtil
    {
        /// <summary>
        /// 默认的屏幕宽高比。1920、1080
        /// </summary>
        public static float ScreenRatio =1.777f ;//1920/1080
        public static float ScreenWidRatio =2.166f ;//1920/1080
        public static float ScreenWidRatioOffset =0.389f ;//1920/1080
        public static float ScreenHgtRatio =1.333f ;//1920/1080
        public static float ScreenHgtRatioOffset =0.444f ;//1920/1080
        
        public static bool Is4To3Screen()
        {
            return Mathf.Abs(1.3f-Screen.width*1.0f / Screen.height)<0.1f ;
        }

        public static bool Is6To5Screen()
        {
            return Mathf.Abs(1.2f - Screen.width * 1.0f / Screen.height) < 0.1f;
        }

        /// <summary>
        /// 获取屏幕分辨率的比例
        /// </summary>
        /// <returns></returns>
        public static float GetScreenRate()
        {
            return Screen.width*1.0f / Screen.height;
        }
    }
}
