using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianTeethInventory : MonoBehaviour
{
    #region Singleton
    public static DurianTeethInventory dTInventory;
    private void Awake()
    {
        dTInventory = this;
    }
    #endregion

    #region Delegates

    public delegate void OnDurianTeethAdded(int i);
    public OnDurianTeethAdded onDurianTeethAddedCallback;
    public delegate void OnDurianTeethMinused(int i);
    public OnDurianTeethMinused onDurianTeethMinusedCallback;

    // This goes to PlayerPicking.cs
    public delegate void TooFullAlready(int i);
    public TooFullAlready tooFullAlreadyCallback;
    public delegate void NotFullAnymore(int i);
    public NotFullAnymore notFullAnymoreCallback;

    // This goes to Inventory UISlot.cs
    public delegate void EmptyAlready(int i);
    public EmptyAlready emptyAlreadyCallback;
    public delegate void NotEmptyAnymore(int i);
    public NotEmptyAnymore notEmptyAnymoreCallback;

    #endregion

    public string[] durianTeethObjectsList;
    public int[] totalSpace;
    public int[] usedSpace;


    private Dictionary<string, int> durianTeethInventoryTally = new Dictionary<string, int>();

    private void Start()
    {
        for(int i = 0; i < usedSpace.Length; i++)
        {
            usedSpace[i] = 0;
        }
    }

    public void AddToWakid(string durianTeethObjects, int amount)
    {
        for(int i = 0; i  < durianTeethObjectsList.Length; i++)
        {
            if(durianTeethObjectsList[i] == durianTeethObjects)
            {
                if(usedSpace[i] <= totalSpace[i])
                {
                    usedSpace[i] += amount;
                    if (notEmptyAnymoreCallback != null)
                        notEmptyAnymoreCallback.Invoke(i);
                    if (onDurianTeethAddedCallback != null)
                        onDurianTeethAddedCallback.Invoke(i);
                }
                else
                {
                    Debug.Log("Too Full Already!");
                    if (tooFullAlreadyCallback != null)
                        tooFullAlreadyCallback.Invoke(i);
                }
            }
            
            
        }
    }
    public void RemoveFromWakid(string durianTeethObjects, int amount)
    {
        for (int i = 0; i < durianTeethObjectsList.Length; i++)
        {
            if (durianTeethObjectsList[i] == durianTeethObjects)
            {
                if (usedSpace[i] == 0)
                {
                    Debug.Log("Empty Already!");
                    if (emptyAlreadyCallback != null)
                        emptyAlreadyCallback.Invoke(i);
                }
                else
                {
                    usedSpace[i] -= amount;
                    if (onDurianTeethMinusedCallback != null)
                        onDurianTeethMinusedCallback.Invoke(i);
                }
            }
        }
    }
}
