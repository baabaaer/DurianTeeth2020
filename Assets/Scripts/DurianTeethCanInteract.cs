using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurianTeethCanInteract : MonoBehaviour
{
    // We need to make a coloured border when we see that the object is within range of us to pick!
    // Also, this is a base class

    public float range = 15f;
    private float distance;
    public Transform whereToInteract;
    public Transform player;
    
    public virtual void Picking() { }
    
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.position, whereToInteract.position);
            if(distance <= range)
            {
                Picking();
            }
        
    }

    private void OnDrawGizmosSelected()
    {
        if (whereToInteract == null)
            whereToInteract = transform;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(whereToInteract.position, range);
    }
}
