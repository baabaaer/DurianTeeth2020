using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Durian Teeth Objects")]
public class DurianTeethObjects : ScriptableObject
{
    public string objectName;
    public string description;
    public Sprite sprite;
    public float baseAttackValue;
    public float baseSpeed;
    public float baseBravery;
    public Vector3 spawnLocation;
    
    [SerializeField] private Transform parent;
    public GameObject prefab;

    public virtual void DroppingToGround()
    {
        
    }




}
