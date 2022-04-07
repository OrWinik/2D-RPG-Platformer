using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Enemy : MonoBehaviour
{
    private GameObject Player;
    public GameObject floatingDamage;
    public GameObject coinPrefab;
    public Transform explosion;
    private Animator anim;
    public MissionManager mission;

    // movement
    private SpriteRenderer sprite;
    public float enemySpeed = 5f;
    private Vector3 movement;
    private BoxCollider2D enemyColl;
    private float movementCooldown = 3f;

    //Damage and HP
    private float monsterdamage;
    public float monsterHP;
    private int activeScene;
    public float expForKill;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("MainPlayer");
        mission = GameObject.FindObjectOfType<MissionManager>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        enemyColl = GetComponent<BoxCollider2D>();
        movement = Vector3.right;
        activeScene = SceneManager.GetActiveScene().buildIndex;
        monsterHP = 20 + ((activeScene-2) * 10);
        monsterdamage = 10 + ((activeScene - 2) * 10);
        expForKill = 5 +(activeScene - 2) * 2.5f;
    }

    void Update()
    {
        transform.position += movement * Time.deltaTime * enemySpeed ;
        movementCooldown -= Time.deltaTime;
        if(movementCooldown <= 0)
        {
            movement = new Vector3(Random.Range(0, 2), 0, 0);
            movementCooldown = 3f;
        }

        if(monsterHP <= 0)
        {
            MonsterDeath(expForKill);
        }

    }


    public void FlipX()
    {
        if (sprite.flipX == false)
        {
            sprite.flipX = true;
        }
        else if (sprite.flipX == false)
        {
            sprite.flipX = true;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainPlayer")
        {
            collision.GetComponent<Player>().ChangeHp(monsterdamage);
        }

        if (collision.tag == "Objects")
        {
            FlipX();
            movement.x = movement.x * -1;
        }
    }

    public void ChangeHpMonster(float damage)
    {
        monsterHP -= damage;
        anim.SetTrigger("GotHit");
        FindObjectOfType<AudioManager>().play("EnemyHit");
        FloatingDamage(damage.ToString());
    }

    public void MonsterDeath(float exp)
    {
        Destroy(this.gameObject);
        Player.GetComponent<Player>().ChangeEXP(expForKill);
        Instantiate(coinPrefab, transform.position + new Vector3 (0,-0.5f,0), coinPrefab.transform.rotation);
        Instantiate(explosion, transform.position, transform.rotation);
        mission.GetComponent<MissionManager>().AddKill(1);
    }

    public void FloatingDamage(string Text)
    {
        if(floatingDamage)
        {
            GameObject FloatingPrefab = Instantiate(floatingDamage,transform.position + new Vector3(0,2,0), Quaternion.identity);
            FloatingPrefab.GetComponentInChildren<TextMesh>().text = Text;
        }
    }

}
