using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianFruitSpawner : MonoBehaviour
{
    [Header("The Size Of Your Imaginary Pyramid")]
    public float xHalfWidth;
    public float yHeight, zHalfLength;
    [Range(0f, 0.5f)] public float yLimit;

    [Header("Amount of Durians")]
    public int numberOfDurians;
    [SerializeField] private int duriansOnTree;
    public Transform durianParent;

    [Header("Durian Tree Prefab")]
    public GameObject durianTree;

    [Header("The Resultant Relative Lengths")]

    [SerializeField]
    private float xRelative;
    [SerializeField]
    private float yRelative, zRelative; [Space(10)]
    [SerializeField]
    private float difference;

    [Header("The Final Random Lengths")]
    [SerializeField]
    private float xRandom;
    [SerializeField]
    private float yRandom, zRandom;[Space(10)]


    private DurianTeethObjectPool durianTeethObjectPool;
    private DurianFruitBehaving durianFruitBehaving;
    public Vector3[] durianPositions;
    private Vector3 positions;
    private GameObject ploop;
    

    private void Awake()
    {
        durianTeethObjectPool = FindObjectOfType<DurianTeethObjectPool>();
        durianFruitBehaving = FindObjectOfType<DurianFruitBehaving>();
    }
    private void Start()
    {
        // Can we make it so I don't depend on Inspector tags to deal with the Dictionary?
        if (numberOfDurians == 0)
        {
            Debug.Log("Don't be shy, add how much durian you want");

        }

        durianPositions = new Vector3[numberOfDurians];

        for (int i = 0; i < numberOfDurians; i++)
        {
            // I am trying to make an area of pyramid to distribute durians randomly without actually making a pyramid mesh!
            // I am thinking on how to make the durians don't overlap with each other, but I still can't implement it yet.
            positions = DurianPositioning(xHalfWidth, yHeight, zHalfLength, yLimit);
            durianPositions[i] = positions + durianTree.transform.position;
            ploop = durianTeethObjectPool.ReleaseTheDurians("Common Durian");
            ploop.transform.position = durianPositions[i];
           
        }
    }

    private void Update()
    {
        
    }

    private Vector3 DurianPositioning(float x, float y, float z, float limitOfY)
    {
        yRelative = Random.Range(limitOfY, 1f);
        yRandom = yRelative * y;
        difference = 1 - yRelative;

        xRelative = Random.Range(-difference, difference);
        zRelative = Random.Range(-difference, difference);

        xRandom = xRelative * x;
        zRandom = zRelative * z;

        Vector3 positioning = new Vector3(xRandom, yRandom, zRandom);

        return positioning;
    }
   
}
