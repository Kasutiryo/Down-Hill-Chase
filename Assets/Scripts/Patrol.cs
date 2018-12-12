using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol : MonoBehaviour {

    public Transform[] points;              //Points to test agent movement
    public GameObject flag;                 //GameObject to visually represent waypoint
    public AudioClip alert;                 //Audio Clip to player when enemy is alerted
    public GameObject projectilePrefab;
    public GameObject projectileSpawn;

    GameObject player;                      //Reference to player GameObject to check distance
    GameObject exclamation;                 //GameObject to visualize alerted enemy
    GameObject caution;
    GameObject M4_Armed;
    GameObject M4_Unarmed;
    NavMeshAgent agent;                     //Reference to NavMeshAgent
    Animator anim;                          //Reference to Animator
    AudioSource audioPlayer;                //Audio Player to load and play sound effects

    int destPoint = 0;                      //

    float thinkingPeriod = 5f;              //Time between assigning new waypoint
    float timer;                            //Variable to keep time since last waypoint assigned
    float timeBetweenShots = 1f;
    float shootingTimer;
    bool alerted;




    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        exclamation = GameObject.FindGameObjectWithTag("Exclamation");
        caution = GameObject.FindGameObjectWithTag("Caution");
        M4_Armed = GameObject.FindGameObjectWithTag("M4 Armed");
        M4_Unarmed = GameObject.FindGameObjectWithTag("M4 Unarmed");

        agent.autoBraking = false;
        exclamation.SetActive(false);
        caution.SetActive(false);
        M4_Armed.SetActive(false);
        M4_Unarmed.SetActive(true);

        GotoNextPoint();

	}
	
    void GotoNextPoint() {

        //Return if there is no testing destinations
        if (points.Length == 0) { return; }

        //Assign next destination
        agent.destination = points[destPoint].position;

        //Create FLAG on waypoint (for testing purposes only)
        InstantiateWayPoint();

        //Randomize next waypoint
        destPoint = (destPoint + 1) % points.Length;

        //Reset timer
        timer = 0;

        //Set walking animation
        anim.SetBool("IsWalking", true);


    }

	// Update is called once per frame
	void Update () {


        if(!agent.isStopped && agent.remainingDistance <= 1)
        {
            Debug.Log("PATROL: Agent has reached it's destination and has stopped");
            agent.isStopped = true;
        } 
        else if (agent.isStopped)
        {
            anim.SetBool("IsWalking", false);
            timer += Time.deltaTime;
        }

        if (timer > thinkingPeriod)
        {
            agent.isStopped = false;
            
            GotoNextPoint();
            
        }

        Vector3 playerPosition = player.transform.position;
        float distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceFromPlayer <= 10f)
        {
            caution.SetActive(true);
            agent.isStopped = true;
            anim.SetBool("IsWalking", false);
            if (distanceFromPlayer <= 5f)
            {
                //agent.isStopped = true;
                shootingTimer += Time.deltaTime;
                transform.LookAt(new Vector3(playerPosition.x, 0.5f, playerPosition.z));
                anim.SetBool("IsShooting", true);
                if (shootingTimer >= timeBetweenShots) { Fire(); shootingTimer = 0; }
                caution.SetActive(false);
                exclamation.SetActive(true);
                M4_Armed.SetActive(true);
                M4_Unarmed.SetActive(false);
                if (!alerted)
                {
                    alerted = true;
                    audioPlayer.clip = alert;
                    audioPlayer.Play();
                }

            } else
            {
                anim.SetBool("IsShooting", false);
                alerted = false;
                exclamation.SetActive(false);
                caution.SetActive(true);
                M4_Armed.SetActive(false);
                M4_Unarmed.SetActive(true);
            }
        } else
        {
            exclamation.SetActive(false);
            caution.SetActive(false);
            M4_Unarmed.SetActive(true);
            M4_Armed.SetActive(false);
        }



        //DEBUGGING LOGS
        Debug.Log("The agent's angular speed is " + agent.angularSpeed);
        //Debug.Log("Time since last waypoint " + timer);
        Debug.Log("Is the agent stopped? - " + agent.isStopped);
        //Debug.Log("Distance between the agent and the player is " + distanceFromPlayer);
	}

    void InstantiateWayPoint()
    {
        var projectile = (GameObject)Instantiate
            (flag,
            points[destPoint].position,
            points[destPoint].rotation);
    }

    void Fire()
    {
        var projectile = (GameObject)Instantiate
                (projectilePrefab,
                 projectileSpawn.transform.position,
                 projectileSpawn.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 25f;
        Destroy(projectile, 5f);
    }
}
