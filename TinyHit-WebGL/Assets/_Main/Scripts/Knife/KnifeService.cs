using PrimeTween;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KnifeService
{
    public event Action OnKnifeHit;
    public event Action OnHitFail;

    private readonly InputArea _inputArea;
    private readonly Transform _spawnPoint;

    private readonly ObjectPool<Knife> _knifePool;
    private readonly List<Knife> _activeKnives = new();

    private readonly TargetService _targetService;

    private KnifeConfig _currentKnifeType;
    private Knife _curentKnife;
    private bool _isThrowActive = true;
    private Tween _currentTween;

    public KnifeService(InputArea inputArea, Knife knifePrefab, int poolSize, Transform spawnPoint, TargetService targetService)
    {
        _inputArea = inputArea;
        _spawnPoint = spawnPoint;
        _targetService = targetService;

        _knifePool = new(knifePrefab, spawnPoint);
        _knifePool.Initialize(poolSize);
    }

    public void Initialize() => _inputArea.OnClick += ThrowKnife;

    public void Deinitialize() => _inputArea.OnClick -= ThrowKnife;

    public void SetKnifeType(KnifeConfig knifeConfig) => _currentKnifeType = knifeConfig;

    public void SpawnKnife()
    {
        _curentKnife = _knifePool.GetItem();

        _curentKnife.KnifeTrigger.OnHitFail += FailHit;
        _curentKnife.Reinitialize(_currentKnifeType, _spawnPoint.position);
        _activeKnives.Add(_curentKnife);

        _isThrowActive = true;
    }

    public void ReturnKnives()
    {
        if (_activeKnives.Count <= 0) return;

        foreach (Knife knife in _activeKnives)
            _knifePool.ReturnItem(knife);

        _activeKnives.Clear();
    }

    private void ThrowKnife()
    {
        if (_isThrowActive == false || _curentKnife == null) return;

        _isThrowActive = false;

        _curentKnife.KnifeVisual.SqueezeAnimation(0.1f);
        _currentTween = Tween.Position(_curentKnife.transform, _targetService.GetKnifePositionOnTarget(), 0.1f)
            .OnComplete(() =>
            {
                OnKnifeHit?.Invoke();
                _curentKnife.KnifeTrigger.OnHitFail -= FailHit;
                _targetService.HitTarget(_curentKnife);
                _curentKnife = null;
                SpawnKnife();
            });
    }

    private void FailHit()
    {
        _curentKnife.KnifeTrigger.OnHitFail -= FailHit;
        _currentTween.Stop();
        Tween.Position(_curentKnife.transform, new Vector2(0, -30), 5f);
        OnHitFail?.Invoke();
    }
}