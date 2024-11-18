using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBehaviour : MonoBehaviour
{
    [SerializeField] private GunBehaviour actualGun;
    [SerializeField] private int layer;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.layer == layer) //has to check for layer instead of tag because of how I made the pick up system.
        {
            Rigidbody ammo = other.gameObject.GetComponent<Rigidbody>();

            if(ammo.isKinematic) //checks if the ammo is currently being held.
            {
                actualGun.SetAmmo(10); //ew, hard coded, do it differently in bigger project.

                Destroy(ammo.gameObject);
            }
        }
    }
}
