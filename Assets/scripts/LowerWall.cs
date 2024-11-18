using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private Transform newLocation;

    private bool moving;
    private void FixedUpdate()
    {
        if(moving)
            wall.transform.position = Vector3.Lerp(wall.transform.position, newLocation.position, Time.deltaTime);
    }
    public void MoveWall()
    {
        moving = true;
    }
}
