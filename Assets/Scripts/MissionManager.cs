using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionManager : MonoBehaviour
{
    public GameObject openMission;
    public GameObject greenMission;
    public GameObject yellowMission;
    public GameObject blueMission;
    public GameObject bossMission;
    public GameObject player;

    [SerializeField] private int amout2kill = 20;
    public int amoutKilled = 0;
    public TextMeshProUGUI amout2killText;
    public TextMeshProUGUI amoutKilledText;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainPlayer");
    }

    public void Update()
    {
        ActiveMissionOrder();
        amout2killText.text = amout2kill.ToString();
        amoutKilledText.text = amoutKilled.ToString();
    }

    public void ActiveMissionOrder()
    {
        if (greenMission.activeInHierarchy == true)
        {
            openMission = greenMission;
            blueMission.SetActive(false);
            yellowMission.SetActive(false);
            bossMission.SetActive(false);
        }
        else if(yellowMission.activeInHierarchy == true)
        {
            openMission = yellowMission;
            blueMission.SetActive(false);
            greenMission.SetActive(false);
            bossMission.SetActive(false);

        }
        else if (blueMission.activeInHierarchy == true)
        {
            openMission = yellowMission;
            greenMission.SetActive(false);
            yellowMission.SetActive(false);
            bossMission.SetActive(false);
        }
        else if (bossMission.activeInHierarchy == true)
        {
            openMission = bossMission;
            greenMission.SetActive(false);
            yellowMission.SetActive(false);
            blueMission.SetActive(false);
        }
    }

    public void FinishMission()
    {
        if(amoutKilled >= amout2kill)
        {
            greenMission.SetActive(false);
            yellowMission.SetActive(false);
            blueMission.SetActive(false);
            bossMission.SetActive(false);
            player.GetComponent<Player>().Coins(30);
        }
    }


    public void AddKill(int addOnePoint)
    {
        amoutKilled += addOnePoint;
    }


}
