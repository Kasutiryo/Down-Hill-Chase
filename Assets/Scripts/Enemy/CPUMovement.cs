using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CPUMovement : MonoBehaviour {

    public GameObject flag;

    NavMeshAgent agent;
    Animator anim;
    EnemyHealth enemyHealth;

    float thinkingPeriod = 5f;
    float timer;

	// Use this for initialization
	void Start () {

        enemyHealth = GetComponent<EnemyHealth>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        agent.autoBraking = false;

        agent.SetDestination(RandomNavSphere(transform.position, 50f, -1));
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<PlayerDetector>().playerWithinDistance) { return; }

        if (!agent.isStopped && agent.remainingDistance <= 1)
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
            Vector3 waypoint = RandomNavSphere(transform.position, 50f, -1);
            float distanceToNextWaypoint = Vector3.Distance(transform.position, waypoint);
            if (distanceToNextWaypoint <= 10f)
            {
                return;
            } else
            {
                agent.SetDestination(waypoint);
            }
        }
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        timer = 0;

        //InstantiateWayPoint(randomDirection);

        anim.SetBool("IsWalking", true);

        return navHit.position;
    }

    void InstantiateWayPoint(Vector3 destination)
    {
        var projectile = (GameObject)Instantiate
            (flag,
            destination,
            transform.rotation);
    }
}
