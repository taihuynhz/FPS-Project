using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalImpactSpawner : Spawner
{
    protected static MetalImpactSpawner instance;
    public static MetalImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
