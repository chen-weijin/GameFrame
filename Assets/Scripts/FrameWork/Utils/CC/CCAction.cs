using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[UnityEngine.Scripting.Preserve]
public class CCAction
{
    private enum EType
    {
        // 按顺序播放动画
        Sequence,

        // 同时播放动画
        Spawn,
    }

    private Sequence _sequence;
    private Sequence _tmpSpawn;
    private Transform _transform;
    private RectTransform _rectTransform;

    //private EType           _type;

    /// <summary>
    /// DOTween程序动画简略写法(包括2D和3D共用)
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public static CCAction Create(Transform transform)
    {
        return new CCAction(transform);
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction(Transform transform)
    {
        _sequence = DOTween.Sequence();
        _transform = transform;
        _rectTransform = transform as RectTransform;
        _tmpSpawn = null;
    }

    /// <summary>
    /// 切换成组合动画模式，表示后续新增动画为同时播放
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction BeginSpawn()
    {
        Debug.Assert(_tmpSpawn == null, "禁止BeginSpawn内在套BeginSpawn");
        _tmpSpawn = DOTween.Sequence();
        return this;
    }

    /// <summary>
    /// 对应上面，结束组合动画模式，还原为顺序模式
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction EndSpawn()
    {
        if (_tmpSpawn != null)
        {
            _sequence.Append(_tmpSpawn);
            _tmpSpawn = null;
        }
        else
        {
            Debug.LogError("调用EndSpawn前未调用BeginSpawn");
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction MoveTo(float duration,
        Vector2 pos,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOAnchorPos(pos, duration)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalMove(pos, duration)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction MoveTo(float duration,
        Vector3 pos,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            // RectTransform要用DOAnchorPos否则动画可能不对
            var tweener = _rectTransform.DOAnchorPos(pos, duration)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalMove(pos, duration)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction MoveBy(float duration,
        Vector2 pos,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            // RectTransform要用DOAnchorPos否则动画可能不对
            var tweener = _rectTransform.DOAnchorPos(pos, duration)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalMove(pos, duration)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction MoveBy(float duration,
        Vector3 pos,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            // RectTransform要用DOAnchorPos否则动画可能不对
            var tweener = _rectTransform.DOAnchorPos(pos, duration)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalMove(pos, duration)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction JumpTo(float duration,
        Vector2 pos,
        float power,
        int numJumps,
        bool snapping = false,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOJumpAnchorPos(pos, power, numJumps, duration, snapping)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalJump(pos, power, numJumps, duration, snapping)
               .SetEase(easeType);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction JumpTo(float duration,
        Vector3 pos,
        float power,
        int numJumps,
        bool snapping = false,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOJumpAnchorPos(pos, power, numJumps, duration, snapping)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalJump(pos, power, numJumps, duration, snapping)
                .SetEase(easeType);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction JumpBy(float duration,
        Vector2 pos,
        float power,
        int numJumps,
        bool snapping = false,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOJumpAnchorPos(pos, power, numJumps, duration, snapping)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalJump(pos, power, numJumps, duration, snapping)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction JumpBy(float duration,
        Vector3 pos,
        float power,
        int numJumps,
        bool snapping = false,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOJumpAnchorPos(pos, power, numJumps, duration, snapping)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOLocalJump(pos, power, numJumps, duration, snapping)
                .SetEase(easeType)
                .SetRelative(true);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ScaleTo(float duration,
        float scale,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOScale(cc.p3(scale, scale, scale), duration)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ScaleTo(float duration,
        float sx,
        float sy,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOScale(cc.p(sx, sy), duration)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ScaleTo(float duration,
        float sx,
        float sy,
        float sz,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOScale(cc.p3(sx, sy, sz), duration)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ScaleBy(float duration,
        float scale,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOScale(cc.p3(scale, scale, scale), duration)
            .SetEase(easeType)
            .SetRelative(true);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ScaleBy(float duration,
        float sx,
        float sy,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOScale(cc.p(sx, sy), duration)
            .SetEase(easeType)
            .SetRelative(true);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ScaleBy(float duration,
        float sx,
        float sy,
        float sz,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOScale(cc.p3(sx, sy, sz), duration)
            .SetEase(easeType)
            .SetRelative(true);
        _PushTween(tweener);
        return this;
    }

    /// <summary>
    /// 旋转z轴，2D旋转
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="rotate"></param>
    /// <param name="easeType"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction RotateTo(float duration,
        float rotate,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var r = _transform.localEulerAngles;
        var tweener = _transform.DOLocalRotate(cc.p3(r.x, r.y, rotate), duration, RotateMode.FastBeyond360)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction RotateTo(float duration,
        Vector3 rotate,
        RotateMode mode = RotateMode.FastBeyond360,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOLocalRotate(rotate, duration, mode)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    /// <summary>
    /// UI旋转
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="rotate"></param>
    /// <param name="easeType"></param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction RotateBy(float duration,
        float rotate,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var r = _transform.localEulerAngles;
        var tweener = _transform.DOLocalRotate(cc.p3(r.x, r.y, rotate), duration, RotateMode.LocalAxisAdd)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction RotateBy(float duration,
        Vector3 rotate,
        RotateMode mode = RotateMode.LocalAxisAdd,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOLocalRotate(rotate, duration, mode)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction RotateQuaternion(float duration,
        Quaternion quat,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var tweener = _transform.DOLocalRotateQuaternion(quat, duration)
            .SetEase(easeType);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction TintTo(float duration,
        Color color,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        var images = _transform.GetComponentsInChildren<Graphic>();
        for (int j = 0; j < images.Length; j++)
        {
            Tween tweener = images[j].DOColor(color, duration).SetEase(easeType);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction FadeIn(float duration,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        _DOFade(duration, 1, easeType);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction FadeOut(float duration,
        DG.Tweening.Ease easeType = DG.Tweening.Ease.Linear)
    {
        _DOFade(duration, 0, easeType);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction CallFunc(Action callback)
    {
        _sequence.AppendCallback(() =>
        {
            callback.Invoke();
        });
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction DelayTime(float time)
    {
        _sequence.AppendInterval(time);
        return this;
    }

    /// <summary>
    /// 打拳反冲效果
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="punch"> 强度 </param>
    /// <param name="vibrato"> 震动次数 </param>
    /// <param name="elasticity"> 反弹系数：0表示无，1表示反弹punch </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction PunchPosition(float duration,
        Vector2 punch,
        int vibrato = 10,
        float elasticity = 1)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOPunchAnchorPos(punch, duration, vibrato, elasticity);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOPunchPosition(punch, duration, vibrato, elasticity);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction PunchPosition(float duration,
        Vector3 punch,
        int vibrato = 10,
        float elasticity = 1)
    {
        var tweener = _transform.DOPunchPosition(punch, duration, vibrato, elasticity);
        _PushTween(tweener);
        return this;
    }

    /// <summary>
    /// 打拳反冲效果
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="punch"> 强度 </param>
    /// <param name="vibrato"> 震动次数 </param>
    /// <param name="elasticity"> 反弹系数：0表示无，1表示反弹punch </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction PunchRotation(float duration,
        float rotate,
        int vibrato = 10,
        float elasticity = 1)
    {
        var r = _transform.localEulerAngles;
        var tweener = _transform.DOPunchRotation(cc.p3(r.x, r.y, rotate), duration, vibrato, elasticity);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction PunchRotation(float duration,
        Vector3 punch,
        int vibrato = 10,
        float elasticity = 1)
    {
        var tweener = _transform.DOPunchRotation(punch, duration, vibrato, elasticity);
        _PushTween(tweener);
        return this;
    }

    /// <summary>
    /// 打拳反冲效果
    /// </summary>
    /// <param name="duration"></param>
    /// <param name="punch"> 强度 </param>
    /// <param name="vibrato"> 震动次数 </param>
    /// <param name="elasticity"> 反弹系数：0表示无，1表示反弹punch </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction PunchScale(float duration,
        Vector2 punch,
        int vibrato = 10,
        float elasticity = 1)
    {
        var tweener = _transform.DOPunchScale(punch, duration, vibrato, elasticity);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction PunchScale(float duration,
        Vector3 punch,
        int vibrato = 10,
        float elasticity = 1)
    {
        var tweener = _transform.DOPunchScale(punch, duration, vibrato, elasticity);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ShakeScale(float duration,
        float strength,
        int vibrato = 10,
        int random = 1)
    {
        var tweener = _transform.DOShakeScale(duration, strength, vibrato, random);
        _PushTween(tweener);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ShakePosition(float duration,
        float strength,
        int vibrato = 10,
        int random = 1)
    {
        if (_rectTransform != null)
        {
            var tweener = _rectTransform.DOShakeAnchorPos(duration, strength, vibrato, random);
            _PushTween(tweener);
        }
        else
        {
            var tweener = _transform.DOShakePosition(duration, strength, vibrato, random);
            _PushTween(tweener);
        }
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction ShakeRotate(float duration,
        float strength,
        int vibrato = 10,
        int random = 1)
    {
        var tweener = _transform.DOShakeRotation(duration, strength, vibrato, random);
        _PushTween(tweener);
        return this;
    }

    /// <summary>
    /// 重复次数
    /// </summary>
    /// <param name="loop"> 重复次数 </param>
    /// <param name="tp"> 重复类型，默认重新开始 </param>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public CCAction Repeat(int loop, LoopType tp = LoopType.Restart)
    {
        _sequence.SetLoops(loop, tp);
        return this;
    }

    [UnityEngine.Scripting.Preserve]
    public CCAction RepeatForever(LoopType tp = LoopType.Restart)
    {
        _sequence.SetLoops(-1, tp);
        return this;
    }

    /// <summary>
    /// 开始播放动画
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public CCAction Run()
    {
        _sequence.SetLink(_transform.gameObject);
        _sequence.Play();
        return this;
    }

    /// <summary>
    /// 开始播放动画，带结束回调
    /// </summary>
    /// <param name="finishCB"></param>
    [UnityEngine.Scripting.Preserve]
    public CCAction Run(TweenCallback finishCB)
    {
        _sequence.onComplete = finishCB;
        _sequence.SetLink(_transform.gameObject);
        _sequence.Play();
        return this;
    }

    /// <summary>
    /// 中断动画
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public void Stop()
    {
        _sequence.SetRecyclable(false);
        _sequence.Kill();
    }

    /// <summary>
    /// 默认动画播完就删了，需要先调用Recyclable，调用Restart才能生效
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public void Restart()
    {
        _sequence.Restart();
    }

    /// <summary>
    /// 设置动画播完不删除，回收接着用。默认自动删除
    /// 可以使用Stop或者DOTween.Clear全部清除(需要确保当前没有正在运行的Dotween)
    /// </summary>
    [UnityEngine.Scripting.Preserve]
    public CCAction Recyclable()
    {
        _sequence.SetRecyclable(true);
        _sequence.SetAutoKill(false);
        return this;
    }

    /// <summary>
    /// 预留接口
    /// </summary>
    /// <returns></returns>
    [UnityEngine.Scripting.Preserve]
    public Sequence GetSequence()
    {
        return _sequence;
    }

    private void _DOFade(float duration, float value, DG.Tweening.Ease easeType)
    {
        var canvsGroup = _transform.GetComponent<CanvasGroup>();
        if (canvsGroup != null)
        {
            Tween tweener = canvsGroup.DOFade(value, duration).SetEase(easeType);
            _PushTween(tweener);
            return;
        }

        var images = _transform.GetComponentsInChildren<Graphic>();
        for (int j = 0; j < images.Length; j++)
        {
            Tween tweener = images[j].DOFade(value, duration).SetEase(easeType);
            _PushTween(tweener);
        }
    }

    private void _PushTween(Tween tween)
    {
        if (_tmpSpawn != null)
        {
            _tmpSpawn.Join(tween);
        }
        else
        {
            _sequence.Append(tween);
        }
    }

    private CCAction()
    {
    }

}
