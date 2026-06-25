using UnityEngine;

public class Knife : MonoBehaviour, IInitializable
{
    public float Damage => _damage;
    public KnifeTrigger KnifeTrigger => _knifeTrigger;
    public KnifeVisual KnifeVisual { get; private set; }

    [SerializeField] private KnifeTrigger _knifeTrigger;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _squeezeScale;

    private float _damage;

    public void Initialize()
    {
        KnifeVisual = new(_spriteRenderer, _squeezeScale);
    }

    public void Reinitialize(KnifeConfig config, Vector2 spawnPosition)
    {
        ResetKnife();
        MoveTo(spawnPosition);
        _damage = config.Damage;
        KnifeVisual.SetSprite(config.Sprite);
        KnifeVisual.PulseAnimation();
    }

    public void MoveTo(Vector2 newPosition) => transform.position = newPosition;

    public void ResetKnife()
    {
        KnifeTrigger.SetStatic(false);
        KnifeTrigger.transform.localPosition = Vector2.zero;

        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector2.one;
    }
}