using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  The PlayerController class handles the player's input from their keyboard, in other words 
 *  the player object's foot movement. It keeps track whether the player is alive or dead and 
 *  contraints its movement options depending on that state. It also handles weapon swiching and 
 *  and firing the the weapons bullet. 
 **/

public class PlayerController : MonoBehaviour
{

    public bool enabled { get; set; }
    public AudioClip step;

    float h;
    float v;
    float movementSpeed = 175f;
    float timeBetweenSteps = 0.6f;
    float timer;

    Rigidbody rb;
    Vector3 movement;
    AudioSource player;


    bool moving = false;
    bool jumping = false;
    bool crouching = false;
    bool objectiveHeld = false;

    /**
     * gets rigidbody and animator components
     **/
    void Awake()
    {
        GameObject.FindGameObjectWithTag("Inventory").SetActive(false);
        enabled = true;
        player = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    /**
     * Input detection per frame
     **/
    void Update()
    {
        if (!enabled) return;

        timer += Time.deltaTime;

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        moving = h != 0f || v != 0f;

        bool isMoving = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W);

        if (isMoving && timer > timeBetweenSteps)
        {
            timer = 0f;
            player.clip = step;
            player.Play();
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumping = true;
        }

        if (Input.GetKeyDown(KeyCode.C)) { crouching = !crouching; }

        if (Input.GetKeyDown(KeyCode.P)) { Time.timeScale = 0; }
    }

    /**
     * Handles any physics movement and controls it the physics on each physics frame
     **/
    void FixedUpdate()
    {

        //Detect player jump action
        if (jumping && isGrounded())
        {
            Jump();
        }

        //Dectect player movement
        if (moving) { Movement(h, v); }

        Crouch();

    }

    bool isGrounded()
    {
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            float distance = hit.distance;
            if (distance >= 1f && distance <= 1.25f)
            {
                return true;
            }
            return false;
        }

        //if there is nothing directly below, no jumping is allowed
        return false;
    }

    void Movement(float h, float v)
    {
        movement = (h * transform.right + v * transform.forward).normalized;
        Vector3 y = new Vector3(0, rb.velocity.y, 0);

        //Walk if Left Shift key is not pressed
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            timeBetweenSteps = 0.6f;
            rb.velocity = movement * movementSpeed * Time.deltaTime;
        }
        //Run if Left Shift Key is pressed and held
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            timeBetweenSteps = 0.4f;
            rb.velocity = movement * (movementSpeed * 2) * Time.deltaTime;
        }
        rb.velocity += y;
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 6f, rb.velocity.z);
        jumping = false;
    }

    void Crouch()
    {
        switch (crouching)
        {
            case true:
                transform.localScale = new Vector3(1, 0.75f, 1);
                break;
            case false:
                transform.localScale = new Vector3(1, 1, 1);
                break;
            default:
                break;
        }
    }

    public void setObjectiveStatus(bool status)
    {
        objectiveHeld = status;
    }

    public void deathAnimation()
    {
        rb.freezeRotation = true;
        transform.Rotate(-45, 0, 0);
    }
}
