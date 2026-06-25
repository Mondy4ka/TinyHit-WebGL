using System.Collections.Generic;
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

    private InputArea _buyArea;
    private KnifeService _knifeService;
    private List<KnifeConfig> _knivesUnlocked;
    private int _currentKnifeIndex;

    public void Initialize(List<KnifeConfig> knivesUnlocked)
    {
        _knivesUnlocked = knivesUnlocked;
    }

    public void NextKnife()
    {
        _currentKnifeIndex = (_currentKnifeIndex + 1) % _knivesUnlocked.Count;
        SetKnifeInfo(_knivesUnlocked[_currentKnifeIndex]);
        InitializeBuyButton();
    }

    public void PreviousKnife()
    {
        _currentKnifeIndex = (_currentKnifeIndex - 1 + _knivesUnlocked.Count) % _knivesUnlocked.Count;
        SetKnifeInfo(_knivesUnlocked[_currentKnifeIndex]);
        InitializeBuyButton();
    }

    public void SetKnifeInfo(KnifeConfig config)
    {
        _knifeIcon.sprite = config.Sprite;
        _knifeNameText.SetText(config.Name);
        _priceText.SetText(config.Price.ToString());
        _damageText.SetText(config.Damage.ToString());
    }

    public void InitializeBuyButton()
    {
        _buyArea.OnClick += () => _knifeService.SetKnifeType(_knivesUnlocked[_currentKnifeIndex]);
    }
}
