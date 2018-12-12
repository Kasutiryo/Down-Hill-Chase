using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour {

    public GameObject projectilePrefab;
    public GameObject projectileSpawn;

    GameObject M4_Armed;
    GameObject M4_Unarmed;
    Animator anim;
    EnemyHealth enemyHealth;

    bool once = false;
    float shootingTimer;
    float timeBetweenShots = 1f;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        enemyHealth = GetComponent<EnemyHealth>();

        M4_Unarmed = GameObject.FindGameObjectWithTag("M4 Unarmed");
        M4_Armed = GameObject.FindGameObjectWithTag("M4 Armed");

        M4_Armed.SetActive(false);
        M4_Unarmed.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

        if (enemyHealth.dead && !once)
        {
            once = true;
            M4_Armed.SetActive(false);
            M4_Unarmed.SetActive(false);
            Destroy(gameObject, 3f);
            return;
        }

        if (!enemyHealth.dead && GetComponent<PlayerDetector>().playerWithinDistance)
        {
            if (GetComponent<PlayerDetector>().alerted)
            {
                shootingTimer += Time.deltaTime;
                anim.SetBool("IsShooting", true);
                if (shootingTimer >= timeBetweenShots)
                {
                    Fire();
                    shootingTimer = 0;
                }
                M4_Armed.SetActive(true);
                M4_Unarmed.SetActive(false);
            }
            else
            {
                anim.SetBool("IsShooting", false);
                M4_Armed.SetActive(false);
                M4_Unarmed.SetActive(true);
            }
        } else if (enemyHealth.dead)
        {
            M4_Armed.SetActive(false);
            M4_Unarmed.SetActive(true);
        }
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

