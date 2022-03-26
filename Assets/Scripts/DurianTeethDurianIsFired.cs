using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianTeethDurianIsFired : MonoBehaviour
{
    
    public Transform whereToPloop;
    public float ploopSpeed = 30f;
    public DurianTeethCommonButton durianTeethCommonButton;

    private Vector3 ploopDestination;
    private Camera ploopCam;

    private void Awake()
    {
        durianTeethCommonButton = FindObjectOfType<DurianTeethCommonButton>();
    }
    // Start is called before the first frame update
    void Start()
    {
        ploopCam = Camera.main;
        durianTeethCommonButton.onDurianTeethDurianIsFiredCallback += LooseTheDurian;
    }

    private void LooseTheDurian(GameObject ploop)
    {
        Rigidbody ploopRb = ploop.GetComponent<Rigidbody>();
        // You need cannonbal-based equations for launching the durian, not camera ray based!

        
        // Call a UI first to allow for aiming and force management!
        /* Ray ray = ploopCam.ScreenPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) 
            { ploopDestination = hit.point; }
        else 
            { ploopDestination = ray.GetPoint(1000); } */

        ploop.transform.position = new Vector3(whereToPloop.position.x, whereToPloop.position.y, whereToPloop.position.z);
        ploopRb.velocity = ploopCam.transform.forward * ploopSpeed;

        ploopRb.useGravity = true;
        

        

        Debug.Log("Blonk satu kali!");

    }
}
