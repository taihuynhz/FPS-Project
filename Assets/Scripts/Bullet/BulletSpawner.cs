using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    protected static BulletSpawner instance;
    public static BulletSpawner Instance => instance;
    [SerializeField] protected Vector3 spawnPos;
    [SerializeField] protected Transform[] SpawnPos;

    protected void Awake() => CreateSingleton();

    public void SpawnBullet()
    {
        GameObject bullet = Spawn();
        if (bullet == null) return;

        //Transform player = GameObject.Find("Player").transform;
        //spawnPos = new Vector3(player.position.x, player.position.y + 2.45f, player.position.z);
        bullet.transform.position = SpawnPos[1].position;
        //bullet.transform.rotation = (player.rotation.y > 0 ? Quaternion.Euler(-90f, 0f, 0f) : Quaternion.Euler(-90f, 0f, 180f));
        bullet.gameObject.SetActive(true);
    }

    protected void CreateSingleton()
    {
        if (instance != null) return;
        instance = this;
    }
}
