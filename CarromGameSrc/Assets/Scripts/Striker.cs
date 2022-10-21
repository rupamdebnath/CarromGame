using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Striker : MonoBehaviour
{
    Rigidbody2D rbody;
    Vector2 startPosition;

    public GameObject arrowDirection;
    Vector2 direction;
    Vector3 worldMousePos;
    Transform selfTransform;
    Transform arrowTransform;
    public int strikerSpeed = 500;
    bool hasStriked = false;
    public bool positionIsSet = false;

    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        selfTransform = transform;
        arrowTransform = arrowDirection.transform;
    }

    void Update()
    {
        
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //arrowTransform.LookAt(Input.mousePosition, arrowTransform.up);
        arrowTransform.LookAt(Input.mousePosition, Vector2.up);
        direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();
        //arrowTransform.LookAt(worldMousePos);
        if (!hasStriked && !positionIsSet)
        {
            selfTransform.position = new Vector2(Mathf.Clamp(worldMousePos.x, -2.71f, 2.65f), startPosition.y);
            rbody.angularVelocity =0f;
            //if (Input.GetMouseButtonDown(0))
            //    selfTransform.position = Input.mousePosition;
        }

        if (positionIsSet && rbody.velocity.magnitude == 0)
        {
            arrowTransform.gameObject.SetActive(true);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!positionIsSet)
            {
                positionIsSet = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && rbody.velocity.magnitude < 1 && !hasStriked && positionIsSet)
        {
            ShootStriker();
        }

        if (rbody.velocity.magnitude < 0.2f && rbody.velocity.magnitude != 0)
        {
            StrikerReset();
        }
    }

    public void StrikerReset()
    {
        rbody.velocity = Vector2.zero;
        hasStriked = false;
        positionIsSet = false;
    }

    public void ShootStriker()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(worldMousePos - transform.position);
        direction.Normalize();
        rbody.AddForce(direction * strikerSpeed);
        hasStriked = true;
    }

}
