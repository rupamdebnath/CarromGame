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
            rBody.angularVelocity = 0f;
        }
            

    }
}
