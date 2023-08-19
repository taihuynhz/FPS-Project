using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterImpactSpawner : Spawner
{
    protected static WaterImpactSpawner instance;
    public static WaterImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
