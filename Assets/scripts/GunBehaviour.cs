using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngineInternal;

public class GunBehaviour : MonoBehaviour
{
    [SerializeField] private InputActionProperty turnOnRight;
    [SerializeField] private InputActionProperty turnOnLeft;
    [SerializeField] private InputActionProperty shootRight;
    [SerializeField] private InputActionProperty shootLeft;
    private Rigidbody rb;
    private int ammo = 10;
    private bool auto = false;
    private string hand;

    private bool canShoot = true;
    private float autoDelay;
    [SerializeField] private float autoDelayTime = 0.2f;

    private bool rightTrigger;
    private bool leftTrigger;

    private bool rightButton;
    private bool leftButton;

    private bool canSwitchLeft;
    private bool canSwitchRight;

    [Header("Object References")]
    [SerializeField] private TextMeshProUGUI ammoCounter;
    [SerializeField] private TextMeshProUGUI autoText;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject shootEffect;
    [SerializeField] private GameObject bullet;
    void Start()
    {
        
    }
    void Update()
    {
        //change the display on the gun
        ammoCounter.text = ammo.ToString();
        autoText.text = "Auto: " + auto.ToString();

        //getting input
        rightButton = turnOnRight.action.ReadValue<float>() == 1;
        leftButton = turnOnLeft.action.ReadValue<float>() == 1;

        rightTrigger = shootRight.action.ReadValue<float>() == 1;
        leftTrigger = shootLeft.action.ReadValue<float>() == 1;

        //
        //the player let go of button, so it can be turned on/off again (there's probably a better way to do this)
        if (!rightButton && !canSwitchRight)
            canSwitchRight = true;

        if (!leftButton && !canSwitchLeft)
            canSwitchLeft = true;

        //don't like how I needed to do the same thing for both hands, would need to look into how to do it better.
        if (hand == "Right Hand")
        {
            if (rightButton && canSwitchRight)
            {
                auto = !auto;
                canSwitchRight = false;
            }

            if(rightTrigger && ammo != 0 && canShoot)
            {
                Shoot();
            }

            if (!canShoot && !rightTrigger && !auto)
                canShoot = true;

            if (auto && autoDelay <= 0)
                canShoot = true;

            autoDelay -= Time.deltaTime;
        }

        if (hand == "Left Hand")
        {
            if (leftButton && canSwitchLeft)
            {
                auto = !auto;
                canSwitchLeft = false;
            }

            if (leftTrigger && ammo > 0 && canShoot)
            {
                Shoot();
            }

            if (!canShoot && !leftTrigger && !auto)
                canShoot = true;

            if (auto && autoDelay <= 0)
                canShoot = true;

            autoDelay -= Time.deltaTime;
        }

    }

    public void SetHand(string value)
    {
        hand = value;
    }

    private void Shoot()
    {
        canShoot = false;

        if (auto)
            autoDelay = autoDelayTime;

        ammo--;

        Instantiate(shootEffect, shootPoint.position, shootPoint.rotation);
        GameObject shotBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        shotBullet.GetComponent<Rigidbody>().velocity = transform.forward * 20;
    }

    public void SetAmmo(int value)
    {
        ammo = value;
    }
}
