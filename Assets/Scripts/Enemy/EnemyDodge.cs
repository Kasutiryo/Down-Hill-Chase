using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDodge : MonoBehaviour {

    Animator anim;
    PlayerDetector detector;
    NavMeshAgent agent;

    float dodgeCooldown = 5f;
    float timer;
    float animTimer = 2;
    float timer2;
	// Use this for initialization
	void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        detector = GetComponent<PlayerDetector>();
	}
	
	// Update is called once per frame
	void Update () {

        timer += Time.deltaTime;
        
        if (detector.alerted && timer >= dodgeCooldown)
        {
            timer = 0;
     
            System.Random ran = new System.Random();
            
            switch(ran.Next(1, 3))
            {
                case 1:
                    anim.SetBool("DodgeLeft", true);
                    agent.velocity += new Vector3(7, 0, 2);
                    StartCoroutine(dodgeLeftAnimation());
                    break;

                case 2:
                    anim.SetBool("DodgeRight", true);
                    agent.velocity -= new Vector3(7, 0, 2);
                    StartCoroutine(dodgeRightAnimation());
                    break;
                default:
                    Debug.Log("Something went wrong!");
                    break;

            }
        }
	}

    IEnumerator dodgeRightAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        agent.velocity.Set(0, 0, 0);
        anim.SetBool("DodgeRight", false);
    }

    IEnumerator dodgeLeftAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        agent.velocity.Set(0, 0, 0);
        anim.SetBool("DodgeLeft", false);
    }
}
