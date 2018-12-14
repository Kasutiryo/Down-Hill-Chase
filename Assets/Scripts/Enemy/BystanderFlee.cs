using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BystanderFlee : MonoBehaviour {

    Animator anim;
    PlayerController player;
    NavMeshAgent agent;

    public GameObject caution;
    public GameObject exclamation;

    float timeBetweenWaypoint = 10f;
    float timer;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 15f)
        {
            anim.SetBool("IsWalking", true);
            if (timer >= timeBetweenWaypoint)
            {
                Vector3 destination = GetComponent<CPUMovement>().RandomNavSphere(this.transform.position, 50f, -1);
                if (Vector3.Distance(transform.position, destination) <= 10f)
                {
                    return;
                }
                GetComponent<CPUMovement>().InstantiateWayPoint(destination);
                timer = 0;
                agent.SetDestination(destination);
            }
            
        }
	}
}
