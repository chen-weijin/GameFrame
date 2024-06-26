using UnityEngine;

/// <summary>
/// 9点布局
/// </summary>
[UnityEngine.Scripting.Preserve]
public static class layout
{
    public static readonly Vector2 left_top = new Vector2(0, 1);
    public static readonly Vector2 left_center = new Vector2(0, 0.5f);
    public static readonly Vector2 left_bottom = new Vector2(0, 0);

    public static readonly Vector2 center = new Vector2(0.5f, 0.5f);
    public static readonly Vector2 center_bottom = new Vector2(0.5f, 0);
    public static readonly Vector2 center_top = new Vector2(0.5f, 1);

    public static readonly Vector2 right_top = new Vector2(1, 1);
    public static readonly Vector2 right_center = new Vector2(1, 0.5f);
    public static readonly Vector2 right_bottom = new Vector2(1, 0);
}