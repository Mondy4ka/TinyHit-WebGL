using UnityEngine;

public class KnifeService : MonoBehaviour
{
    public KnifePool KnifePool { get; private set; }

    [SerializeField] private Knife _knifePrefab;
    [SerializeField] private Target _target;
    [SerializeField] private Transform _knifePoint;
    [SerializeField] private int _poolSize;

    private Knife _currentKnife;

    private void Awake()
    {
        KnifePool = new(_knifePrefab, _target);

        KnifePool.Initialize(_poolSize);

        _currentKnife = KnifePool.GetKnife();
        _currentKnife.MoveTo(_knifePoint.position);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentKnife.Throw();
            _currentKnife = KnifePool.GetKnife();
            _currentKnife.MoveTo(_knifePoint.position);
        }
    }
}
