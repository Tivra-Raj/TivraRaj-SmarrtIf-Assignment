using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public float InitialGameSpeed = 0.5f;
    public float GameSpeedIncrease = 0.1f;
    public float GameSpeed {  get; private set; }

    public bool isGameRunning = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if(Instance == this)
            Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        GameSpeed = InitialGameSpeed;
        isGameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameSpeed += GameSpeedIncrease * Time.deltaTime;
    }

    public bool IsGameRunning() => isGameRunning;
}
