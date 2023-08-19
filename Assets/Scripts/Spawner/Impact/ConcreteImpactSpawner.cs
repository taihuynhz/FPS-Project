using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteImpactSpawner : Spawner
{
    protected static ConcreteImpactSpawner instance;
    public static ConcreteImpactSpawner Instance => instance;

    protected void Awake() => CreateSingleton();

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
