using UnityEngine;

public class Knife : MonoBehaviour, IInitializable, IResetable
{
    public float Damage { get; private set; }
    public KnifeTrigger KnifeTrigger => _knifeTrigger;
    public KnifeVisual KnifeVisual { get; private set; }

    [SerializeField] private KnifeTrigger _knifeTrigger;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _squeezeScale;

    public void Initialize() => KnifeVisual = new(_spriteRenderer, _squeezeScale);

    public void Reinitialize(KnifeConfig config)
    {
        ResetItem();
        Damage = config.Damage;
        KnifeVisual.SetSprite(config.Sprite);
        KnifeVisual.PulseAnimation();
    }

    public void MoveTo(Vector2 newPosition) => transform.position = newPosition;

    public void ResetItem()
    {
        KnifeTrigger.SetStatic(false);
        KnifeVisual.StopAnimation();

        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
}