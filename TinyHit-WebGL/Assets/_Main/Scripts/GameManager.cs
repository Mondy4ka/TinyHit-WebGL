using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameState GameState { get; private set; }

    public TargetService TargetService { get; private set; }
    public KnifeService KnifeService { get; private set; }

    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private Target _target;
    [SerializeField] private Transform _knifeSpawnPoint;
    [SerializeField] private InputService _inputService;
    [SerializeField] private UIService _uiService;

    private ScoreService _scoreService;

    public void Awake()
    {
        _scoreService = new();

        TargetService = new(_target, _gameSettings.TargetConfigs, _gameSettings.KnifePrefab);
        KnifeService = new(_inputService.ThrowArea, _gameSettings.KnifePrefab, _gameSettings.KnifePoolSize, _knifeSpawnPoint, TargetService);

        KnifeService.Initialize();
        TargetService.Initialize();
        _uiService.Initialize(_scoreService, _target);

        _inputService.StartArea.OnClick += StartRun;
        _inputService.RestartArea.OnClick += RestartRun;
        _inputService.MenuArea.OnClick += Menu;
        TargetService.Target.TargetHealth.OnDeath += SwitchStage;
        KnifeService.OnHitFail += GameOver;
        KnifeService.OnKnifeHit += () => _scoreService.AddScore(1);

        _uiService.MenuScreen.SetCanvas(true);
        _uiService.FadeScreen.SetCanvas(false);
        _uiService.GameScreen.SetCanvas(false);
        _uiService.GameOverScreen.SetCanvas(false);
        _uiService.MenuScreen.TipPulse();
    }

    private void RestartRun()
    {
        GameState = GameState.Playing;

        _inputService.SetActiveReastartInputArea(false);
        _inputService.SetActiveMenuInputArea(false);
        TargetService.ReinitializeTarget();
        _scoreService.Reset();
        _uiService.GameScreen.UpdateStageNameUI(_scoreService.Stage);

        _uiService.GameOverScreen.Close(() =>
            {
                KnifeService.SpawnKnife();
                _inputService.SetActiveThrowInputArea(true);
            });
    }

    private void StartRun()
    {
        GameState = GameState.Playing;

        _inputService.SetActiveStartInputArea(false);
        TargetService.ReinitializeTarget();
        _scoreService.Reset();
        _uiService.GameScreen.UpdateStageNameUI(_scoreService.Stage);

        _uiService.MenuScreen.Close(() =>
            {
                KnifeService.SpawnKnife();
                _inputService.SetActiveThrowInputArea(true);
                _uiService.MenuScreen.SetCanvas(false);
                _uiService.GameScreen.SetCanvas(true);
            });
    }

    private void SwitchStage()
    {
        _inputService.SetActiveThrowInputArea(false);

        _scoreService.NextStage();

        _uiService.FadeScreen.SetCanvas(true);
        _uiService.FadeScreen.SetText(_scoreService.Stage);

        _uiService.FadeScreen.Close(() =>
        {
            _uiService.GameScreen.UpdateStageNameUI(_scoreService.Stage);
            KnifeService.ReturnKnives();
            KnifeService.SpawnKnife();
            TargetService.ReinitializeTarget();
            _uiService.FadeScreen.Open(() =>
            {
                _inputService.SetActiveThrowInputArea(true);
                _uiService.FadeScreen.SetCanvas(false);
            });
        });
    }

    public void GameOver()
    {
        GameState = GameState.GameOver;

        _uiService.FadeScreen.SetCanvas(false);
        _uiService.GameOverScreen.SetCanvas(true);

        _inputService.SetActiveThrowInputArea(false);

        _uiService.GameOverScreen.SetRunResults(_scoreService.Score, _scoreService.Stage);
        _uiService.GameOverScreen.Open(() =>
        {
            KnifeService.ReturnKnives();
            _inputService.SetActiveReastartInputArea(true);
            _inputService.SetActiveMenuInputArea(true);
        });
    }

    public void Menu()
    {
        GameState = GameState.Menu;

        _inputService.SetActiveReastartInputArea(false);
        _inputService.SetActiveMenuInputArea(false);

        _uiService.GameOverScreen.Close();
        _uiService.MenuScreen.Open(() =>
        {
            _inputService.SetActiveStartInputArea(true);
            _uiService.GameOverScreen.SetCanvas(false);
        });
    }
}

public enum GameState
{
    Menu,
    Playing,
    GameOver
}