using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadMainPlayer : MonoBehaviour
{
    public GameObject[] mainPlayer;

    void Start()
    {
        mainPlayer = GameObject.FindGameObjectsWithTag("MainPlayer");
        if (mainPlayer.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
