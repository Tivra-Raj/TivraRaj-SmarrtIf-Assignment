using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    [SerializeField] private GameObject GameWinObject;
    [SerializeField] private GameObject GameLoseObject;

    public static UIController Instance { get; private set; }

    private int score;
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
        GameWinObject.SetActive(false);
        GameLoseObject.SetActive(false);
        UpdateScore();
    }

    public void IncremententScore() => score++; 

    public void UpdateScore()
    {
        scoreText.SetText("Score = " + score);
    }

    public void EnableGameWinCanvas()
    {
        GameWinObject.SetActive(true);
    }

    public void EnableGameLoseCanvas()
    {
        GameLoseObject.SetActive(true);
    }
}
