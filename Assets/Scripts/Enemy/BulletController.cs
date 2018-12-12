using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * The BulletController Class handles the amount of damage
 * and/or actions that it does after colliding with specificed objects
 * that have a tag.
 **/
public class BulletController : MonoBehaviour
{

    GameObject[] bulletType;

    /**
     * Determines the object that the bullet collided with and does a
     * action based on that.
     **/
    void OnTriggerEnter(Collider other)
    {
        var hitObject = other.gameObject;
        Debug.Log(hitObject.tag);

        if (hitObject.tag == "Player")
        {
            hitObject.GetComponent<PlayerHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
        if (hitObject.tag == "Enemy")
        {
            hitObject.GetComponent<EnemyHealth>().TakeDamage(10);
            Destroy(gameObject);
        }
    }

}
