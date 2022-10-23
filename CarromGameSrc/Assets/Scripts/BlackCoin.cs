using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCoin : MonoBehaviour, IPointCalculate
{
    Rigidbody2D rBody;

    private void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        //stop unwanted rotation that happens for sometime after hit by striker
        if (rBody.velocity.magnitude < 1)
        {
            rBody.angularVelocity = 0f;
        }        

    }

    //Check if pocketed and calculate respective score
    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Hole")
        {
            Destroy(gameObject);
            CalculatePoints();
        }
    }

    public void CalculatePoints()
    {
        GameManager.Instance.FivePoints();
    }
}
