using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : MonoBehaviour
{
    public T ItemPrefab { get; private set; }
    public Transform Parent { get; private set; }

    private readonly Queue<T> _pool = new();

    public ObjectPool(T itemPrefab, Transform parent)
    {
        ItemPrefab = itemPrefab;
        Parent = parent;
    }

    public void Initialize(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
            InstantiateNewItem();
    }

    private void InstantiateNewItem()
    {
        T newItem = Object.Instantiate(ItemPrefab, Parent);
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
        item.transform.SetParent(Parent);

        (item as IResetable)?.ResetItem();

        _pool.Enqueue(item);
    }
}