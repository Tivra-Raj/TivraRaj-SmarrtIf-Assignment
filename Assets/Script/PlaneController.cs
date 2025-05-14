using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private GameObject planePrefab;
    [SerializeField] private Transform spawnPosition;

    [SerializeField] private string planeGenerator = "PlaneGenerator";
    [SerializeField] private string planeDestroyer = "PlaneDestroyer";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(planeGenerator))
        {
            SpawnNewPlane();
            
        }

        if (other.CompareTag(planeDestroyer))
        {
            DestroyOldPlane(other.transform.parent.gameObject);
        }
    }

    private void SpawnNewPlane()
    {
        Vector3 position = new Vector3(spawnPosition.position.x, spawnPosition.position.y, spawnPosition.position.z + 125);
        GameObject.Instantiate(planePrefab, position, Quaternion.identity);
    }

    private void DestroyOldPlane(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}