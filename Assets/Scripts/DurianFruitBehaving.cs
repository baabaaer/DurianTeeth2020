using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianFruitBehaving : DurianTeethCanInteract
{
    public float minTimeDrop;
    public float maxTimeDrop;
        
    private float timeDrop;
    private float timer;
    private Collider collision;
    public Rigidbody rb;
    public string objectName;
    
    public DurianTeethObjects durianTeethObjects;
    private DurianTeethObjectPool durianTeethObjectPool;
    
    public void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        collision = GetComponent<Collider>();
        durianTeethObjectPool = FindObjectOfType<DurianTeethObjectPool>();
        objectName = durianTeethObjects.objectName;
        
    }

    public void OnEnable()
    {
        timeDrop = Random.Range(minTimeDrop, maxTimeDrop);
        if(rb != null)
            rb.useGravity = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > timeDrop)
        {
            rb.useGravity = true;
        }
        // How do I set the durian to be pickable after it is fired?
    }

    public override void Picking()
    {
        base.Picking();
        gameObject.SetActive(false);
    }

    // Should I put a Shooting method here?
    

    private void OnDisable()
    {
        if(rb != null)
            rb.useGravity = false;
        // Need to cheeck if the pool exists to prevent Reference Exception error!
        if(durianTeethObjectPool != null)
        {
            durianTeethObjectPool.ReturnTheDurians(this.gameObject);
        }
        
       
    }
    
}
