using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockImpactSpawner : Spawner
{
    protected static RockImpactSpawner instance;
    public static RockImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
