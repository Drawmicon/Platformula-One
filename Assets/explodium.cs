using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodium : MonoBehaviour
{
    //array of collider objects
    private Collider[] hitColliders;

    public float radius = 5.0F;//distance of exposion
    public float power = 10.0F;//force of explosion

    public LayerMask explosionLayer;//layer of objects affected by explosion

    public bool bouncy = false;

    private void OnCollisionEnter(Collision collision)
    {
        ExplosionWork(collision.contacts[0].point);

        if (!bouncy)//if false, object gets destroyed on collision
        {
            Destroy(gameObject);
        }
    }

    void ExplosionWork(Vector3 explosionPoint)
    {
        //add all objects affected by explosion to array
        hitColliders = Physics.OverlapSphere(explosionPoint, radius, explosionLayer);
        
        //for each object in array
        foreach (Collider hitCol in hitColliders)
        {
            //check if array object has rigidbody, aka not null
            if (hitCol.GetComponent<Rigidbody>() != null)
            {
                //
                hitCol.GetComponent<Rigidbody>().isKinematic = false;
                hitCol.GetComponent<Rigidbody>().AddExplosionForce(power, explosionPoint, radius,1, ForceMode.Impulse);
            }
        }
    
    }

    /*
    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }
    }*/
}
