using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurianTeethCommonButton : MonoBehaviour
{
    public DurianTeethInventory durianTeethInventory;
    private DurianTeethObjectPool durianTeethObjectPool;
    public Button commonDurianButton;
    private Button insidingCommonDurianButton;
    public GameObject whereToPloop;
    private GameObject ploop;
    
    public delegate void OnDurianTeethDurianIsFired(GameObject gameObject);
    public OnDurianTeethDurianIsFired onDurianTeethDurianIsFiredCallback;

    private void Start()
    {
        // The inventory is already instantiated at DurianTeethInventory.cs. Here you are calling the static object!
        durianTeethInventory = DurianTeethInventory.dTInventory;
        // Yes, there are the external and internal sides of the button cscripting.
        insidingCommonDurianButton = commonDurianButton.GetComponent<Button>();
        // I Want to return the durian back to ground
        durianTeethObjectPool = FindObjectOfType<DurianTeethObjectPool>();
       
        // And listeners are added at the Start()
        insidingCommonDurianButton.onClick.AddListener(delegate{ RemovingFromWakid("Common Durian", 1); });
    }
    
    public void RemovingFromWakid(string wordys, int readyToLaunch)
    {
        // If you call Durian Teeth Inventory at Awake(), you get a NullReferenceExcepetion error!
        if (durianTeethInventory != null)
            { durianTeethInventory.RemoveFromWakid(wordys, readyToLaunch); }
        else
            { Debug.Log("How can this be null?"); }

        #region To Wakid
       
        // Throwing the durian back out of the wakid, you know
        ploop = durianTeethObjectPool.ReleaseTheDurians(wordys);
        if (onDurianTeethDurianIsFiredCallback != null)
            onDurianTeethDurianIsFiredCallback.Invoke(ploop);
        #endregion
    }

    
}
