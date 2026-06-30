using PrimeTween;
using System;
using System.Collections.Generic;

public class KnifeService
{
    public event Action OnKnifeHit;
    public event Action OnHitFail;

    private readonly InputArea _inputArea;
    private readonly ObjectPool<Knife> _knifePool;
    private readonly TargetService _targetService;
    private readonly GameData _gameData;

    private List<Knife> _activeKnives = new();
    private Knife _currentKnife;
    private Tween _currentTween;

    private bool _isThrowActive = true;

    public KnifeService(InputArea inputArea, GameData gameData, ObjectPool<Knife> knifePool, TargetService targetService)
    {
        _inputArea = inputArea;
        _gameData = gameData;
        _knifePool = knifePool;
        _targetService = targetService;
    }

    public void Initialize() => _inputArea.OnClick += ThrowKnife;

    public void Deinitialize() => _inputArea.OnClick -= ThrowKnife;

    public void SpawnKnife()
    {
        if (_currentKnife != null)
            _knifePool.ReturnItem(_currentKnife);

        _currentKnife = _knifePool.GetItem();

        _activeKnives.Add(_currentKnife);
        _currentKnife.Reinitialize(_gameData.CurrentKnifeConfig);
        _currentKnife.MoveTo(_knifePool.Parent.position);

        _currentKnife.KnifeTrigger.OnHitFail += FailHit;
        _currentKnife.KnifeTrigger.OnAppleHit += OnAppleHit;

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
        if (_isThrowActive == false || _currentKnife == null) return;

        _isThrowActive = false;

        _currentKnife.KnifeVisual.SqueezeAnimation(0.1f);
        _currentTween = Tween.Position(_currentKnife.transform, _targetService.GetKnifePositionOnTarget(), 0.1f)
            .OnComplete(OnThrowComplete);
    }

    private void OnThrowComplete()
    {
        OnKnifeHit?.Invoke();
        _targetService.HitTarget(_currentKnife);
        _currentKnife.KnifeTrigger.OnHitFail -= FailHit;
        _currentKnife.KnifeTrigger.OnAppleHit -= OnAppleHit;
        _currentKnife = null;
        Tween.Delay(0.01f, SpawnKnife);
    }

    private void OnAppleHit(Apple apple)
    {
        apple.PickUp();
        _gameData.Money += apple.Worth;
    }

    private void FailHit()
    {
        _currentKnife.KnifeTrigger.OnHitFail -= FailHit;
        _currentKnife.KnifeTrigger.OnAppleHit -= OnAppleHit;
        _currentKnife.KnifeVisual.DeathAnimation();
        _currentTween.Stop();
        OnHitFail?.Invoke();
    }
}