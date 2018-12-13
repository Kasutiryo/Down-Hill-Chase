using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * ~~~~~~~~~WEAPON CLASS~~~~~~~~~~~
 * 
 **/

public class Weapon_old : MonoBehaviour {

    public float timeBetweenShots = 2f;
    public bool enabled { get; set; }
    public bool automatic;
    public bool semi_automatic;
    public bool unlocked { get; set; }
    public AudioClip shot;
    public GameObject projectilePrefab;
    public GameObject projectileSpawn;
    public int damage = 10;
    Camera camera;
    float timer;
    AudioSource player;


    /**
     * Use this for initialization
     **/
    void Start () {
        enabled = true;
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GetComponent<AudioSource>();
        player.clip = shot;
	}

    /*
     * FixedUpdate is called once per physics frame. Generally used to control
     * any movement or physics of a GameObject.
     **/
    void Update () {

        if (!enabled) return;

        timer += Time.deltaTime;

        if (automatic && Input.GetButton("Fire1") && timer > timeBetweenShots)

            //Calls the function to shoot a bullet
            Fire();

        if (semi_automatic && Input.GetButtonDown("Fire1") && timer > timeBetweenShots)
            
            //Calls the function to shoot a bullet
            Fire();
    }


    void DetermineAction(GameObject obj)
    {
        Debug.Log("Object hit: " + obj.tag);
        EnemyHealth health = obj.GetComponent<EnemyHealth>();
        switch (obj.tag)
        {
            case "Enemy One":
                health.TakeDamage(damage);
                break;
            case "Enemy Two":
                health.TakeDamage(damage);
                break;
            default:
                Debug.Log("Object has no tag or no action on this object is required");
                break;
        }
    }

    /**
     * Creates a new bullet on the weapon's bulletSpawn and shoots it
     * at a default speed of 50 units per frame towards the bulletSpawns 
     * current Z direction.
     **/
    public void Fire()
    {
        timer = 0f;
        player.Play();
        Vector3 point = new Vector3(camera.pixelWidth / 2, camera.pixelHeight / 2, 0);
        //Ray ray = camera.ScreenPointToRay(point);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        //{
        //    Debug.Log(hit.collider.tag);
        //    GameObject hitObject = hit.transform.gameObject;
        //    DetermineAction(hitObject);
        //}
        var projectile = (GameObject)Instantiate
        (projectilePrefab,
         projectileSpawn.transform.position,
         projectileSpawn.transform.rotation);
        projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * 50f;
        Destroy(projectile, 5f);
    }

}
