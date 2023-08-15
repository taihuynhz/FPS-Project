using FPSFramework;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected AudioSource playerAudioSource;
    [SerializeField] protected AudioSource gunAudioSource;
    [SerializeField] protected AudioSource hitAudioSource;
    [SerializeField] protected new Camera camera;
    [SerializeField] protected GameObject crosshair;
    [SerializeField] protected Hitmarker hitmarker;
    [SerializeField] protected Transform recoilTransform;
    [SerializeField] protected Transform tracerTransform;

    [Header("Attribute")]
    [SerializeField] public int ammo = 30;
    [SerializeField] public int currentAmmo = 30;
    [SerializeField] public int magazineSize = 30;
    [SerializeField] protected float BulletDistance = 100;
    [SerializeField] protected float fireRate = 0.05f;
    protected float nextFire = 0f;
    
    [Header("Mode")]
    [SerializeField] bool canAutomaticFire;
    [SerializeField] bool automaticFire;
    [SerializeField] bool singleFire;

    [Header("Aiming")]
    [SerializeField] protected bool aiming;
    [SerializeField] protected Vector3 normalPosition = new Vector3(0.2f, -0.05f, 0.02f);
    [SerializeField] protected Vector3 aimPosition = new Vector3(-0.303f, 0.04f, 0.02f);
    protected Vector3 velocity = Vector3.zero;
    protected float smoothTime = 0.03f;

    [Header("Recoil")]
    [SerializeField] protected Vector3 normalRecoil;
    [SerializeField] protected Vector3 runningRecoil;
    [SerializeField] protected Vector3 aimRecoil;
    [SerializeField] protected float snappiness;
    [SerializeField] protected float returnSpeed;
    protected Vector3 currentRotation;
    protected Vector3 targetRotation;

    [Header("Effect")]
    [SerializeField] protected ImpactInfo[] ImpactElemets = new ImpactInfo[0];
    [SerializeField] protected ParticleGroupEmitter[] shotEmitters;
    [SerializeField] protected ParticleGroupPlayer[] afterFireSmoke;
    [SerializeField] protected LineRenderer[] bulletTracer;
    [SerializeField] protected AudioClip[] gunSound;

    public RaycastHit hit;

    protected void Update()
    {
        Recoil();
        Fire();
        Aim();
        EnableCrossHair();
        Reload();
        SetAmmoText();
    }

    protected void Fire()
    {
        if (currentAmmo > 0)
        {
            if (canAutomaticFire)
            {
                if (automaticFire)
                {
                    if (Input.GetButton("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        currentAmmo--;
                        animator.SetTrigger("Shoot");

                        PlayFireEffect();
                        PlayFireSound();
                        RecoilFire();
                        CreateImpactEffect();
                        CreateBulletTracer();
                        crosshair.GetComponent<Crosshair>().UpdateSize(2);
                    }

                    if (Input.GetButtonUp("Fire1"))
                        PlayAfterFireEffect();
                }
                else
                {
                    if (Input.GetButtonDown("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && Time.time > nextFire)
                    {
                        nextFire = Time.time + fireRate;
                        currentAmmo--;
                        animator.SetTrigger("Shoot");

                        PlayFireEffect();
                        PlayFireSound();
                        RecoilFire();
                        CreateImpactEffect();
                        CreateBulletTracer();
                        PlayAfterFireEffect();
                        crosshair.GetComponent<Crosshair>().UpdateSize(2);
                    }

                    if (Input.GetButtonUp("Fire1"))
                        PlayAfterFireEffect();
                }
            }
            else 
            {
                if (Input.GetButtonDown("Fire1") && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    currentAmmo--;
                    animator.SetTrigger("Shoot");

                    PlayFireEffect();
                    PlayFireSound();
                    RecoilFire();
                    CreateImpactEffect();
                    CreateBulletTracer();
                    PlayAfterFireEffect();
                    crosshair.GetComponent<Crosshair>().UpdateSize(2);
                }

                if (Input.GetButtonUp("Fire1"))
                    PlayAfterFireEffect();
            }

        }
           
        if (Input.GetButtonDown("Fire1") && currentAmmo <= 0)
        {
            gunAudioSource.PlayOneShot(Resources.Load("Audio/Weapon/OutOfAmmoSound") as AudioClip);
        }
    }

    protected void Recoil()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, snappiness * Time.fixedDeltaTime);
        recoilTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    protected void CreateImpactEffect()
    {
        Ray ray = new Ray(recoilTransform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, BulletDistance))
        {
            GameObject effect = GetImpactEffect(hit.transform.gameObject);
            if (effect == null) return;
            GameObject effectIstance = Instantiate(effect, hit.point, new Quaternion(), hit.transform.parent);
            effectIstance.transform.LookAt(hit.point + hit.normal);
            Destroy(effectIstance, 20);
        }

        if (hit.transform.gameObject.layer == 6)
        {
            hitmarker.Enable();
            hit.transform.GetComponent<ShootingSheet>().Enable();
            hitAudioSource.PlayOneShot(Resources.Load("Audio/Bullet/Hitmarker") as AudioClip);
        }

        if (hit.transform.gameObject.layer == 7)
        {
            hitmarker.Enable(true);
            hitAudioSource.PlayOneShot(Resources.Load("Audio/Bullet/Hitmarker") as AudioClip);
        }
    }

    protected void CreateBulletTracer()
    {
        Ray ray = new Ray(tracerTransform.transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit))
        {
            LineRenderer lineRenderer = Instantiate(bulletTracer[0], ray.origin, Quaternion.identity);
            lineRenderer.SetPosition(0, tracerTransform.position);
            lineRenderer.SetPosition(1, hit.point);
        }
    }

    protected void PlayFireEffect()
    {
        if (afterFireSmoke != null)
        {
            foreach (ParticleGroupPlayer effect in afterFireSmoke)
                effect.Stop();
        }

        if (shotEmitters != null)
        {
            foreach (ParticleGroupEmitter effect in shotEmitters)
                effect.Emit(1);
        }
    }

    protected void PlayAfterFireEffect()
    {
        if (afterFireSmoke != null)
        {
            foreach (ParticleGroupPlayer effect in afterFireSmoke)
                effect.Play();
        }
    }

    protected void PlayFireSound()
    {
        playerAudioSource.PlayOneShot(gunSound[0]);
        StartCoroutine(ShellDropSound());
    }

    protected void Aim()
    {
        if (Input.GetButton("Fire2"))
        {
            camera.fieldOfView = 55f;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, aimPosition, ref velocity, smoothTime);
            aiming = true;
        }
        else
        {
            camera.fieldOfView = 60f;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, normalPosition, ref velocity, smoothTime);
            aiming = false;
        }
    }

    protected void EnableCrossHair()
    {
        if (aiming || animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            crosshair.SetActive(false);
        else
            crosshair.SetActive(true);

        if (!FirstPersonController.Instance.m_IsWalking && !animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
            crosshair.GetComponent<Crosshair>().UpdateSize(2);
    }

    protected void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && currentAmmo < magazineSize)
        {
            int oldammo = currentAmmo;
            currentAmmo = magazineSize;
            ammo -= (magazineSize - oldammo);

            gunAudioSource.PlayOneShot(Resources.Load("Audio/Weapon/DE/ReloadSound") as AudioClip);
            animator.SetTrigger("Reload");
        }
    }

    protected void RecoilFire()
    {
        if (aiming)
            targetRotation += new Vector3(aimRecoil.x, Random.Range(-aimRecoil.y, aimRecoil.y), Random.Range(-aimRecoil.z, aimRecoil.z));
        else if (!FirstPersonController.Instance.m_IsWalking && !aiming)
            targetRotation += new Vector3(runningRecoil.x, Random.Range(-runningRecoil.y, runningRecoil.y), Random.Range(-runningRecoil.z, runningRecoil.z));
        else
            targetRotation += new Vector3(normalRecoil.x, Random.Range(-normalRecoil.y, normalRecoil.y), Random.Range(-normalRecoil.z, normalRecoil.z));
    }

    protected IEnumerator ShellDropSound()
    {
        yield return new WaitForSeconds(0.5f);
        gunAudioSource.PlayOneShot(Resources.Load("Audio/Bullet/BulletDrop") as AudioClip);
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

    protected void SetAmmoText()
    {
        AmmoUI.Instance.ammoText.text = currentAmmo.ToString() + " / " + ammo.ToString();
    }
}

