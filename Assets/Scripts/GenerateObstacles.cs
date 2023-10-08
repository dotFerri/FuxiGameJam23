using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObstacles : MonoBehaviour
{

    public List<GameObject> obstacles;
    [Min(0.25f)]
    public float generationInterval;
    public Transform target;
    [Min(3f)]
    public float minDistance;
    [Min(3f)]
    public float maxDistance;
    [Range(0, 1000)]
    public int maxObstacles;
    [Range(1, 100)]
    public int obstaclesPerGeneration;

    private List<GameObject> obstacleInstances = new();
    private float timeUntilGeneration;

    // Start is called before the first frame update
    void Start()
    {
        timeUntilGeneration = generationInterval;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilGeneration -= Time.deltaTime;
        if(timeUntilGeneration <= 0)
        {
            timeUntilGeneration = generationInterval;

            // Clean up dead objects from the instance list
            obstacleInstances.RemoveAll((a) => !a);

            for(int i = 0; i < obstaclesPerGeneration; i++)
            {
                // Don't fill the scene with a lot of obstacles
                if(obstacleInstances.Count >= maxObstacles)
                    break;
                GameObject original = obstacles[Random.Range(0, obstacles.Count)];

                Vector3 position;
                int maxGenerationAttempts = 5;
                Collider collider = original.GetComponent<Collider>();
                float yOffset = 0f;
                if (collider)
                {
                    yOffset = collider.bounds.extents.y - collider.bounds.center.y;
                }
                do
                {
                    position = Random.insideUnitSphere * maxDistance;
                    position.y = 99;
                    if (Physics.Raycast(position, Vector3.down, out RaycastHit hit, 100))
                        position.y = hit.point.y;
                    else
                        position.y = 0;
                    //  position.y += yOffset;
                    position.y = target.position.y; // Set Y within a range
                } while (Vector3.Distance(position, target.position) < minDistance && --maxGenerationAttempts >= 0);
                //               GameObject instance = Instantiate(original, position, Quaternion.LookRotation(target.position - position));
                Vector3 directionToTarget = target.position - position;
                directionToTarget.y = 0f; // Ensure the rotation is horizontal
                Quaternion rotation = Quaternion.LookRotation(directionToTarget);
                GameObject instance = Instantiate(original, position, rotation);
                obstacleInstances.Add(instance);
            }

        }
    }
}
