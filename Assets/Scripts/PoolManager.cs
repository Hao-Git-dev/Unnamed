using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PoolType
{

}
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    Dictionary<PoolType, ObjectPool_Object> pools = new();

    private void Start()
    {
        
    }

}
