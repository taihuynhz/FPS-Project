using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasterImpactSpawner : Spawner
{
    protected static PlasterImpactSpawner instance;
    public static PlasterImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
