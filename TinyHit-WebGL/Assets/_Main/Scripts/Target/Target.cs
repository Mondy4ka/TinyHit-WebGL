using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float KnifeDepth => _knifeDepth;
    public Transform KnivesParent => _knivesParent;
    public TargetHealth TargetHealth { get; private set; }
    public TargetRotate TargetRotate { get; private set; }
    public TargetVisual TargetVisual { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _knivesParent;
    [SerializeField] private float _damageScale;
    [SerializeField] private float _knifeDepth;

    private ObjectPool<Knife> _staticKnivesPool;
    private List<Knife> _activeKnives = new();
    private bool _isRotateActive;

    public void Initialize(ObjectPool<Knife> staticKnivesPool)
    {
        _staticKnivesPool = staticKnivesPool;

        TargetHealth = new();
        TargetRotate = new(transform);
        TargetVisual = new(_spriteRenderer, _damageScale);

        TargetHealth.OnHealthChanged += (c, m) => TargetVisual.DamageAnimation();
        TargetHealth.OnDeath += () => TargetVisual.DeathAnimation(() => SetRotateActive(false));
    }

    public void Deinitilize()
    {
        TargetHealth.OnHealthChanged -= (c, m) => TargetVisual.DamageAnimation();
        TargetHealth.OnDeath -= () => TargetVisual.DeathAnimation(() => SetRotateActive(false));
    }

    public void SetStats(TargetConfig statsConfig)
    {
        TargetHealth.SetHealthStats(statsConfig.MaxHealth);
        TargetRotate.SetRotationSequence(statsConfig.RotationSequence);

        PlaceStaticKnives(statsConfig.KnivesAngel);
    }

    private void PlaceStaticKnives(List<int> knivesAngels)
    {
        ReturnKnives();

        for (int i = 0; i < knivesAngels.Count; i++)
        {
            float rad = knivesAngels[i] * Mathf.Deg2Rad;
            float posX = transform.position.x + (1 / _knifeDepth * Mathf.Cos(rad));
            float posY = transform.position.y + (1 / _knifeDepth * Mathf.Sin(rad));

            Knife knife = _staticKnivesPool.GetItem();

            knife.KnifeTrigger.transform.SetParent(knife.transform);
            knife.KnifeTrigger.transform.localPosition = Vector2.zero;
            knife.KnifeTrigger.transform.localRotation = Quaternion.identity;

            knife.transform.SetParent(KnivesParent);
            knife.MoveTo(new(posX, posY));
            knife.KnifeTrigger.SetStatic(true);

            Vector2 direction = (transform.position - knife.transform.position).normalized;
            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            knife.transform.Rotate(0, 0, angel - 90);


            _activeKnives.Add(knife);
        }
    }

    public void ReturnKnives()
    {
        if (_activeKnives.Count <= 0) return;

        foreach (Knife knife in _activeKnives)
        {
            _staticKnivesPool.ReturnItem(knife);

            knife.ResetKnife();
            knife.KnifeTrigger.SetStatic(false);
        }

        _activeKnives.Clear();
    }

    public void SetRotateActive(bool isRotateActive) => _isRotateActive = isRotateActive;

    private void Update()
    {
        if (_isRotateActive == false) return;

        TargetRotate.Update();
    }

    private void OnDestroy() => Deinitilize();
}