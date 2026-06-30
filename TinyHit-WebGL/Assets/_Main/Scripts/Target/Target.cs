using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float KnivesRadius => _knivesRadius;
    public Transform ItemsParent => _itemsParent;
    public TargetHealth TargetHealth { get; private set; }
    public TargetRotate TargetRotate { get; private set; }
    public TargetVisual TargetVisual { get; private set; }

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform _itemsParent;
    [SerializeField] private float _damageScale;
    [SerializeField] private float _knivesRadius;
    [SerializeField] private float _applesRadius;

    private ObjectPool<Knife> _staticKnivesPool;
    private ObjectPool<Apple> _applesPool;

    private List<Knife> _activeKnives = new();
    private List<Apple> _activeApples = new();
    private bool _isRotateActive;

    public void Initialize(ObjectPool<Knife> staticKnivesPool, ObjectPool<Apple> applesPool)
    {
        _staticKnivesPool = staticKnivesPool;
        _applesPool = applesPool;

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
        transform.eulerAngles = Vector3.zero;

        TargetHealth.SetHealthStats(statsConfig.MaxHealth);
        TargetRotate.SetRotationSequence(statsConfig.RotationSequence);

        PlaceStaticItems(statsConfig.KnivesAngel, statsConfig.ApplesAngel);
    }

    private void PlaceStaticItems(List<int> knivesAngels, List<int> applesAngels)
    {
        PlacePreinstalledItems(_staticKnivesPool, _activeKnives, knivesAngels, _knivesRadius, 270);
        PlacePreinstalledItems(_applesPool, _activeApples, applesAngels, _applesRadius, 90);

        foreach (var knife in _activeKnives)
            knife.KnifeTrigger.SetStatic(true);
    }

    private void PlacePreinstalledItems<T>(ObjectPool<T> objectPool, List<T> activeItems, List<int> itemAngles, float radius, float rotationOffset = 0.0f) where T : MonoBehaviour
    {
        ReturnItemsToPool(objectPool, activeItems);

        foreach (var angle in itemAngles)
        {
            var item = objectPool.GetItem();

            float radians = angle * Mathf.Deg2Rad;
            float positionX = transform.position.x + (radius * Mathf.Cos(radians));
            float positionY = transform.position.y + (radius * Mathf.Sin(radians));

            item.transform.SetParent(_itemsParent);
            item.transform.position = new(positionX, positionY);

            Vector2 direction = (transform.position - item.transform.position).normalized;
            float angel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            item.transform.eulerAngles = new(0, 0, angel + rotationOffset);

            activeItems.Add(item);
        }
    }
    public void ReturnItemsToPool<T>(ObjectPool<T> objectPool, List<T> activeItems) where T : MonoBehaviour
    {
        if (activeItems.Count <= 0) return;

        foreach (var item in activeItems)
            objectPool.ReturnItem(item);

        activeItems.Clear();
    }

    public void SetRotateActive(bool isRotateActive) => _isRotateActive = isRotateActive;

    private void Update()
    {
        if (_isRotateActive == false) return;

        TargetRotate.Update();
    }

    private void OnDestroy() => Deinitilize();
}