using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BystanderFlee : MonoBehaviour {

    Animator anim;
    PlayerController player;
    NavMeshAgent agent;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        agent = GetComponent<NavMeshAgent>();

	}
	
	// Update is called once per frame
	void Update () {
		
        if (GetComponent<PlayerDetector>().playerWithinDistance)
        {
            anim.SetBool("IsAlerted", true);
            if (GetComponent<PlayerDetector>().alerted)
            {
                //Make bystander run away
                anim.SetBool("IsWalking", true);
                Vector3 dirToPlayer = transform.position - player.transform.position;
                Vector3 newPosition = transform.position + dirToPlayer + new Vector3(100, 0, 0);

                agent.SetDestination(newPosition);

            } else
            {
                //anim.SetBool("IsAlerted", false);
                anim.SetBool("IsWalking", false);
            }
        } else
        {
            anim.SetBool("IsAlerted", false);
        }
	}
}
