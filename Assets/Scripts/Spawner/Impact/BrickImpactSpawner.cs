using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickImpactSpawner : Spawner
{
    protected static BrickImpactSpawner instance;
    public static BrickImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
