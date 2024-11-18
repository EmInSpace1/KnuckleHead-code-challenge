using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBehaviour : MonoBehaviour
{
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        //Set the hat to the correct position, and correct parent.
        if(other.gameObject.tag == "HatPoint" && !body.isKinematic)
        {
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            transform.SetParent(other.transform);

            body.isKinematic = true;
        }
    }
}
