using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    public FadeScreen FadeScreen => _fadeScreen;
    public GameOverScreen GameOverScreen => _gameOverScreen;
    public MenuScreen MenuScreen => _menuScreen;
    public GameScreen GameScreen => _gameScreen;
    public ShopScreen ShopScreen => _shopScreen;
    public MoneyUI MoneyUI { get; private set; }
    public KnifeIconUI KnifeIconUI { get; private set; }

    [SerializeField] private UISettings _uiSettings;

    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private GameScreen _gameScreen;
    [SerializeField] private ShopScreen _shopScreen;
    [SerializeField] private TMP_Text[] _moneyTexts;
    [SerializeField] private Image[] _knifeIcons;

    public void Initialize(ScoreService scoreService, GameData gameData, Target target)
    {
        MoneyUI = new(_moneyTexts, gameData);
        KnifeIconUI = new(_knifeIcons, gameData);

        _fadeScreen.Initialize(_uiSettings);
        _gameOverScreen.Initialize(_uiSettings);
        _menuScreen.Initialize(_uiSettings, gameData);
        _gameScreen.Initialize(scoreService, target);
        MoneyUI.Initialize();
        KnifeIconUI.Initialize();
    }
}