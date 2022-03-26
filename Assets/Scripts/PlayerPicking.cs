using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerPicking : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private int durianNumbers;
    public DurianTeethInventory durianTeethInventory;
    private bool canStillPickOrNot = true;

    void Start()
    {
        durianTeethInventory = DurianTeethInventory.dTInventory;
        durianTeethInventory.tooFullAlreadyCallback += DontPickAnymore;
        durianTeethInventory.notFullAnymoreCallback += CanPickAgain;
        cam = Camera.main;
    }
    private void Update()
    {
        // Apparently PointerEventData is for UI, so have to use Input to pick durians
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = cam.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                DurianFruitBehaving durianFruitBehaving = hit.collider.GetComponent<DurianFruitBehaving>();
                if (durianFruitBehaving != null && durianFruitBehaving.rb.useGravity == true)
                {
                    string objectName = durianFruitBehaving.objectName;

                    // How to implement picking?
                    if(canStillPickOrNot == true)
                    {
                        durianFruitBehaving.Picking();

                        // Tally the durians here, not at DurianIsBehaving.cs !
                        durianNumbers++;
                    }
                    else
                        { Debug.Log("Don't Pick Don't Pick We Bring Back To Cart"); }
                    
                    

                    // Try call the AddToWakid method
                    durianTeethInventory.AddToWakid(durianFruitBehaving.objectName, 1);
                }
            }
        }

        #if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 50f))
            {
                DurianFruitBehaving durianFruitBehaving = hit.collider.GetComponent<DurianFruitBehaving>();
                if (durianFruitBehaving != null && durianFruitBehaving.rb.useGravity == true)
                {
                    string objectName = durianFruitBehaving.objectName;

                    // How to implement picking?
                    if (canStillPickOrNot == true)
                    { 
                        durianFruitBehaving.Picking();

                        // Tally the durians here, not at DurianIsBehaving.cs !
                        durianNumbers++;
                    }
                    else
                    { Debug.Log("Don't Pick Don't Pick We Bring Back To Cart"); }

                    // Try call the AddToWakid method
                    durianTeethInventory.AddToWakid(durianFruitBehaving.objectName, 1);
                }
                
            
            }
        }
        #endif
    }

    private void DontPickAnymore(int i)
    {
        canStillPickOrNot = false;
    }

    private void CanPickAgain(int i)
    {
        canStillPickOrNot = true;
    }
}
