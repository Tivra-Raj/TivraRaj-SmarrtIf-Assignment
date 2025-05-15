using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] Transform cam; //Main Camera
    Vector3 camStartPos;
    float distance; 

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.5f, 2f)]
    public float parallaxSpeed;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        parallaxSpeed = gameManager.GameSpeed;

        camStartPos = cam.position;
        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }

        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        for (int i = 0; i < backCount; i++) //find the farthest background
        {
            if ((backgrounds[i].transform.position.z - cam.position.z) > farthestBack)
            {
                farthestBack = backgrounds[i].transform.position.z - cam.position.z;
            }
        }

        for (int i = 0; i < backCount; i++) //set the speed of bacground
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }

    private void Update()
    {
        parallaxSpeed = gameManager.GameSpeed;
        distance += Time.deltaTime * parallaxSpeed;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i];
            mat[i].SetTextureOffset("_MainTex", new Vector2(distance * speed, 0));
        }
    }
}
