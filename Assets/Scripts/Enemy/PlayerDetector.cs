using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetector : MonoBehaviour {

    public AudioClip alert;
    public bool alerted { get; set; }
    public bool playerWithinDistance { get; set; }

    Vector3 playerPosition;
    GameObject player;
    GameObject caution;
    GameObject exclamation;
    NavMeshAgent agent;
    Animator anim;
    EnemyHealth enemyHealth;

    AudioSource audioPlayer;

    float distanceFromPlayer;
    float shootingTimer;
    float timeBetweenShots = 1f;
   
	// Use this for initialization
	void Start () {

        //enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();

        player = GameObject.FindGameObjectWithTag("Player");
        exclamation = GameObject.FindGameObjectWithTag("Exclamation");
        caution = GameObject.FindGameObjectWithTag("Caution");

        exclamation.SetActive(false);
        caution.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        
        //if (enemyHealth.dead)
        //{
        //    exclamation.SetActive(false);
        //    caution.SetActive(false);
        //    return;
        //}

        playerPosition = player.transform.position;
        distanceFromPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceFromPlayer <= 15f)
        {
            playerWithinDistance = true;
            caution.SetActive(true);
            agent.isStopped = true;
            anim.SetBool("IsWalking", false);
            if (distanceFromPlayer <= 10f)
            {
                agent.isStopped = true;
                transform.LookAt(new Vector3(playerPosition.x, playerPosition.y - 0.8f, playerPosition.z));
                caution.SetActive(false);
                exclamation.SetActive(true);
                if (!alerted)
                {
                    alerted = true;
                    audioPlayer.clip = alert;
                    audioPlayer.Play();
                }
            }
            else
            {
                alerted = false;
                exclamation.SetActive(false);
                caution.SetActive(true);
            }
        }
        else
        {
            playerWithinDistance = false;
            exclamation.SetActive(false);
            caution.SetActive(false);
        }
    }
}
