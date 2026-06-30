using TMPro;

public class MoneyUI
{
    private readonly TMP_Text[] _moneyTexts;
    private readonly GameData _gameData;

    public MoneyUI(TMP_Text[] moneyTexts, GameData gameData)
    {
        _moneyTexts = moneyTexts;
        _gameData = gameData;
    }

    public void Initialize() => _gameData.OnMoneyChanged += UpdateMoneyTexts;

    public void Deinitialize() => _gameData.OnMoneyChanged -= UpdateMoneyTexts;

    public void UpdateMoneyTexts(int money)
    {
        foreach (var moneyText in _moneyTexts)
        {
            if (moneyText == null) continue;

            moneyText.SetText(money.ToString());
        }
    }
}