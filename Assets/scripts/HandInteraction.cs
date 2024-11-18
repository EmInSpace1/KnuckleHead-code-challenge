using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandInteraction : MonoBehaviour
{
    [SerializeField] private InputActionProperty pickUpObject;
    [SerializeField] private float handSpeedMultiplier = 50;

    private bool closingHand;
    private bool holding;
    private GameObject heldObject;
    private Rigidbody heldBody;

    private Vector3 handSpeed;
    private Vector3 lastPosition;

    void Start()
    {
        //this is needed for calculating velocity when letting go of an object.
        lastPosition = transform.position;
    }

    void Update()
    {
        closingHand = pickUpObject.action.ReadValue<float>() == 1;

        if (holding && !closingHand)
        {
            //turn on physics on the object
            heldBody.isKinematic = false;
            heldObject.transform.SetParent(null);

            GunBehaviour gun = heldObject.GetComponent<GunBehaviour>();

            //reset the information given to the gun.
            if (gun)
            {
                gun.SetHand("");
            }

            //set the velocity of the object to what is basically the current speed of the hand.
            //the number is set to what feels the best to me.
            heldBody.velocity = handSpeed*handSpeedMultiplier;

            //clear all references.
            heldObject = null;
            heldBody = null;
            holding = false;
        }

        //clear all refences if the object in one of the hands got destroyed while holding it.
        if(holding && !heldBody)
        {
            heldObject = null;
            heldBody = null;
            holding = false;
        }
    }

    private void FixedUpdate()
    {
        //calculate the distance from the last position, used for the object speed when let go.
        handSpeed = transform.position - lastPosition;
        lastPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Holdable" && !holding && closingHand)
        {
            //check if the object being tried to picked up is already being held, or if it's a hat (otherwise you wouldn't be able to remove the hat from your head)
            if (!other.gameObject.GetComponent<Rigidbody>().isKinematic || other.gameObject.GetComponent<HatBehaviour>())
            {
                ///set all the refences
                heldObject = other.gameObject;
                holding = true;

                GunBehaviour gun = heldObject.GetComponent<GunBehaviour>();

                //if picking up gun, set location to the correct spot.
                if (gun)
                {
                    gun.SetHand(gameObject.tag);
                    gun.gameObject.transform.position = transform.position;
                    gun.gameObject.transform.rotation = transform.rotation;

                    Debug.Log(gameObject.tag);
                }

                heldObject.transform.SetParent(transform);
                heldBody = heldObject.GetComponent<Rigidbody>();

                //turning off physics
                heldBody.isKinematic = true;
            }

        }
    }
}