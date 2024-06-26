using System;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;


namespace SGame
{
    /// <summary>
    /// 圆角矩形，矩形大小可调
    /// </summary>
    public class RoundRectImage : Image
    {
        [Range(1, 20)] [SerializeField] private int _triangleCount = 5; //三角形数量
        
        [Range(0, 10)] [SerializeField] private int _leftBottom = 4;       //圆角距离，像素
        [Range(0, 10)] [SerializeField] private int _rightBottom = 4;
        [Range(0, 10)] [SerializeField] private int _rightTop = 4;
        [Range(0, 10)] [SerializeField] private int _leftTop = 4;
        
        [Range(0, 1)] [SerializeField] private float _left;     //往相反方向靠的百分比
        [Range(0, 1)] [SerializeField] private float _right;
        [Range(0, 1)] [SerializeField] private float _top;
        [Range(0, 1)] [SerializeField] private float _bottom;


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
            CalRect(ref rect);
            CalRect(ref uv);
            DrawRoundRect(toFill, rect, uv);
        }

        //根据参数，确定矩形大小
        private void CalRect(ref Vector4 v4)
        {
            var rightX = v4.x + v4.z;
            var topY = v4.y + v4.w;
            v4.x += v4.z * _left;
            v4.y += v4.w * _bottom;
            v4.z = rightX - v4.z * _right - v4.x;
            v4.w = topY - v4.w * _top - v4.y;
        }

        private void DrawRoundRect(VertexHelper vh, Vector4 rect, Vector4 uvRect)
        {
            var convertRatio = new Vector2(uvRect.z / rect.z, uvRect.w / rect.w); //顶点 跟 uv的转换系数

            var color32 = color;

            var middlePos = new Vector2(rect.x + rect.z / 2, rect.y + rect.w / 2); //中心点
            var middleUv = GetUvByPos(middlePos, rect, uvRect, convertRatio);
            vh.AddVert(middlePos, color32, middleUv); //中心点是第一个顶点

            var angleStep = Mathf.PI / 2 / _triangleCount;
            var totalCount = _triangleCount;
            
            //画 1/4 圆。 qrMiddlePos：中心点，radius：半径，startAngle：开始角度， 返回值：画的顶点数量
            int DrawQuarterRound(Vector2 qrMiddlePos, float radius, float startAngle, int triangleCount)
            {
                if (radius == 0)    //没有圆角，只画一个点
                {
                    var uv = GetUvByPos(qrMiddlePos, rect, uvRect, convertRatio);
                    vh.AddVert(qrMiddlePos, color32, uv);
                    return 1;
                }
                
                for (var i = 0; i <= totalCount; i++)   //画 1/4 圆
                {
                    var angle = startAngle + angleStep * i;
                    var offsetPos = new Vector2(radius * Mathf.Cos(angle), radius * Mathf.Sin(angle));
                    var pos = qrMiddlePos + offsetPos;
                    var uv = GetUvByPos(pos, rect, uvRect, convertRatio);
                    vh.AddVert(pos, color32, uv);

                    if (i >= 1)
                    {
                        vh.AddTriangle(0, triangleCount + i + 1, triangleCount + i);
                    }
                }

                return totalCount + 1;  //顶点数量
            }

            //右上
            var rightTopPos = new Vector2(rect.x + rect.z, rect.y + rect.w);    //右上点
            var rtMiddlePos = new Vector2(rightTopPos.x - _rightTop, rightTopPos.y - _rightTop); //右上中心点
            var drawTriangleCount = DrawQuarterRound(rtMiddlePos, _rightTop, 0, 0); //画 1/4 圆

            //左上
            var leftTopPos = new Vector2(rect.x, rect.y + rect.w);  //左上点
            var ltMiddlePos = new Vector2(leftTopPos.x + _leftTop, leftTopPos.y - _leftTop); //左上中心点
            var addTriangleCount = DrawQuarterRound(ltMiddlePos, _leftTop, Mathf.PI / 2, drawTriangleCount); //画 1/4 圆
            vh.AddTriangle(0, drawTriangleCount + 1, drawTriangleCount);    //上边那个三角形
            drawTriangleCount += addTriangleCount;
            
            //左下
            var leftBottomPos = new Vector2(rect.x, rect.y);  //左下点
            var lbMiddlePos = new Vector2(leftBottomPos.x + _leftBottom, leftBottomPos.y + _leftBottom); //左下中心点
            addTriangleCount = DrawQuarterRound(lbMiddlePos, _leftBottom, Mathf.PI, drawTriangleCount); //画 1/4 圆
            vh.AddTriangle(0, drawTriangleCount + 1, drawTriangleCount);    //左边那个三角形
            drawTriangleCount += addTriangleCount;
            
            //右下
            var rightBottomPos = new Vector2(rect.x + rect.z, rect.y);  //右下点
            var rbMiddlePos = new Vector2(rightBottomPos.x - _rightBottom, rightBottomPos.y + _rightBottom); //右下中心点
            addTriangleCount = DrawQuarterRound(rbMiddlePos, _rightBottom, Mathf.PI * 1.5f, drawTriangleCount); //画 1/4 圆
            vh.AddTriangle(0, drawTriangleCount + 1, drawTriangleCount);    //下边那个三角形
            drawTriangleCount += addTriangleCount;

            vh.AddTriangle(0, 1, drawTriangleCount); //右边那个三角形
        }

        /// <summary>
        /// 根据位置获取对应的uv
        /// </summary>
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
        
        [Serializable]
        public class Pos
        {
            [Range(0, 1)]
            public float x;
            [Range(0, 1)]
            public float y;
        }
    }
}