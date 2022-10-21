using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCoin : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Hole")
        {
            Destroy(gameObject);
            CalculatePoints();
        }
    }

    public void CalculatePoints()
    {
        GameManager.Instance.TenPoints();
    }
}
