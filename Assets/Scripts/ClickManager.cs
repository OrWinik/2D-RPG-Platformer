using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] private Camera MainCam;
    private string nameOfCollider;
    public GameObject PotionNPC;
    public GameObject WeaponNPC;
    public bool shopIsOpen = false;

    public void Start()
    {
        PotionNPC = GameObject.FindGameObjectWithTag("Npc");
        WeaponNPC = GameObject.FindGameObjectWithTag("NpcWeapons");
    }

    public void Update()
    {
        ClickNPC();
    }

    public void ClickNPC()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = MainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if(hit)
            {
                if (hit.collider.tag == "Npc")
                {
                    PotionNPC.GetComponent<PotionsShop>().Shop(shopIsOpen);
                }
                else if (hit.collider.tag == "NpcWeapons")
                {
                    WeaponNPC.GetComponent<WeaponShop>().ShopOpen(false);
                }
                else if(hit.collider.tag == "Mission")
                {
                    if (hit.collider.name == "NPC_Blue")
                    {
                        hit.collider.GetComponent<NPCMissions>().blueMission.SetActive(true);
                    }
                    if (hit.collider.name == "NPC_Yellow")
                    {
                        hit.collider.GetComponent<NPCMissions>().yellowMission.SetActive(true);
                    }
                    if (hit.collider.name == "NPC_Green")
                    {
                        hit.collider.GetComponent<NPCMissions>().greenMission.SetActive(true);
                    }
                    if (hit.collider.name == "NPC_Boss")
                    {
                        hit.collider.GetComponent<NPCMissions>().bossMission.SetActive(true);
                    }
                }
                else
                    return;
            }
        }
    }

}
