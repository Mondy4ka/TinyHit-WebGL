using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    [SerializeField] private Image _knifeIcon;
    [SerializeField] private TMP_Text _knifeNameText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _damageText;

    [SerializeField] private InputArea _buyButton;
    [SerializeField] private TMP_Text _buyButtonText;

    public void SetActiveCanvas(bool isActive) => _canvas.enabled = isActive;

    public void SetKnifeInfo(KnifeConfig config)
    {
        _knifeIcon.sprite = config.Sprite;
        _knifeNameText.SetText(config.Name);
        _priceText.SetText($"PRICE: {config.Price}");
        _damageText.SetText($"DAMAGE: {config.Damage}");
    }

    public void InitializeBuyButton(Action onClick, string text)
    {
        _buyButtonText.SetText(text);

        _buyButton.ResetAction();
        _buyButton.OnClick += onClick;
    }
}