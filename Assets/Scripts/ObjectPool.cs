using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : class, new()
{
    public Queue<T> PoolQueue;

    // 常驻容量 -1代表无限
    public int maxCapacity = -1;

    public ObjectPool(int capacity = -1)
    {
        PoolQueue = new Queue<T>();
        maxCapacity = capacity;
    }

    // 拿
    public T Get()
    {
        if (PoolQueue.Count > 0)
        {
            return PoolQueue.Dequeue();
        }
        return new T();
    }

    public void Push(T obj)
    {
        PoolQueue.Enqueue(obj);
    }

    public void Clear()
    {
        PoolQueue.Clear();
    }

    public void Freed()
    {
        if (maxCapacity < 0)
        {
            return;
        }
        while (PoolQueue.Count > maxCapacity)
        {
            PoolQueue.Dequeue();
        }

    }
}
