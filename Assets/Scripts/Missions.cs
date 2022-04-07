using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Missions : MonoBehaviour
{
    public string missionName;
    public GameObject missionCanvas;
    public int amout2kill;
    public int amoutKilled;
    public TextMeshProUGUI amount2killText;
    public TextMeshProUGUI amountKilledText;

}
