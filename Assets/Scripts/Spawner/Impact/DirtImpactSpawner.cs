using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtImpactSpawner : Spawner
{
    protected static DirtImpactSpawner instance;
    public static DirtImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
