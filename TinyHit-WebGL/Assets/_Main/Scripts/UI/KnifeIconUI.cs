using UnityEngine;
using UnityEngine.UI;

public class KnifeIconUI
{
    private readonly Image[] _icons;
    private readonly GameData _gameData;

    public KnifeIconUI(Image[] icons, GameData gameData)
    {
        _icons = icons;
        _gameData = gameData;
    }

    public void Initialize() => _gameData.OnKnifeChanged += UpdateKnifeIcons;

    public void Deinitialize() => _gameData.OnKnifeChanged -= UpdateKnifeIcons;

    public void UpdateKnifeIcons(int knifeId)
    {
        Sprite icon = _gameData.GetKnifeConfigById(knifeId).Sprite;

        foreach (var item in _icons)
        {
            if (item == false) continue;

            item.sprite = icon;
        }
    }
}