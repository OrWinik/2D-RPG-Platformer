using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    //movement
    public SpriteRenderer playerSprite;
    private int playerSpeed = 5;
    private Rigidbody2D rb;
    private float jumpHight = 300f;
    private bool touchingGround = false;
    private Vector3 movement;

    //animations
    private Animator anim;

    //attack
    public Transform attackPoint;
    private float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public float playerDamage = 10;

    //cooldowns
    public float attackcooldown = 1f;
    public float gettingHitCooldown = 2f;

    //Health&EXP
    public float HP = 100;
    public float maxHP = 100;
    private Image healthImage;
    public float EXP = 0;
    public float maxEXP = 50;
    private Image expImage;
    public int lvl = 1;
    public TextMeshProUGUI lvlText;

    //potions Coins & Weapons
    [SerializeField]private int amountOfPotions = 5;
    public TextMeshProUGUI potionText;
    [SerializeField]private int coins = 0;
    public TextMeshProUGUI coinsText;
    private SpriteRenderer weapon;
    public Sprite[] weapons;
    public int weaponLvl = 1;

    public Transform deathMenu;
    public bool deathMenuOn = false;
    private int activeScene;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        expImage = transform.GetChild(2).GetChild(0).gameObject.GetComponent<Image>();
        healthImage = transform.GetChild(2).GetChild(1).gameObject.GetComponent<Image>();
        weapon = transform.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        deathMenu = transform.GetChild(5).gameObject.GetComponent<Transform>();
        activeScene = SceneManager.GetActiveScene().buildIndex;
        DeathMenu();
        if (activeScene == 1)
        {
            transform.position = new Vector3(23, -1, 0);
        }
    }

    void Update()
    {
        float Horizontal = Input.GetAxisRaw("Horizontal");

        movement = new Vector3(Horizontal, 0, 0);

        transform.position += Time.deltaTime * movement * playerSpeed;

        Jumping();

        flipX();

        attackcooldown -= Time.deltaTime;
        gettingHitCooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && attackcooldown<=0)
        {
            Attack();
            FindObjectOfType<AudioManager>().play("Attack");
            attackcooldown = 1;
        }

        if (Horizontal != 0)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }



        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            FindObjectOfType<AudioManager>().play("Walk");
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            FindObjectOfType<AudioManager>().Pause("Walk");
        }
        else
            return;

        healthImage.fillAmount = HP / maxEXP;
        expImage.fillAmount = EXP / maxEXP;

        PlayerLvlUp();
        Death();
        PotionUse();

        lvlText.text = "Level: "  + lvl.ToString();
        potionText.text = "X" + amountOfPotions.ToString();
        coinsText.text = coins.ToString();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            touchingGround = true;
        }
    }

    public void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && touchingGround == true)
        {
            anim.SetTrigger("Jump");
            rb.AddForce(Vector2.up * jumpHight);
            touchingGround = false;
        }

    }

    public void flipX()
    {
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (movement.x > 0)
        {
            transform.localScale = Vector3.one;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Ladders")
        {
            if(Input.GetKey(KeyCode.UpArrow))
            {
                transform.position += new Vector3 (0,0.2f,0);
            }
        }
    }

    public void Attack()
    {
        anim.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit enemy" + enemy.name);
            enemy.GetComponent<Enemy>().ChangeHpMonster(playerDamage);
        }
    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void PlayerLvlUp()
    {
        if(EXP >= maxEXP)
        {
            lvl++;
            EXP = 0;
            maxEXP = maxEXP + 25f;
        }
    }

    public void Death()
    {
        if(HP <= 0)
        {
            HP = 100;
            EXP = 0;
            playerSprite.enabled = false;
            if(deathMenuOn== false)
            {
                deathMenuOn = true;
                deathMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (HP > 100)
        {
            HP = 100;
        }
    }

    public void DeathMenu()
    {
        deathMenuOn = false;
        playerSprite.enabled = true;
        deathMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void ChangeHp(float damage)
    {
        if(gettingHitCooldown <= 0)
        {
            gettingHitCooldown = 2f;
            HP -= damage;
            anim.SetTrigger("PlayerGotHit");
            FindObjectOfType<AudioManager>().play("PlayerHit");
            UpdateHP();
        }

    }

    public void UpdateHP()
    {
        healthImage.fillAmount = HP / maxEXP;
    }

    public void ChangeEXP(float exp)
    {
        EXP += exp;
        UpdateEXP();
    }

    public void UpdateEXP()
    {
        expImage.fillAmount = EXP / maxEXP;
    }

    public void BuyingPotions(int PotionsBought, int Cost)
    {
        if(Cost>coins)
        {
            amountOfPotions += PotionsBought;
            coins -= Cost;
        }
    }

    public void PotionUse()
    {
        if (Input.GetKeyDown(KeyCode.H) && HP < 100 && amountOfPotions > 0)
        {
            amountOfPotions--;
            HP += 50;
            Debug.Log("Used Potion");
        }
    }

    public void UpgradeWeapon(int cost)
    {
        if(cost>coins)
        {
            weapon.sprite = weapons[weaponLvl + 1];
            weaponLvl++;
            coins -= cost * weaponLvl;
            playerDamage += 10 + (weaponLvl * 5);
        }
    }

    public void Coins(int addedCoins)
    {
        coins += addedCoins;
    }

    public void LoadButton()
    {
        HP = PlayerPrefs.GetFloat("HP",100);
        EXP = PlayerPrefs.GetFloat("EXP",1);
        PlayerPrefs.GetFloat("x",0);
        PlayerPrefs.GetFloat("y",0);
        PlayerPrefs.GetFloat("z",0);
        transform.position = new Vector3(PlayerPrefs.GetFloat("x", 0), PlayerPrefs.GetFloat("y", 0),PlayerPrefs.GetFloat("z", 0));
    }

    public void SaveButton()
    {
        PlayerPrefs.SetInt("LastScene", SceneManager.GetActiveScene().buildIndex);
        Debug.Log("SavedScene" + PlayerPrefs.GetInt("LastScene"));
        PlayerPrefs.SetFloat("HP", HP);
        PlayerPrefs.SetFloat("EXP",EXP);
        PlayerPrefs.SetFloat("x", playerSprite.transform.position.x);
        PlayerPrefs.SetFloat("y", playerSprite.transform.position.y);
        PlayerPrefs.SetFloat("z", playerSprite.transform.position.z);
    }
}