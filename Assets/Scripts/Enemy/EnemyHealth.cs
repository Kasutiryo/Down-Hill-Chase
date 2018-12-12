using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/**
 * The Health Class controls the amount of health the player or enemy has
 * and reduces it accordlngly. It also controls the amount of health is healed.
 **/

public class EnemyHealth : MonoBehaviour
{

    public const int maxHealth = 100;
    public int currentHealth = maxHealth;
    public RectTransform healthBar;
    public AudioClip death;

    public bool dead { get; set; }
    AudioSource source;
    Animator anim;
    NavMeshAgent agent;

    void Start()
    { 
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

        dead = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }    
    }
    /**
     * Subtracts x amount of health from the health component of the game object. Calls Die function
     * if health is zero.
     **/
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    /**
     *  This method is only to be used if there is no scene controller active in the 
     *  game world. It disables any input of from the player, acting as if dead.s
     **/
    public void Die()
    {
        anim.SetBool("Die", true);
        dead = true;
        agent.isStopped = true;
        Destroy(gameObject, 3f);

    }
}
