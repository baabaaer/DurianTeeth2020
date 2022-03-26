using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianTeethObjectPool : MonoBehaviour
{
    public string[] objectNames;
    public GameObject[] durianTeethObjects;
    public int[] howManyObjects;
    public GameObject[] durianTeethObjecstParent;
    
    private Dictionary<string, Queue<GameObject>> durianTeethObjectsPool
        = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        InitialSpawningOfPool();
    }

    private void InitialSpawningOfPool()
    {
        for (int i = 0; i < objectNames.Length; i++)
        {
            Queue<GameObject> objPool = new Queue<GameObject>();
            for (int j = 0; j < howManyObjects[i]; j++)
            {
                GameObject obj = Instantiate(durianTeethObjects[i], 
                    durianTeethObjecstParent[i].transform);
                obj.SetActive(false);
                objPool.Enqueue(obj);

            }
            durianTeethObjectsPool.Add(objectNames[i], objPool);
        }
    }

    public GameObject ReleaseTheDurians(string tag)
    {
        GameObject objectToSpawn;
        if (!durianTeethObjectsPool.ContainsKey(tag))
        {
            Debug.Log("Pool with tag" + tag + "doesn't exist");
            return null;
        }
        objectToSpawn = durianTeethObjectsPool[tag].Dequeue();
        objectToSpawn.SetActive(true);
        return objectToSpawn;
    }

    public void ReturnTheDurians(GameObject gameObject)
    {
        if(durianTeethObjectsPool.TryGetValue(gameObject.tag, out Queue<GameObject> durianTeethList))
        {
            durianTeethList.Enqueue(gameObject);
        }
        else
        {
            Queue<GameObject> newObjectQueue = new Queue<GameObject>();
            newObjectQueue.Enqueue(gameObject);
            durianTeethObjectsPool.Add(gameObject.tag, newObjectQueue);
        }
        gameObject.SetActive(false);
    }


}
