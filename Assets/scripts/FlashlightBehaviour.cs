using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightBehaviour : MonoBehaviour
{
    [SerializeField] private InputActionProperty turnOnRight;
    [SerializeField] private InputActionProperty turnOnLeft;
    [SerializeField] private Light light;

    private bool rightButton;
    private bool leftButton;

    private bool canSwitchRight;
    private bool canSwitchLeft;

    void Update()
    {
        rightButton = turnOnRight.action.ReadValue<float>() == 1;
        leftButton = turnOnLeft.action.ReadValue<float>() == 1;

        //the player let go of button, so it can be turned on/off again (there's probably a better way to do this)
        if (!rightButton && !canSwitchRight)
            canSwitchRight = true;

        if(!leftButton && !canSwitchLeft)
            canSwitchLeft = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Left Hand" && leftButton && canSwitchLeft)
        {
            light.enabled = !light.enabled;
            canSwitchLeft = false;
        }

        if (other.gameObject.tag == "Right Hand" && rightButton && canSwitchRight)
        {
            light.enabled = !light.enabled;
            canSwitchRight = false;
        }
    }
}
