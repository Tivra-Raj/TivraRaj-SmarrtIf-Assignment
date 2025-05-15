using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private float speedMultiplier = 0.1f;

    void Start()
    {
        gameManager = GameManager.Instance;
    }

    void Update()
    {
        transform.position += gameManager.GameSpeed * speedMultiplier * Time.deltaTime * Vector3.left;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("OutOfBound"))
        {
            Destroy(gameObject);
        }
    }
}