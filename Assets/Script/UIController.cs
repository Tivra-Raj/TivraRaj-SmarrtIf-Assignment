using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject GameLoseObject;
    [SerializeField] private TextMeshProUGUI uiScoreText;
    [SerializeField] private TextMeshProUGUI distanceText;

    public static UIController Instance { get; private set; }

    private int score;
    private float distance;
    public int Score => score;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        score = 0;
        GameLoseObject.SetActive(false);
        UpdateScore();
    }

    public void IncremententScore() => score++; 

    public void UpdateScore()
    {
        scoreText.SetText("Score = " + score);
        uiScoreText.SetText("Score = " + score);
        distanceText.SetText("Distance = " + distance);
    }

    public void UpdateDistance(float distance)
    {
        this.distance = distance;
    }

    public void EnableGameLoseCanvas()
    {
        GameLoseObject.SetActive(true);
    }
}
