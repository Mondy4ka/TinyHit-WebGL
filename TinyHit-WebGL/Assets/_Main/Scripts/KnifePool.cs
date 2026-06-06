using System.Collections.Generic;
using UnityEngine;

public class KnifePool
{
    private readonly Knife _knifePrefab;
    private readonly Target _target;

    private readonly List<Knife> _pool = new();

    public KnifePool(Knife knifePrefab, Target target)
    {
        _knifePrefab = knifePrefab;
        _target = target;
    }

    public void Initialize(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
            InstantiateNewKnife();
    }

    private void InstantiateNewKnife()
    {
        var newKnife = Object.Instantiate(_knifePrefab);

        newKnife.gameObject.SetActive(false);
        newKnife.Initialize(_target);

        _pool.Add(newKnife);
    }

    public Knife GetKnife()
    {
        if (_pool.Count <= 0)
            InstantiateNewKnife();

        var knife = _pool[0];
        knife.gameObject.SetActive(true);
        _pool.Remove(knife);

        return knife;
    }

    public void ReturnKnife(Knife knife)
    {
        knife.gameObject.SetActive(false);
        _pool.Add(knife);
    }
}