using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player;
    private Vector3 movement;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("MainPlayer") != null)
        {
            Player = GameObject.FindGameObjectWithTag("MainPlayer");
        }
        else
            Player = GameObject.FindGameObjectWithTag("BackPortal");

        movement = new Vector3 (0,2,-10);
    }

    void LateUpdate()
    {
        transform.position = Player.transform.position + movement ;
    }

}
