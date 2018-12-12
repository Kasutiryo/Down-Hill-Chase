using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BystanderFlee : MonoBehaviour {

    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
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
