using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchAttackAlert : MonoBehaviour {

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

            if(GetComponent<PlayerDetector>().alerted)
            {
                alertNearByEnemies();
            }

        } else
        {
            anim.SetBool("IsAlerted", false);
        }

	}

    void alertNearByEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy One");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            
            if (distance <= 25)
            {
                enemy.GetComponent<PlayerDetector>().Agro();
            }
        }
    }


}
