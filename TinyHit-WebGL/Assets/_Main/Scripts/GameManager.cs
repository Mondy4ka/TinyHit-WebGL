using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    [SerializeField] private InputService _inputService;
    [SerializeField] private UIService _uiService;

    private TargetService _targetService;
    private KnifeService _knifeService;
    private ScoreService _scoreService;
    private ShopService _shopService;
    private GameData _gameData;

    [SerializeField] private Target _target;

    [SerializeField] private Transform _throwKnivesSpawnPoint;
    [SerializeField] private Transform _targetKnivesSpawnPoint;
    [SerializeField] private Transform _targetAppleSpawnPoint;

    private ObjectPool<Knife> _throwKnifePool;
    private ObjectPool<Knife> _targetKnifePool;
    private ObjectPool<Apple> _targetApplePool;

    public void Awake()
    {
        _gameData = new(_gameSettings.KnifeConfigs);
        InitializePools();
        InitializeServices();

        _inputService.StartArea.OnClick += StartRun;
        _inputService.RestartArea.OnClick += RestartRun;
        _inputService.MenuArea.OnClick += Menu;
        _inputService.ShopArea.OnClick += Shop;
        _inputService.ShopToMenuArea.OnClick += ShopToMenu;

        _targetService.Target.TargetHealth.OnDeath += SwitchStage;
        _knifeService.OnHitFail += GameOver;
        _knifeService.OnKnifeHit += () => _scoreService.AddScore(1);

        _gameData.SetData(0, 0, 0, 0, new() { 0 });
        _uiService.MenuScreen.TipPulse();
        _uiService.GameOverScreen.Close();
    }

    private void InitializeServices()
    {
        _scoreService = new(_gameData);
        _targetService = new(_target, _targetKnifePool, _targetApplePool, _gameSettings.Stages, _scoreService);
        _knifeService = new(_inputService.ThrowArea, _gameData, _throwKnifePool, _targetService);
        _shopService = new(_uiService.ShopScreen, _gameData, _inputService.SwitchShopPrevArea, _inputService.SwitchShopNextArea);

        _knifeService.Initialize();
        _targetService.Initialize();
        _shopService.Intialize();

        _uiService.Initialize(_scoreService, _gameData, _target);
    }

    private void InitializePools()
    {
        _throwKnifePool = new(_gameSettings.KnifePrefab, _throwKnivesSpawnPoint);
        _targetKnifePool = new(_gameSettings.KnifePrefab, _targetKnivesSpawnPoint);
        _targetApplePool = new(_gameSettings.ApplePrefab, _targetAppleSpawnPoint);

        _throwKnifePool.Initialize(_gameSettings.ThrowKnifePoolSize);
        _targetKnifePool.Initialize(_gameSettings.TargetKnifePoolSize);
        _targetApplePool.Initialize(_gameSettings.TargetApplePoolSize);
    }

    private void StartRun()
    {
        _inputService.SetActiveMenuInputAreas(false);

        InitializeStartPlaying();

        _uiService.MenuScreen.Close(() => _inputService.SetActiveThrowInputArea(true));
    }

    private void RestartRun()
    {
        _inputService.SetActiveGameOverInputAreas(false);

        InitializeStartPlaying();

        _uiService.GameOverScreen.Close(() => _inputService.SetActiveThrowInputArea(true));
    }

    private void InitializeStartPlaying()
    {
        _scoreService.Reset();
        _targetService.ReinitializeTarget();
        _knifeService.SpawnKnife();

        _uiService.GameScreen.UpdateStageNameUI(_scoreService.Stage);
        _uiService.ShopScreen.SetActiveCanvas(false);
        _uiService.GameScreen.SetActiveCanvas(true);
    }

    private void SwitchStage()
    {
        _inputService.SetActiveThrowInputArea(false);

        _scoreService.NextStage();

        _uiService.FadeScreen.SetText(_scoreService.Stage);
        _uiService.FadeScreen.Close(() =>
        {
            _knifeService.ReturnKnives();
            _knifeService.SpawnKnife();
            _targetService.ReinitializeTarget();

            string stageName = _gameSettings.Stages[_scoreService.Stage - 1].StageName;

            if (_scoreService.Stage >= _gameSettings.Stages.Count)
                if (_scoreService.Stage % 5 == 0)
                    stageName = "Boss";
                else
                    stageName = $"Stage {_scoreService.Stage}";

            _uiService.GameScreen.UpdateStageNameUI(stageName);
            _uiService.FadeScreen.Open(() => _inputService.SetActiveThrowInputArea(true));
        });
    }

    public void GameOver()
    {
        _inputService.SetActiveThrowInputArea(false);

        _uiService.GameOverScreen.SetRunResults(_scoreService.Score, _scoreService.Stage);
        _uiService.GameOverScreen.Open(() =>
        {
            _knifeService.ReturnKnives();
            _inputService.SetActiveGameOverInputAreas(true);
        });
    }

    public void Menu()
    {
        _inputService.SetActiveGameOverInputAreas(false);

        _uiService.MenuScreen.Open(() =>
        {
            _uiService.GameOverScreen.Close();
            _uiService.ShopScreen.SetActiveCanvas(false);
            _inputService.SetActiveMenuInputAreas(true);
        });
    }

    public void ShopToMenu()
    {
        _inputService.SetActiveShopInputAreas(false);

        _uiService.MenuScreen.Open(() =>
        {
            _uiService.ShopScreen.SetActiveCanvas(false);
            _inputService.SetActiveMenuInputAreas(true);
        });
    }

    public void Shop()
    {
        _inputService.SetActiveMenuInputAreas(false);
        _uiService.ShopScreen.SetActiveCanvas(true);
        _uiService.GameScreen.SetActiveCanvas(false);

        _shopService.UpdateKnifePage();

        _uiService.MenuScreen.Close(() => _inputService.SetActiveShopInputAreas(true));
    }
}