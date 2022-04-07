using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadMission : MonoBehaviour
{
    public GameObject[] MissionManager;

    void Start()
    {
        MissionManager = GameObject.FindGameObjectsWithTag("MainPlayer");
        if (MissionManager.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
