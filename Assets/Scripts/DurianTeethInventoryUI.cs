using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurianTeethInventoryUI : MonoBehaviour
{ // Since the slot will never be destroyed, I don't make an Inventory Slot script
    private int i;
    
    public Button braveDurianButton;
    public Button commonDurianButton;
    public Button unloadBraveDurian;
    public Button unloadCommonDurian;
    public Text braveDurianTally;
    public Text commonDurianTally;
    public DurianTeethInventory durianTeethInventory;
    

    private void Awake()
    {
        durianTeethInventory = DurianTeethInventory.dTInventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        durianTeethInventory.onDurianTeethAddedCallback += AddToTally;
        durianTeethInventory.onDurianTeethMinusedCallback += RemoveFromTally;
        durianTeethInventory.emptyAlreadyCallback += EmptyAlready;
        durianTeethInventory.notEmptyAnymoreCallback += NotEmptyAnymore;

        braveDurianButton.interactable = false;
        commonDurianButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddToTally(int i)
    {
        if(i == 0)
        {
            braveDurianTally.text = durianTeethInventory.usedSpace[0].ToString();
        }
        if (i == 1)
        {
            commonDurianTally.text = durianTeethInventory.usedSpace[1].ToString();
        }
    }

    private void RemoveFromTally(int i) 
    {
        if (i == 0)
        {
            braveDurianTally.text = durianTeethInventory.usedSpace[0].ToString();
        }
        if (i == 1)
        {
            commonDurianTally.text = durianTeethInventory.usedSpace[1].ToString();
        }
    }
    
    private void EmptyAlready(int i)
    {
        if (i == 0)
        {
            braveDurianButton.interactable = false;
        }
        if (i == 1)
        {
            commonDurianButton.interactable = false;
        }
    }

    private void NotEmptyAnymore(int i)
    {
        if (i == 0)
        {
            
            braveDurianButton.interactable = true;
        }
        if (i == 1)
        {
            
            commonDurianButton.interactable = true;
        }
    }

    
}
