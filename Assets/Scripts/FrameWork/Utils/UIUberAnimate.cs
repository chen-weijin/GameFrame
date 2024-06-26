using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NGame
{
    /// <summary>
    /// UI动态工具，用于Animate控制参数
    /// </summary>
    [RequireComponent(typeof(CanvasRenderer))]
    [ExecuteInEditMode]
    public class UIUberAnimate : MonoBehaviour
    {
        public enum UV
        {
            UV0 = 0,
            UV1 = 1,
        }

        public enum ColorBlend
        {
            Blend = 0,
            Add = 1,
            Mul = 2,
        }

        public enum Channel
        {
            R = 0,
            G = 1,
            B = 2,
            A = 3,
        }

        public enum ZWrite
        {
            Off = 0,
            On = 1,
        }

        public enum BlendMode
        {
            AlphaBlend = 0,
            Additive = 1,
            Additive2 = 2,
            SoftAdditive = 3,
            ParticleAdditive = 4,
            Premultiplied = 5,
            UI = 6,
        }

        public enum SurfaceType
        {
            Transparent = 0,
            AlphaTest = 1,
        }

        public Texture _MainTex;
        public Vector2 _MainTexScale;
        public Vector2 _MainTexOffset;
        public Color _Color;
        public Vector4 _MainUVPanner;
        public float _MainIntensity;
        
        public bool _MainUVDistort;
        public bool _MainUVXClamp;
        public bool _MainUVYClamp;
        public UV _MainUV;

        [Space(10)]
        public bool _SecondLayer;
        public ColorBlend _SecondColorBlend;

        public Texture _SecondTex;
        public Vector2 _SecondTexScale;
        public Vector2 _SecondTexOffset;
        public Color _SecondColor;
        public Vector4 _SecondUVPanner;

        public bool _SecondDistort;
        public bool _SecondUVXClamp;
        public bool _SecondUVYClamp;
        public UV _SecondUV;

        public bool _CustomData;

        [Space(10)]
        public bool _Rim;
        public float _RimPower;
        public Color _RimColor;

        [Space(10)]
        public bool _Distort;
        public Texture _DistortionTex;
        public Vector2 _DistortionTexScale;
        public Vector2 _DistortionTexOffset;
        public float _DistortionIntensity;
        public Vector4 _DistortUVPanner;
        public UV _DistortUV;
        public Channel _DistortChannel;

        [Space(10)]
        public bool _Alpha;
        public Texture _AlphaTex;
        public Vector2 _AlphaTexScale;
        public Vector2 _AlphaTexOffset;
        public Vector4 _AlphaUVPanner;
        public bool _AlphaDistort;
        public bool _AlphaUVXClamp;
        public bool _AlphaUVYClamp;
        public UV _AlphaUV;
        public Channel _AlphaChannel;

        [Space(10)]
        public bool _Diagonal;
        [Range(-1, 1)] public float _DiagonalX;
        [Range(-1, 1)] public float _DiagonalExtendX;
        [Range(-1, 1)] public float _DiagonalY;
        [Range(-1, 1)] public float _DiagonalExtendY;

        [Space(10)]
        public bool _Dissolve;
        public Texture _DissolveTex;
        public Vector2 _DissolveTexScale;
        public Vector2 _DissolveTexOffset;
        [Range(-0.5f, 2)] public float _DissolveIntensity;
        [Range(0, 2)] public float _DissolveSoft;
        [Range(0, 2)] public float _DissolveEdgeWidth;
        public Color _DissolveEdgeColor;
        public Vector4 _DissolveUVPanner;
        public UV _DissolveUV;
        public Channel _DissolveChannel;

        [Space(10)]
        public bool _GradientDissolve;
        public Vector4 _DissolveDirAndSphere;
        public bool _InverseSphere;
        public float _ObjectScale;
        public float _NoiseIntensity;
        public Vector4 _VertexOrigin;
        public bool _DissolveDistort;

        [Space(10)]
        public UnityEngine.Rendering.CullMode _Cull;
        public UnityEngine.Rendering.CompareFunction _ZTest;
        public ZWrite _ZWrite;
        public bool _PreZ;

        [Space(10)]
        public UnityEngine.Rendering.ColorWriteMask _ColorMask;
        public UnityEngine.Rendering.BlendMode _BlendSrc;
        public UnityEngine.Rendering.BlendMode _BlendDes;
        public BlendMode _BlendMode;
        public SurfaceType _SurfaceType;
        public bool _CloseLinearToSRGB;

        [Space(10)]
        [Range(-25, 25)] public int _QueueID;
        public float _CutOff;

        [Space(10)]
        public bool _HideButtom;

        public float _AlphaClip;
        public float _ReceiveShadows;
        [Range(0, 2)] public float _AlphaEnhance;

        //==========================================================

        private readonly int
            ShaderID_MainTex =              Shader.PropertyToID("_MainTex"),
            ShaderID_Color =                Shader.PropertyToID("_Color"),
            ShaderID_MainUVPanner =         Shader.PropertyToID("_MainUVPanner"),
            ShaderID_MainIntensity =        Shader.PropertyToID("_MainIntensity"),
            ShaderID_MainUVDistort =        Shader.PropertyToID("_MainUVDistort"),
            ShaderID_MainUVXClamp =         Shader.PropertyToID("_MainUVXClamp"),
            ShaderID_MainUVYClamp =         Shader.PropertyToID("_MainUVYClamp"),
            ShaderID_MainUV =               Shader.PropertyToID("_MainUV"),
            ShaderID_SecondLayer =          Shader.PropertyToID("_SecondLayer"),
            ShaderID_SecondColorBlend =     Shader.PropertyToID("_SecondColorBlend"),
            ShaderID_SecondTex =            Shader.PropertyToID("_SecondTex"),
            ShaderID_SecondColor =          Shader.PropertyToID("_SecondColor"),
            ShaderID_SecondUVPanner =       Shader.PropertyToID("_SecondUVPanner"),
            ShaderID_SecondDistort =        Shader.PropertyToID("_SecondDistort"),
            ShaderID_SecondUVXClamp =       Shader.PropertyToID("_SecondUVXClamp"),
            ShaderID_SecondUVYClamp =       Shader.PropertyToID("_SecondUVYClamp"),
            ShaderID_SecondUV =             Shader.PropertyToID("_SecondUV"),
            ShaderID_CustomData =           Shader.PropertyToID("_CustomData"),
            ShaderID_Rim =                  Shader.PropertyToID("_Rim"),
            ShaderID_RimPower =             Shader.PropertyToID("_RimPower"),
            ShaderID_RimColor =             Shader.PropertyToID("_RimColor"),
            ShaderID_Distort =              Shader.PropertyToID("_Distort"),
            ShaderID_DistortionTex =        Shader.PropertyToID("_DistortionTex"),
            ShaderID_DistortionIntensity =  Shader.PropertyToID("_DistortionIntensity"),
            ShaderID_DistortUVPanner =      Shader.PropertyToID("_DistortUVPanner"),
            ShaderID_DistortUV =            Shader.PropertyToID("_DistortUV"),
            ShaderID_DistortChannel =       Shader.PropertyToID("_DistortChannel"),
            ShaderID_Alpha =                Shader.PropertyToID("_Alpha"),
            ShaderID_AlphaTex =             Shader.PropertyToID("_AlphaTex"),
            ShaderID_AlphaUVPanner =        Shader.PropertyToID("_AlphaUVPanner"),
            ShaderID_AlphaDistort =         Shader.PropertyToID("_AlphaDistort"),
            ShaderID_AlphaUVXClamp =        Shader.PropertyToID("_AlphaUVXClamp"),
            ShaderID_AlphaUVYClamp =        Shader.PropertyToID("_AlphaUVYClamp"),
            ShaderID_AlphaUV =              Shader.PropertyToID("_AlphaUV"),
            ShaderID_AlphaChannel =         Shader.PropertyToID("_AlphaChannel"),
            ShaderID_Diagonal =             Shader.PropertyToID("_Diagonal"),
            ShaderID_DiagonalX =            Shader.PropertyToID("_DiagonalX"),
            ShaderID_DiagonalExtendX =      Shader.PropertyToID("_DiagonalExtendX"),
            ShaderID_DiagonalY =            Shader.PropertyToID("_DiagonalY"),
            ShaderID_DiagonalExtendY =      Shader.PropertyToID("_DiagonalExtendY"),
            ShaderID_Dissolve =             Shader.PropertyToID("_Dissolve"),
            ShaderID_DissolveTex =          Shader.PropertyToID("_DissolveTex"),
            ShaderID_DissolveIntensity =    Shader.PropertyToID("_DissolveIntensity"),
            ShaderID_DissolveSoft =         Shader.PropertyToID("_DissolveSoft"),
            ShaderID_DissolveEdgeWidth =    Shader.PropertyToID("_DissolveEdgeWidth"),
            ShaderID_DissolveEdgeColor =    Shader.PropertyToID("_DissolveEdgeColor"),
            ShaderID_DissolveUVPanner =     Shader.PropertyToID("_DissolveUVPanner"),
            ShaderID_DissolveUV =           Shader.PropertyToID("_DissolveUV"),
            ShaderID_DissolveChannel =      Shader.PropertyToID("_DissolveChannel"),
            ShaderID_GradientDissolve =     Shader.PropertyToID("_GradientDissolve"),
            ShaderID_DissolveDirAndSphere = Shader.PropertyToID("_DissolveDirAndSphere"),
            ShaderID_InverseSphere =        Shader.PropertyToID("_InverseSphere"),
            ShaderID_ObjectScale =          Shader.PropertyToID("_ObjectScale"),
            ShaderID_NoiseIntensity =       Shader.PropertyToID("_NoiseIntensity"),
            ShaderID_VertexOrigin =         Shader.PropertyToID("_VertexOrigin"),
            ShaderID_DissolveDistort =      Shader.PropertyToID("_DissolveDistort"),
            ShaderID_Cull =                 Shader.PropertyToID("_Cull"),
            ShaderID_ZTest =                Shader.PropertyToID("_ZTest"),
            ShaderID_ZWrite =               Shader.PropertyToID("_ZWrite"),
            ShaderID_PreZ =                 Shader.PropertyToID("_PreZ"),
            ShaderID_ColorMask =            Shader.PropertyToID("_ColorMask"),
            ShaderID_BlendSrc =             Shader.PropertyToID("_BlendSrc"),
            ShaderID_BlendDes =             Shader.PropertyToID("_BlendDes"),
            ShaderID_BlendMode =            Shader.PropertyToID("_BlendMode"),
            ShaderID_SurfaceType =          Shader.PropertyToID("_SurfaceType"),
            ShaderID_CloseLinearToSRGB =    Shader.PropertyToID("_CloseLinearToSRGB"),
            ShaderID_QueueID =              Shader.PropertyToID("_QueueID"),
            ShaderID_CutOff =               Shader.PropertyToID("_CutOff"),
            ShaderID_HideButtom =           Shader.PropertyToID("_HideButtom"),
            ShaderID_AlphaClip =            Shader.PropertyToID("_AlphaClip"),
            ShaderID_ReceiveShadows =       Shader.PropertyToID("_ReceiveShadows"),
            ShaderID_AlphaEnhance =         Shader.PropertyToID("_AlphaEnhance");

        private Material mMaterials;
        private CanvasRenderer mCanvasRenderer;

        private bool CheckMaterial()
        {
            mCanvasRenderer ??= GetComponent<CanvasRenderer>();

            if (mMaterials == null || mCanvasRenderer.GetMaterial() != mMaterials)
            {
                if (mCanvasRenderer == null)
                {
                    Debug.LogError("没有放到UI组件上!");
                    return false;
                }

                mMaterials = mCanvasRenderer.GetMaterial();

                if (mMaterials == null)
                {
                    Debug.LogWarning("UIUberAnimate mMaterials is null!");
                    return false;
                }

                if (mMaterials.shader == null)
                {
                    return false;
                }

                if (mMaterials.shader.name != "FB/UI/SGameUIUber" &&
                    mMaterials.shader.name != "FB/UI/SGameUIUber_Stencil")
                {
                    return false;
                }

                //_MainTex                = mMaterials.GetTexture(ShaderID_MainTex);
                _MainTexScale           = mMaterials.GetTextureScale(ShaderID_MainTex);
                _MainTexOffset          = mMaterials.GetTextureOffset(ShaderID_MainTex);
                _Color                  = mMaterials.GetColor(ShaderID_Color);
                _MainUVPanner           = mMaterials.GetVector(ShaderID_MainUVPanner);
                _MainIntensity          = mMaterials.GetFloat(ShaderID_MainIntensity);
                _MainUVDistort          = mMaterials.GetFloat(ShaderID_MainUVDistort) != 0;
                _MainUVXClamp           = mMaterials.GetFloat(ShaderID_MainUVXClamp) != 0;
                _MainUVYClamp           = mMaterials.GetFloat(ShaderID_MainUVYClamp) != 0;
                _MainUV                 = (UV)mMaterials.GetFloat(ShaderID_MainUV);

                _SecondLayer            = mMaterials.GetFloat(ShaderID_SecondLayer) != 0;
                _SecondColorBlend       = (ColorBlend)mMaterials.GetFloat(ShaderID_SecondColorBlend);
                _SecondTex              = mMaterials.GetTexture(ShaderID_SecondTex);
                _SecondTexScale         = mMaterials.GetTextureScale(ShaderID_SecondTex);
                _SecondTexOffset        = mMaterials.GetTextureOffset(ShaderID_SecondTex);
                _SecondColor            = mMaterials.GetColor(ShaderID_SecondColor);
                _SecondUVPanner         = mMaterials.GetVector(ShaderID_SecondUVPanner);
                _SecondDistort          = mMaterials.GetFloat(ShaderID_SecondDistort) != 0;
                _SecondUVXClamp         = mMaterials.GetFloat(ShaderID_SecondUVXClamp) != 0;
                _SecondUVYClamp         = mMaterials.GetFloat(ShaderID_SecondUVYClamp) != 0;
                _SecondUV               = (UV)mMaterials.GetFloat(ShaderID_SecondUV);

                _CustomData             = mMaterials.GetFloat(ShaderID_CustomData) != 0;
                _Rim                    = mMaterials.GetFloat(ShaderID_Rim) != 0;
                _RimPower               = mMaterials.GetFloat(ShaderID_RimPower);
                _RimColor               = mMaterials.GetColor(ShaderID_RimColor);
                _Distort                = mMaterials.GetFloat(ShaderID_Distort) != 0;
                _DistortionTex          = mMaterials.GetTexture(ShaderID_DistortionTex);
                _DistortionTexScale     = mMaterials.GetTextureScale(ShaderID_DistortionTex);
                _DistortionTexOffset    = mMaterials.GetTextureOffset(ShaderID_DistortionTex);
                _DistortionIntensity    = mMaterials.GetFloat(ShaderID_DistortionIntensity);
                _DistortUVPanner        = mMaterials.GetVector(ShaderID_DistortUVPanner);
                _DistortUV              = (UV)mMaterials.GetFloat(ShaderID_DistortUV);
                _DistortChannel         = (Channel)mMaterials.GetFloat(ShaderID_DistortChannel);

                _Alpha                  = mMaterials.GetFloat(ShaderID_Alpha) != 0;
                _AlphaTex               = mMaterials.GetTexture(ShaderID_AlphaTex);
                _AlphaTexScale          = mMaterials.GetTextureScale(ShaderID_AlphaTex);
                _AlphaTexOffset         = mMaterials.GetTextureOffset(ShaderID_AlphaTex);
                _AlphaUVPanner          = mMaterials.GetVector(ShaderID_AlphaUVPanner);
                _AlphaDistort           = mMaterials.GetFloat(ShaderID_AlphaDistort) != 0;
                _AlphaUVXClamp          = mMaterials.GetFloat(ShaderID_AlphaUVXClamp) != 0;
                _AlphaUVYClamp          = mMaterials.GetFloat(ShaderID_AlphaUVYClamp) != 0;
                _AlphaUV                = (UV)mMaterials.GetFloat(ShaderID_AlphaUV);
                _AlphaChannel           = (Channel)mMaterials.GetFloat(ShaderID_AlphaChannel);

                _Diagonal               = mMaterials.GetFloat(ShaderID_Diagonal) != 0;
                _DiagonalX              = mMaterials.GetFloat(ShaderID_DiagonalX);
                _DiagonalExtendX        = mMaterials.GetFloat(ShaderID_DiagonalExtendX);
                _DiagonalY              = mMaterials.GetFloat(ShaderID_DiagonalY);
                _DiagonalExtendY        = mMaterials.GetFloat(ShaderID_DiagonalExtendY);

                _Dissolve               = mMaterials.GetFloat(ShaderID_Dissolve) != 0;
                _DissolveTex            = mMaterials.GetTexture(ShaderID_DissolveTex);
                _DissolveTexScale       = mMaterials.GetTextureScale(ShaderID_DissolveTex);
                _DissolveTexOffset      = mMaterials.GetTextureOffset(ShaderID_DissolveTex);
                _DissolveIntensity      = mMaterials.GetFloat(ShaderID_DissolveIntensity);
                _DissolveSoft           = mMaterials.GetFloat(ShaderID_DissolveSoft);
                _DissolveEdgeWidth      = mMaterials.GetFloat(ShaderID_DissolveEdgeWidth);
                _DissolveEdgeColor      = mMaterials.GetColor(ShaderID_DissolveEdgeColor);
                _DissolveUVPanner       = mMaterials.GetVector(ShaderID_DissolveUVPanner);
                _DissolveUV             = (UV)mMaterials.GetFloat(ShaderID_DissolveUV);
                _DissolveChannel        = (Channel)mMaterials.GetFloat(ShaderID_DissolveChannel);

                _GradientDissolve       = mMaterials.GetFloat(ShaderID_GradientDissolve) != 0;
                _DissolveDirAndSphere   = mMaterials.GetVector(ShaderID_DissolveDirAndSphere);
                _InverseSphere          = mMaterials.GetFloat(ShaderID_InverseSphere) != 0;
                _ObjectScale            = mMaterials.GetFloat(ShaderID_ObjectScale);
                _NoiseIntensity         = mMaterials.GetFloat(ShaderID_NoiseIntensity);
                _VertexOrigin           = mMaterials.GetVector(ShaderID_VertexOrigin);
                _DissolveDistort        = mMaterials.GetFloat(ShaderID_DissolveDistort) != 0;
                _Cull                   = (UnityEngine.Rendering.CullMode)mMaterials.GetFloat(ShaderID_Cull);
                _ZTest                  = (UnityEngine.Rendering.CompareFunction)mMaterials.GetFloat(ShaderID_ZTest);
                _ZWrite                 = (ZWrite)mMaterials.GetFloat(ShaderID_ZWrite);
                _PreZ                   = mMaterials.GetFloat(ShaderID_PreZ) != 0;
                _ColorMask              = (UnityEngine.Rendering.ColorWriteMask)mMaterials.GetFloat(ShaderID_ColorMask);
                _BlendSrc               = (UnityEngine.Rendering.BlendMode)mMaterials.GetFloat(ShaderID_BlendSrc);
                _BlendDes               = (UnityEngine.Rendering.BlendMode)mMaterials.GetFloat(ShaderID_BlendDes);
                _BlendMode              = (BlendMode)mMaterials.GetFloat(ShaderID_BlendMode);
                _SurfaceType            = (SurfaceType)mMaterials.GetFloat(ShaderID_SurfaceType);
                _CloseLinearToSRGB      = mMaterials.GetFloat(ShaderID_CloseLinearToSRGB) != 0;
                _QueueID                = mMaterials.GetInt(ShaderID_QueueID);
                _CutOff                 = mMaterials.GetFloat (ShaderID_CutOff);
                _HideButtom             = mMaterials.GetFloat(ShaderID_HideButtom) != 0;

                _AlphaClip              = mMaterials.GetFloat(ShaderID_AlphaClip);
                _ReceiveShadows         = mMaterials.GetFloat(ShaderID_ReceiveShadows);
                _AlphaEnhance           = mMaterials.GetFloat(ShaderID_AlphaEnhance);
            }

            if (mMaterials != null && mMaterials.shader != null)
            {
                if (mMaterials.shader.name != "FB/UI/SGameUIUber" &&
                    mMaterials.shader.name != "FB/UI/SGameUIUber_Stencil")
                {
                    return false;
                }
                return true;
            }

            return false;
        }

        public void Update()
        {
            UnityEngine.Profiling.Profiler.BeginSample("UIUberAnimate SetMaterial");

            if (CheckMaterial())
            {
                mMaterials.SetTexture(ShaderID_MainTex, _MainTex);
                mMaterials.SetTextureScale(ShaderID_MainTex, _MainTexScale);
                mMaterials.SetTextureOffset(ShaderID_MainTex, _MainTexOffset);
                mMaterials.SetColor(ShaderID_Color, _Color);
                mMaterials.SetVector(ShaderID_MainUVPanner, _MainUVPanner);
                mMaterials.SetFloat(ShaderID_MainIntensity, _MainIntensity);
                mMaterials.SetFloat(ShaderID_MainUVDistort, _MainUVDistort ? 1 : 0);
                mMaterials.SetFloat(ShaderID_MainUVXClamp, _MainUVXClamp ? 1 : 0);
                mMaterials.SetFloat(ShaderID_MainUVYClamp, _MainUVYClamp ? 1 : 0);
                mMaterials.SetFloat(ShaderID_MainUV, (float)_MainUV);

                mMaterials.SetFloat(ShaderID_SecondLayer, _SecondLayer ? 1 : 0);
                mMaterials.SetFloat(ShaderID_SecondColorBlend, (float)_SecondColorBlend);
                mMaterials.SetTexture(ShaderID_SecondTex, _SecondTex);
                mMaterials.SetTextureScale(ShaderID_SecondTex, _SecondTexScale);
                mMaterials.SetTextureOffset(ShaderID_SecondTex, _SecondTexOffset);
                mMaterials.SetColor(ShaderID_SecondColor, _SecondColor);
                mMaterials.SetVector(ShaderID_SecondUVPanner, _SecondUVPanner);
                mMaterials.SetFloat(ShaderID_SecondDistort, _SecondDistort ? 1 : 0);
                mMaterials.SetFloat(ShaderID_SecondUVXClamp, _SecondUVXClamp ? 1 : 0);
                mMaterials.SetFloat(ShaderID_SecondUVYClamp, _SecondUVYClamp ? 1 : 0);
                mMaterials.SetFloat(ShaderID_SecondUV, (float)_SecondUV);

                mMaterials.SetFloat(ShaderID_CustomData, _CustomData ? 1 : 0);
                mMaterials.SetFloat(ShaderID_Rim, _Rim ? 1 : 0);
                mMaterials.SetFloat(ShaderID_RimPower, _RimPower);
                mMaterials.SetColor(ShaderID_RimColor, _RimColor);
                mMaterials.SetFloat(ShaderID_Distort, _Distort ? 1 : 0);
                mMaterials.SetTexture(ShaderID_DistortionTex, _DistortionTex);
                mMaterials.SetTextureScale(ShaderID_DistortionTex, _DistortionTexScale);
                mMaterials.SetTextureOffset(ShaderID_DistortionTex, _DistortionTexOffset);
                mMaterials.SetFloat(ShaderID_DistortionIntensity, _DistortionIntensity);
                mMaterials.SetVector(ShaderID_DistortUVPanner, _DistortUVPanner);
                mMaterials.SetFloat(ShaderID_DistortUV, (float)_DistortUV);
                mMaterials.SetFloat(ShaderID_DistortChannel, (float)_DistortChannel);

                mMaterials.SetFloat(ShaderID_Alpha, _Alpha ? 1 : 0);
                mMaterials.SetTexture(ShaderID_AlphaTex, _AlphaTex);
                mMaterials.SetTextureScale(ShaderID_AlphaTex, _AlphaTexScale);
                mMaterials.SetTextureOffset(ShaderID_AlphaTex, _AlphaTexOffset);
                mMaterials.SetVector(ShaderID_AlphaUVPanner, _AlphaUVPanner);
                mMaterials.SetFloat(ShaderID_AlphaDistort, _AlphaDistort ? 1 : 0);
                mMaterials.SetFloat(ShaderID_AlphaUVXClamp, _AlphaUVXClamp ? 1 : 0);
                mMaterials.SetFloat(ShaderID_AlphaUVYClamp, _AlphaUVYClamp ? 1 : 0);
                mMaterials.SetFloat(ShaderID_AlphaUV, (float)_AlphaUV);
                mMaterials.SetFloat(ShaderID_AlphaChannel, (float)_AlphaChannel);

                mMaterials.SetFloat(ShaderID_Diagonal, _Diagonal ? 1 : 0);
                mMaterials.SetFloat(ShaderID_DiagonalX, _DiagonalX);
                mMaterials.SetFloat(ShaderID_DiagonalExtendX, _DiagonalExtendX);
                mMaterials.SetFloat(ShaderID_DiagonalY, _DiagonalY);
                mMaterials.SetFloat(ShaderID_DiagonalExtendY, _DiagonalExtendY);

                mMaterials.SetFloat(ShaderID_Dissolve, _Dissolve ? 1 : 0);
                mMaterials.SetTexture(ShaderID_DissolveTex, _DissolveTex);
                mMaterials.SetTextureScale(ShaderID_DissolveTex, _DissolveTexScale);
                mMaterials.SetTextureOffset(ShaderID_DissolveTex, _DissolveTexOffset);
                mMaterials.SetFloat(ShaderID_DissolveIntensity, _DissolveIntensity);
                mMaterials.SetFloat(ShaderID_DissolveSoft, _DissolveSoft);
                mMaterials.SetFloat(ShaderID_DissolveEdgeWidth, _DissolveEdgeWidth);
                mMaterials.SetColor(ShaderID_DissolveEdgeColor, _DissolveEdgeColor);
                mMaterials.SetVector(ShaderID_DissolveUVPanner, _DissolveUVPanner);
                mMaterials.SetFloat(ShaderID_DissolveUV, (float)_DissolveUV);
                mMaterials.SetFloat(ShaderID_DissolveChannel, (float)_DissolveChannel);

                mMaterials.SetFloat(ShaderID_GradientDissolve, _GradientDissolve ? 1 : 0);
                mMaterials.SetVector(ShaderID_DissolveDirAndSphere, _DissolveDirAndSphere);
                mMaterials.SetFloat(ShaderID_InverseSphere, _InverseSphere ? 1 : 0);
                mMaterials.SetFloat(ShaderID_ObjectScale, _ObjectScale);
                mMaterials.SetFloat(ShaderID_NoiseIntensity, _NoiseIntensity);
                mMaterials.SetVector(ShaderID_VertexOrigin, _VertexOrigin);
                mMaterials.SetFloat(ShaderID_DissolveDistort, _DissolveDistort ? 1 : 0);
                mMaterials.SetFloat(ShaderID_Cull, (float)_Cull);
                mMaterials.SetFloat(ShaderID_ZTest, (float)_ZTest);
                mMaterials.SetFloat(ShaderID_ZWrite, (float)_ZWrite);
                mMaterials.SetFloat(ShaderID_PreZ, _PreZ ? 1 : 0);
                mMaterials.SetFloat(ShaderID_ColorMask, (float)_ColorMask);
                mMaterials.SetFloat(ShaderID_BlendSrc, (float)_BlendSrc);
                mMaterials.SetFloat(ShaderID_BlendDes, (float)_BlendDes);
                mMaterials.SetFloat(ShaderID_BlendMode, (float)_BlendMode);
                mMaterials.SetFloat(ShaderID_SurfaceType, (float)_SurfaceType);
                mMaterials.SetFloat(ShaderID_CloseLinearToSRGB, _CloseLinearToSRGB ? 1 : 0);
                mMaterials.SetInt(ShaderID_QueueID, _QueueID);
                mMaterials.SetFloat(ShaderID_CutOff, _CutOff);
                mMaterials.SetFloat(ShaderID_HideButtom, _HideButtom ? 1 : 0);

                mMaterials.SetFloat(ShaderID_AlphaClip, _AlphaClip);
                mMaterials.SetFloat(ShaderID_ReceiveShadows, _ReceiveShadows);
                mMaterials.SetFloat(ShaderID_AlphaEnhance, _AlphaEnhance);
            }



            UnityEngine.Profiling.Profiler.EndSample();
        }
    }
}
