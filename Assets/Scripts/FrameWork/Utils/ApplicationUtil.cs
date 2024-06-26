using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationUtil 
{
    /// <summary>
    /// 检查当前设备是否连接到Internet
    /// </summary>
    /// <returns> 1： 移动数据;  2： wifi </returns>
    public static int CheckWifi()
    {
        if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            return 1;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }

}
