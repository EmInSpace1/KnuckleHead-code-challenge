using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent buttonPressed;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Button"))
        {
            buttonPressed.Invoke();
        }
    }
}
