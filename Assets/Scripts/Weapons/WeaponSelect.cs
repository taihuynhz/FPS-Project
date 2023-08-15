using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] protected GameObject[] weapons;

    protected enum Gun { DE, M4A1 }
    protected Gun gun = Gun.M4A1;

    protected void Update()
    {
        SelectWeapon();
    }

    protected void SelectWeapon()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            gun = Gun.DE;
        else if (Input.GetKey(KeyCode.Alpha2))
            gun = Gun.M4A1;
            
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == (int) gun) 
                weapons[i].gameObject.SetActive(true);
            else 
                weapons[i].gameObject.SetActive(false);
        }
    }
}
