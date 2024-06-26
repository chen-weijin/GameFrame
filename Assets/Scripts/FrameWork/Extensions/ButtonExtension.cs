using UnityEngine.UI;

/// <summary>
/// Button组件功能扩展
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class ButtonExtension
{
    /// <summary>
    /// 设置能否点击且按钮置灰
    /// </summary>
    /// <param name="bt"></param>
    [UnityEngine.Scripting.Preserve]
    public static void SetEnable(this Button bt, bool enable)
    {
        bt.enabled = enable;
        bt.interactable = enable;
    }
}
