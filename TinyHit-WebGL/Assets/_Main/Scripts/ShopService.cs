public class ShopService
{
    private readonly ShopScreen _shopScreen;
    private readonly GameData _gameData;

    private readonly InputArea _switchShopPrevArea;
    private readonly InputArea _switchShopNextArea;

    private int _currentKnife = 0;

    public ShopService(ShopScreen shopScreen, GameData gameData, InputArea switchShopPrevArea, InputArea switchShopNextArea)
    {
        _shopScreen = shopScreen;
        _gameData = gameData;
        _switchShopPrevArea = switchShopPrevArea;
        _switchShopNextArea = switchShopNextArea;
    }

    public void Intialize()
    {
        _switchShopNextArea.OnClick += NextKnife;
        _switchShopPrevArea.OnClick += PreviousKnife;
    }

    public void Deintialize()
    {
        _switchShopNextArea.OnClick -= NextKnife;
        _switchShopPrevArea.OnClick -= PreviousKnife;
    }

    public void NextKnife()
    {
        _currentKnife = (_currentKnife + 1) % _gameData.KnifeConfigs.Count;

        UpdateKnifePage();
    }

    public void PreviousKnife()
    {
        _currentKnife = (_currentKnife - 1 + _gameData.KnifeConfigs.Count) % _gameData.KnifeConfigs.Count;

        UpdateKnifePage();
    }

    public void UpdateKnifePage()
    {
        var config = _gameData.KnifeConfigs[_currentKnife];

        _shopScreen.SetKnifeInfo(config);

        if (_gameData.IsKnifeUnlocked(config.Id))
        {
            if (_gameData.CurrentKnifeId == config.Id)
            {
                _shopScreen.InitializeBuyButton(null, "Selected");
            }
            else
            {
                _shopScreen.InitializeBuyButton(() => SetKnife(config), "Select");
            }
        }
        else
        {
            _shopScreen.InitializeBuyButton(() => BuyKnife(config), "Buy");
        }
    }

    public void BuyKnife(KnifeConfig config)
    {
        if (_gameData.Money < config.Price) return;

        _gameData.Money -= config.Price;
        _gameData.TryUnlockedKnife(config.Id);

        UpdateKnifePage();
    }

    public void SetKnife(KnifeConfig config)
    {
        if (_gameData.IsKnifeUnlocked(config.Id) == false || _gameData.CurrentKnifeId == config.Id) return;

        _gameData.CurrentKnifeId = config.Id;

        UpdateKnifePage();
    }
}