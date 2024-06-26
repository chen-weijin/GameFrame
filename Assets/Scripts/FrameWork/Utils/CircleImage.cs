using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;


namespace NGame
{
    /// <summary>
    /// 圆形
    /// </summary>
    public class CircleImage : Image
    {
        [Range(3, 100)] [SerializeField] private int _triangleCount = 50; //三角形数量

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            if (sprite == null)
            {
                base.OnPopulateMesh(toFill);
                return;
            }

            toFill.Clear();

            var r = GetPixelAdjustedRect();
            if (r.width < 0.001f || r.height < 0.001f) return; //没有图片 或 宽高有为0的，就不画了

            var rect = GetDrawingDimensions();
            var uv = DataUtility.GetOuterUV(sprite);
            uv.z -= uv.x;
            uv.w -= uv.y;
            DrawCircle(toFill, rect, uv);
        }

        private void DrawCircle(VertexHelper vh, Vector4 rect, Vector4 uvRect)
        {
            var convertRatio = new Vector2(uvRect.z / rect.z, uvRect.w / rect.w); //顶点 跟 uv的转换系数

            var color32 = color;

            var middlePos = new Vector2(rect.x + rect.z / 2, rect.y + rect.w / 2); //中心点
            var middleUv = GetUvByPos(middlePos, rect, uvRect, convertRatio);
            vh.AddVert(middlePos, color32, middleUv); //中心点是第一个顶点

            var radius = Mathf.Min(rect.z, rect.w) / 2; //半径
            var angleStep = Mathf.PI * 2 / _triangleCount; //每一次转动的角度

            var totalCount = _triangleCount;
            for (var i = 0; i < totalCount; i++)
            {
                var offsetPos = new Vector2(radius * Mathf.Cos(angleStep * i), radius * Mathf.Sin(angleStep * i));
                var pos = middlePos + offsetPos;
                var uv = GetUvByPos(pos, rect, uvRect, convertRatio);
                vh.AddVert(pos, color32, uv);

                if (i >= 1)
                {
                    vh.AddTriangle(0, i + 1, i);
                }
            }

            vh.AddTriangle(0, 1, totalCount); //最后那个三角形
        }

        private Vector2 GetUvByPos(Vector2 pos, Vector4 rect, Vector4 uvRect, Vector2 convertRatio)
        {
            var offset = new Vector2((pos.x - rect.x) * convertRatio.x, (pos.y - rect.y) * convertRatio.y);
            return new Vector2(uvRect.x + offset.x, uvRect.y + offset.y);
        }

        /// 从 Image 那边复制过来的，方便后面计算，返回值上 zw 改成了 宽高
        /// Image's dimensions used for drawing. X = left, Y = bottom, Z = width, W = height
        private Vector4 GetDrawingDimensions()
        {
            var padding = DataUtility.GetPadding(sprite);
            var size = new Vector2(sprite.rect.width, sprite.rect.height);

            Rect r = GetPixelAdjustedRect();
            // NGDebug.Log(string.Format("r:{2}, size:{0}, padding:{1}", size, padding, r));

            int spriteW = Mathf.RoundToInt(size.x);
            int spriteH = Mathf.RoundToInt(size.y);

            var v = new Vector4(
                padding.x / spriteW,
                padding.y / spriteH,
                (spriteW - padding.z) / spriteW,
                (spriteH - padding.w) / spriteH);

            v = new Vector4(
                r.x + r.width * v.x,
                r.y + r.height * v.y,
                r.x + r.width * v.z,
                r.y + r.height * v.w
            );

            v.z -= v.x;
            v.w -= v.y;

            return v;
        }
    }
}