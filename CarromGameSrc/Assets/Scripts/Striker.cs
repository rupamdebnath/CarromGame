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
    public GameObject Powerbar;
    bool powerFixed = false;
    bool strikerPointed = false;
    int foulCounter = 0;
    [SerializeField]
    GameObject foulText;
    bool startForce = false;
    void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        selfTransform = transform;
    }
    void Update()
    {        
        worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();
        
        if (!hasStriked && !positionIsSet)
        {
            selfTransform.position = new Vector2(Mathf.Clamp(posSlider.value, -2.71f, 2.65f), startPosition.y);
            rbody.angularVelocity =0f;
        }
        if (foulCounter>=3)
        {
            GameManager.Instance.GameOver(false);
        }
#if UNITY_ANDROID
        DragShoot();
        GeneratePower();

#endif

        #region Windows or Mac build specific not required for Android
#if UNITY_STANDALONE
        //if (Input.GetButtonUp("Fire1") && rbody.velocity.magnitude < 1 && !hasStriked && positionIsSet && !EventSystem.current.IsPointerOverGameObject()) //  
        //{
        //    //if (Input.GetMouseButtonUp(0))
        //    //ShootStriker();
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    PlaceButton();
        //}
        //if (positionIsSet)
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        powerApplied += 0.1f;
        //        powerStats.fillAmount = (float)powerApplied / 100;
        //        strikerSpeed = (int)(powerApplied * 50f);
        //        if (powerStats.fillAmount == 1)
        //            powerApplied = 0f;
        //    }
        //}

        //public void PlaceButton()
        //{
        //    if (!positionIsSet)
        //    {
        //        positionIsSet = true;
        //    }
        //    if (EventSystem.current.currentSelectedGameObject.GetComponent<Button>() != null)
        //        Debug.Log("Hallelu");
        //}
#endif

        #endregion

        if (rbody.velocity.magnitude < 0.2f && rbody.velocity.magnitude != 0)
        {
            StrikerReset();
        }
    }

    //Reset striker in baseline along with power bar once it has been fired
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

    //main shoot function that uses the striker speed and adds a force in the set direction
    public void ShootStriker()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)(worldMousePos - transform.position);
        direction.Normalize();
        strikerSpeed = (int)(powerApplied * 50f);
        startForce = true;
        //if (rbody.velocity.sqrMagnitude <= 0.1f)
        //    return;
        
    }
    private void FixedUpdate()
    {
        if(startForce)
        {
            rbody.AddForce(direction * strikerSpeed);
            SceneController.Instance.PlaySound(Sounds.Striker);
            hasStriked = true;
            startForce = false;
        }
            
    }
    //To check for striker foul when it is pocketed, fix random position in baseline and don't allow user to move it
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StrikerReset();
        selfTransform.position = new Vector2(Random.Range(-2.71f, 2.65f), startPosition.y);
        positionIsSet = true;
        strikerPointed = false;
        hasStriked = false;
        foulCounter++;
        StartCoroutine(WaitForText(1));
    }
    IEnumerator WaitForText(float seconds)
    {
        foulText.SetActive(true);
        yield return new WaitForSeconds(seconds);
        foulText.SetActive(false);
    }
    //Fetch touchcount and shoot striker with power when touchphase ends
    void DragShoot()
    {
        
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 startPos = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(startPos, -Vector2.up);
            if (touch.phase == TouchPhase.Began)
            {
                if (hit && hit.collider.CompareTag("Player"))
                    strikerPointed = true;

            }
            if (touch.phase == TouchPhase.Ended && strikerPointed && powerFixed && !hasStriked)
            {
                ShootStriker();              
            }

        }
    }
    //For setting the power value in the power bar with touch in respective area
    void GeneratePower()
    {
        if(!powerFixed)
        {
            powerApplied += (50f * Time.deltaTime);
            powerStats.fillAmount = (float)powerApplied / 100;
            
            if (powerStats.fillAmount == 1)
                powerApplied = 0f;
        }

        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            PointerEventData pointer = new PointerEventData(EventSystem.current);
            pointer.position = Input.GetTouch(0).position;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, raycastResults);
            
            if (raycastResults.Count > 0)
            {
                foreach (var go in raycastResults)
                {                   
                    if (go.gameObject == Powerbar)
                    {
                        powerFixed = true;
                        strikerPointed = false;
                    }
                }

            }
        }

    }
}
