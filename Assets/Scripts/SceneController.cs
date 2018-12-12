using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * The SceneControler class handles the intital spawning of enemies.
 * It chooses five random NPCs to spawn randomly within the environment while
 * also removing any dead enemies from the list and making sure that there
 * are no more than ten enemies in the environment.
 **/
public class SceneController : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject[] NPCsPrefabs;
    public int numberOfEnemies = 0;

    GameObject gameOverCanvas;
    GameObject player;

    List<GameObject> NPCs = new List<GameObject>();

    AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        source.Play();
        player = (GameObject)Instantiate(playerPrefab, new Vector3(0, 0, 0), transform.rotation);
        //gameOverCanvas = GameObject.FindGameObjectWithTag("Game Over");
        //gameOverCanvas.SetActive(false);
        SpawnEnemies(5);
	}
	
	// Update is called once per frame
	void Update () {

        //if (!isAlive)
        //    gameOverCanvas.SetActive(true);
        //    if (Input.GetKeyDown(KeyCode.R))

        //        //Calls the function to shoot a bullet
        //        RestartGame();


    }

    void RestartGame()
    {
        Destroy(player);
        wipeEnemies();
        gameOverCanvas.SetActive(false);
        Start();
    }

    void wipeEnemies()
    {
        foreach (GameObject enemy in NPCs)
        {
            Destroy(enemy);
        }

        numberOfEnemies = 0;
    }

    public void SpawnEnemies(int n)
    {
        numberOfEnemies--;
        int spawnableEnemies = 0;
        if (numberOfEnemies >= 10)
        {
            return;
        }
        else if (numberOfEnemies >= 8 && numberOfEnemies < 10)
        {
            spawnableEnemies = (numberOfEnemies + n) - 10;

            for (int i = 0; i < spawnableEnemies; i++)
            {
                Vector3 respawnLocation = Random.insideUnitSphere * 22;
                respawnLocation.y = 15;
                int r = Random.Range(0, NPCsPrefabs.Length);
                NPCs.Add((GameObject)Instantiate(NPCsPrefabs[r], respawnLocation, transform.rotation));
                numberOfEnemies++;
            }
        }
        else
        {
            for (int i = 0; i < n; i++)
            {
                Vector3 respawnLocation = Random.insideUnitSphere * 22;
                respawnLocation.y = 1;
                int r = Random.Range(0, NPCsPrefabs.Length);
                NPCs.Add((GameObject)Instantiate(NPCsPrefabs[r], respawnLocation, transform.rotation));
                numberOfEnemies++;
            }
        }


    }
}
