using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            print("Hit " + collision.gameObject.name);
            
            CreateBulletImpactEffect(collision);
            
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            print("Hit a wall ");
            
            CreateBulletImpactEffect(collision);
            
            Destroy(gameObject);
        }
        
        if (collision.gameObject.CompareTag("Beer"))
        {
            print("Hit a Beer");
            
            collision.gameObject.GetComponent<DestructibleObject>().Break();
        }
    }
    
    private void CreateBulletImpactEffect(Collision objectWeHit)
    {
        ContactPoint contact = objectWeHit.contacts[0];

        GameObject hole = Instantiate(
            GlobalReferences.Instance.bulletImpactEffect,
            contact.point,
            Quaternion.LookRotation(contact.normal)
        );
        
        hole.transform.SetParent(objectWeHit.gameObject.transform);
    }
}
