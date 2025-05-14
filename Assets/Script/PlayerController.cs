using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sideSpeed;

    [SerializeField] private Transform leftLane; //0
    [SerializeField] private Transform centerLane; //1
    [SerializeField] private Transform rightLane; //2

    [SerializeField] private int currentLane = 1;

    private bool changeOnce = false;
    private bool isGameRunning = false;

    void Start()
    {
        isGameRunning = true;
        currentLane = 1;
        //ChangePlayerPositionOnLaneChange();

        StartCoroutine(GameWin());
    }

    void Update()
    {
        ChangeLaneInLeft();
        ChangeLaneInRight();
    }

    private void FixedUpdate()
    {
        if (isGameRunning)
        {

            Move();
            ChangePlayerPositionOnLaneChange();
        }
    }

    private void Move()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + moveSpeed * Time.deltaTime);
    }

    private void ChangeLaneInLeft()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentLane == 1 && !changeOnce)
            {
                currentLane = 0;
                changeOnce = true;
            }
            else if (currentLane == 2 && !changeOnce)
            {
                Debug.Log("change to lane 1");
                currentLane = 1;
                changeOnce = true;
            }
        }
        StartCoroutine(ChangeLaneBool());
    }

    private void ChangeLaneInRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentLane == 0 && !changeOnce)
            {
                currentLane = 1;
                changeOnce = true;
            }
            if (currentLane == 1 && !changeOnce)
            {
                currentLane = 2;
                changeOnce = true;
            }
        }
        StartCoroutine(ChangeLaneBool());
    }

    private IEnumerator ChangeLaneBool()
    {
        yield return new WaitForSeconds(0.1f);
        changeOnce = false;
    }

    private void ChangePlayerPositionOnLaneChange()
    {
        if (currentLane == 0)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(leftLane.position.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
        }
        else if(currentLane == 1)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(centerLane.position.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
        }
        else if(currentLane == 2)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(rightLane.position.x, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            UIController.Instance.IncremententScore();
            UIController.Instance.UpdateScore();
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Train"))
        {
            Debug.Log("collided with train");
            UIController.Instance.EnableGameLoseCanvas();
        }
    }

    private IEnumerator GameWin()
    {
        yield return new WaitForSeconds(60);
        UIController.Instance.EnableGameWinCanvas();
        isGameRunning = false;
    }
}
