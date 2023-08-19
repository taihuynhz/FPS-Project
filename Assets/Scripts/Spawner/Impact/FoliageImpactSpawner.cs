using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoliageImpactSpawner : Spawner
{
    protected static FoliageImpactSpawner instance;
    public static FoliageImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
