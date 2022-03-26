using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianTreeSpawner : MonoBehaviour
{
    public GameObject prefabDurianTree;
    public GameObject durianTreeSpawnParent;
    public GameObject ground;
    public float groundLength;
    public float groundWidth;
    public float randomnessX;
    public float randomnessZ;
    [Range(10,200)] public float treeSpaceX;
    [Range(10,200)] public float treeSpaceZ;
    private int gridHalfLength;
    private int gridHalfWidth;
    public Vector3 plantSpot;

    public int numberOfTrees = 0;

    private void Awake()
    {
        // Measure land, so to speak
        groundLength = ground.GetComponent<Renderer>().bounds.size.x;
        groundWidth = ground.GetComponent<Renderer>().bounds.size.z;

        // No, I am not implementing tree growing over time
        PlantTrees(groundLength, groundWidth);
    }

    private void PlantTrees(float length, float width)
    {
        // Divide the area by 2 times the distance between trees in meter
        gridHalfLength = (int)(length / (2 * treeSpaceX));
        gridHalfWidth = (int)(width / (2 * treeSpaceZ));

        // The plant them in place, with some randomness because people are juling at long distances
        for(int i = -gridHalfLength; i < gridHalfLength; i++)
        {
            for(int j = -gridHalfWidth; j < gridHalfWidth; j++)
            {
                // Try not to make too random, it's still a kebun, not a hutan
                randomnessX = Random.Range(-treeSpaceX/4f, treeSpaceX/4f);
                randomnessZ = Random.Range(-treeSpaceZ/4f, treeSpaceZ/4f);

                // The exact spot to be planted
                plantSpot = new Vector3(treeSpaceX * i + 15 + randomnessX,
                                        0,
                                        treeSpaceZ * j + 15 + randomnessZ
                                        );
                Instantiate(prefabDurianTree,
                            plantSpot,
                            Quaternion.identity,
                            durianTreeSpawnParent.transform);
                numberOfTrees++;

            }
        }
    }
}
