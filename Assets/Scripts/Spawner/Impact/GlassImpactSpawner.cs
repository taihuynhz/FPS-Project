using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassImpactSpawner : Spawner
{
    protected static GlassImpactSpawner instance;
    public static GlassImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
