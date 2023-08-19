using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodImpactSpawner : Spawner
{
    protected static WoodImpactSpawner instance;
    public static WoodImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
