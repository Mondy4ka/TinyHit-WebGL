using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectPool<T> where T : MonoBehaviour
{
    private readonly T _objectPrefab;
    private readonly Transform _parent;
    private readonly Queue<T> _pool = new();

    public ObjectPool(T objectPrefab, Transform parent)
    {
        _objectPrefab = objectPrefab;
        _parent = parent;
    }

    public void Initialize(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
            InstantiateNewItem();
    }

    private void InstantiateNewItem()
    {
        T newItem = UnityEngine.Object.Instantiate(_objectPrefab, _parent);
        newItem.gameObject.SetActive(false);

        (newItem as IInitializable)?.Initialize();

        _pool.Enqueue(newItem);
    }

    public T GetItem()
    {
        if (_pool.Count <= 0)
            InstantiateNewItem();

        T item = _pool.Dequeue();

        item.gameObject.SetActive(true);
        item.transform.SetParent(null);

        return item;
    }

    public void ReturnItem(T item)
    {
        item.gameObject.SetActive(false);
        item.transform.SetParent(_parent);

        _pool.Enqueue(item);
    }
}