using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinImpactSpawner : Spawner
{
    protected static SkinImpactSpawner instance;
    public static SkinImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
