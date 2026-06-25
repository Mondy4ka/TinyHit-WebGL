using UnityEngine;

public class UIService : MonoBehaviour
{
    public FadeScreen FadeScreen => _fadeScreen;
    public GameOverScreen GameOverScreen => _gameOverScreen;
    public MenuScreen MenuScreen => _menuScreen;
    public GameScreen GameScreen => _gameScreen;

    [SerializeField] private UISettings _uiSettings;

    [SerializeField] private FadeScreen _fadeScreen;
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private GameScreen _gameScreen;

    public void Initialize(ScoreService scoreService, Target target)
    {
        _fadeScreen.Initialize(_uiSettings);
        _gameOverScreen.Initialize(_uiSettings);
        _menuScreen.Initialize(_uiSettings, scoreService);
        _gameScreen.Initialize(scoreService, target);
    }
}