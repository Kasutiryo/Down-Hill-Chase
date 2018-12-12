using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  The AIController class dicates the specific movement of the 
 *  enemies. It controls whether they're allowed to shoot the player
 *  and notice if the player is within their range. It will either run away
 *  from the player or follow the play and shoot the player to death.
 **/

public class EnemyOneController : MonoBehaviour {

   
    public float withinRange = 3f;
    public GameObject projectilePrefab;
    public Transform projectileSpawn;
    public AudioClip fireCast;
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    bool jumping = false;
    bool shooting = false;
    public bool moving = false;
    bool playerInRange;

    float h;
    float v;
    float speed = 175f;

    Rigidbody rb;
    Vector3 movement;
    Animator anim;
    AudioSource source;
    GameObject player;
    PlayerHealth playerHealth;
    float timer;


    void Awake () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;

        if (timer >= timeBetweenAttacks && playerInRange)
        {
            Attack();
        }
        
        moving = h != 0f || v != 0f;

        //checks per frame if character is in the air or on ground
        anim.SetBool("IsGrounded", isGrounded());
    }

    void FixedUpdate()
    {
        if (jumping && isGrounded())
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsJumping", true);
            Jump();
        }

        anim.SetBool("IsWalking", moving);

        if (moving)
        {
            Movement(h, v);
        }
   }

    void Attack()
    {
        timer = 0f;
        transform.LookAt(player.transform);
        if (playerHealth.currentHealth > 0)
        {
            source.PlayOneShot(fireCast);
            anim.SetBool("IsShooting", true);
            var projectile = (GameObject)Instantiate
                (projectilePrefab,
                 projectileSpawn.position,
                 projectileSpawn.rotation);
            projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 10f;
            Destroy(projectile, 2f);
            playerHealth.TakeDamage(attackDamage);
            anim.SetBool("IsShooting", false);
        }
    }

     void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 6f, rb.velocity.z);
        jumping = false;
    }

    void Movement(float h, float v)
    {
        movement = (h * transform.right + v * transform.forward).normalized;
        Vector3 y = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = movement * speed * Time.deltaTime;
        rb.velocity += y;
    }

    bool isGrounded()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            float distance = hit.distance;

            if (distance >= 0f && distance <= 0.5f)
            {
                anim.SetBool("IsJumping", false);
                return true;
            }

            anim.SetBool("IsJumping", true);

            return false;
        }

        //if there is nothing directly below, no jumping is allowed
        return false;
    }

    void OnTriggerEnter(Collider other)
    {
        // If the entering collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is in range.
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If the exiting collider is the player...
        if (other.gameObject == player)
        {
            // ... the player is no longer in range.
            playerInRange = false;
        }
    }
}