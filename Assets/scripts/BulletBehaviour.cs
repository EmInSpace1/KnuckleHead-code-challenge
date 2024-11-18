using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.GetComponent<GunBehaviour>())
        {
            if(collision.gameObject.CompareTag("target"))
                Destroy(collision.gameObject);

            Destroy(gameObject);
        }
    }
}
