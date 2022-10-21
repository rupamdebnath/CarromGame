using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    Rigidbody2D rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (rBody.velocity.magnitude < 1)
        {
            //Debug.Log("Stopped");
            //rBody.angularVelocity = 0f;
            //rBody.SetRotation(0);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }
            

    }
}
