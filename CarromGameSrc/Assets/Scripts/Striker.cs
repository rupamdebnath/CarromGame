using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    float powerApplied = 10f;

    public Image powerStats;
    public Slider posSlider;
    //[SerializeField] 
    //EventSystem m_EventSystem;
    //[SerializeField] 
    //GraphicRaycaster m_Raycaster;
    public GameObject Powerbar;
    bool powerFixed = false;
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
        direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();
        //Debug.Log(positionIsSet);
        if (!hasStriked && !positionIsSet)
        {
            //selfTransform.position = new Vector2(Mathf.Clamp(worldMousePos.x, -2.71f, 2.65f), startPosition.y);
            selfTransform.position = new Vector2(Mathf.Clamp(posSlider.value, -2.71f, 2.65f), startPosition.y);
            rbody.angularVelocity =0f;
        }


        if (Input.GetMouseButtonDown(1))
        {
            PlaceButton();
        }
        if(positionIsSet)
        {
            if (Input.GetMouseButton(0))
            {
                powerApplied += 0.1f;
                powerStats.fillAmount = (float)powerApplied / 100;
                strikerSpeed = (int)(powerApplied * 50f);
                if (powerStats.fillAmount == 1)
                    powerApplied = 0f;
            }
        }
#if UNITY_ANDROID
        DragShoot();
        GeneratePower();

#endif

#if UNITY_EDITOR
        if (Input.GetButtonUp("Fire1") && rbody.velocity.magnitude < 1 && !hasStriked && positionIsSet && !EventSystem.current.IsPointerOverGameObject()) //  
        {
            //if (Input.GetMouseButtonUp(0))
                ShootStriker();
        }
#endif
        if (rbody.velocity.magnitude < 0.2f && rbody.velocity.magnitude != 0)
        {
            StrikerReset();
        }
    }

    public void StrikerReset()
    {
        rbody.velocity = Vector2.zero;
        rbody.angularVelocity = 0;
        hasStriked = false;
        positionIsSet = false;
        powerStats.fillAmount = 0;
        powerApplied = 0f;
        powerFixed = false;
    }

    public void ShootStriker()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(worldMousePos - transform.position);
        direction.Normalize();
        rbody.AddForce(direction * strikerSpeed);
        hasStriked = true;
    }
#if UNITY_EDITOR
    public void PlaceButton()
    {
        if (!positionIsSet)
        {
            positionIsSet = true;
        }
        if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
            Debug.Log("Hallelu");
    }
#endif
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StrikerReset();
        selfTransform.position = new Vector2(Random.Range(-2.71f, 2.65f), startPosition.y);
        positionIsSet = true;
    }

    void DragShoot()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 startPos = Camera.main.ScreenToWorldPoint(touch.position);
            //Debug.Log(startPos);
            RaycastHit2D hit = Physics2D.Raycast(startPos, -Vector2.up);
            if (hit && hit.collider.CompareTag("Player"))
            {
                Debug.Log("I m hit");
            }
        }
    }

    void GeneratePower()
    {
        if(!powerFixed)
        {
            powerApplied += 0.1f;
            powerStats.fillAmount = (float)powerApplied / 100;
            strikerSpeed = (int)(powerApplied * 50f);
            if (powerStats.fillAmount == 1)
                powerApplied = 0f;
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.mousePosition;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);

            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {                   
                    if (go.gameObject == Powerbar)
                    {
                        //Debug.Log(go.gameObject.name);
                        Debug.Log("power hit");
                        //powerStats.fillAmount = (float)powerApplied / 100;
                        powerFixed = true;
                    }
                }

            }
        }

    }
}
