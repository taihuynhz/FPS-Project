using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float flySpeed = 50f;
    [SerializeField] protected Transform spawn;
    [SerializeField] protected ImpactInfo[] ImpactElemets = new ImpactInfo[0];
    protected Vector3 flyDirection;

    protected void OnEnable()
    {
        flyDirection = spawn.transform.forward;
        spawn = GameObject.Find("M4A1").transform;
    }

    protected void Start()
    {    
        spawn = GameObject.Find("M4A1").transform;
        SetFlyDirection();
    }

    protected void Awake()
    {
        spawn = GameObject.Find("M4A1").transform;
    }

    protected void Update()
    {
        Fly();
        StartCoroutine(DespawnAfterTime(2));
    }


    protected void Fly()
    {
        transform.Translate(flyDirection * flySpeed * Time.deltaTime);
    }

    protected void SetFlyDirection()
    {
        flyDirection = spawn.transform.forward;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //RaycastHit hit;
        //Ray ray = new Ray(recoilTransform.position, transform.forward);
        //if (Physics.Raycast(ray, out hit, BulletDistance))
        //{
        //    GameObject effect = GetImpactEffect(hit.transform.gameObject);
        //    if (effect == null)
        //        return;
        //    GameObject effectIstance = Instantiate(effect, hit.point, new Quaternion());
        //    effectIstance.transform.LookAt(hit.point + hit.normal);
        //    Destroy(effectIstance, 20);
        //}
    }

    protected GameObject GetImpactEffect(GameObject impactedGameObject)
    {
        MaterialType materialType = impactedGameObject.GetComponent<MaterialType>();
        if (materialType == null)
            return null;
        foreach (ImpactInfo impactInfo in ImpactElemets)
        {
            if (impactInfo.MaterialType == materialType.TypeOfMaterial)
                return impactInfo.ImpactEffect;
        }
        return null;
    }

    protected virtual IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        BulletSpawner.Instance.Despawn(transform);
    }
}
