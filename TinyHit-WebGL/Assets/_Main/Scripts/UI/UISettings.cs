using PrimeTween;
using UnityEngine;

[CreateAssetMenu(fileName = "UISettings", menuName = "UISettings")]
public class UISettings : ScriptableObject
{
    [Header("==== FadeScreen Settings ====")]
    [Header("Animation Duration Settings")]
    public float FadeScreenOpenDuration;
    public float FadeScreenCloseDuration;

    [Header("Animation Easing Settings")]
    public Ease FadeScreenOpenEasing;
    public Ease FadeScreenCloseEasing;

    [Header("Chunk1 Settings")]
    public Vector2 FadeScreenOpenPosition1;
    public Vector2 FadeScreenClosePosition1;

    [Header("Chunk2 Settings")]
    public Vector2 FadeScreenOpenPosition2;
    public Vector2 FadeScreenClosePosition2;

    //============================================

    [Header("==== GameOverScreen Settings ====")]
    [Header("Background Settings")]
    public float GameOverBackgroundAlpha;

    [Header("GameOver Panel Settings")]
    public Vector2 GameOverOpenPosition;
    public Vector2 GameOverClosePosition;

    [Header("Animation Duration Settings")]
    public float GameOverOpenDuration;
    public float GameOverCloseDuration;

    [Header("Animation Easing Settings")]
    public Ease GameOverOpenEasing;
    public Ease GameOverCloseEasing;

    //============================================

    [Header("==== MenuScreen Settings ====")]
    [Header("Animation Duration Settings")]
    public float MenuScreenOpenDuration;
    public float MenuScreenCloseDuration;
    public float MenuScreenTipPulseDuration;

    [Header("Animation Easing Settings")]
    public Ease MenuScreenOpenEasing;
    public Ease MenuScreenCloseEasing;
    public Ease MenuScreenPulseEasing;

    [Header("Chunk1 Settings")]
    public Vector2 MenuScreenOpenPosition1;
    public Vector2 MenuScreenClosePosition1;

    [Header("Chunk2 Settings")]
    public Vector2 MenuScreenOpenPosition2;
    public Vector2 MenuScreenClosePosition2;
}
