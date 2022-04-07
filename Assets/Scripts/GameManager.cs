using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] Monster;
    public int ActiveScene;
    public GameObject[] maxMonsters;
    public GameObject spawnPoint1;
    public GameObject spawnPoint2;
    private float spawnCoolDown = 3;


    public void Start()
    {
        ActiveScene = SceneManager.GetActiveScene().buildIndex;
        if(ActiveScene == 5)
        {
            SpawnBoss();
        }
    }

    void Update()
    {
        spawnCoolDown -= Time.deltaTime;

        if (spawnCoolDown <= 0 && ActiveScene != 5)
        {
            SpawnMonsters();
            maxMonsters = GameObject.FindGameObjectsWithTag("Enemy");
            spawnCoolDown = 3;
        }
    }

    public void SpawnMonsters()
    {
        if(maxMonsters.Length <=6)
        {
            Instantiate(Monster[ActiveScene - 2], spawnPoint1.transform.position, Monster[ActiveScene - 2].transform.rotation);
            Instantiate(Monster[ActiveScene - 2], spawnPoint2.transform.position, Monster[ActiveScene - 2].transform.rotation);
        }
    }

    public void SpawnBoss()
    {
        Instantiate(Monster[ActiveScene - 2], spawnPoint2.transform.position + new Vector3 (0,1,0), Monster[ActiveScene - 2].transform.rotation);

    }

}
