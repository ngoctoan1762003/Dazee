using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Header("Component")]
    Rigidbody2D r2D;

    //stat
    public float maxHp, maxMana, maxStamina, maxDefense, currentHealth, currentMana, currentStamina;

    public float speed = 10f, jumpForce = 400f, dashSpeed = 100f, fallSpeed, h, radius, xWallJump, yWallJump;
    public float startDashTime, currentDashTime, wallJumpTime, startWallJumpTime;

    public bool isGround, isOnWall, isWallSliding, isWallJump, isDoubleJump, isDashing, isFaceRight;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    public GameObject DashDustPrefab;

    public LayerMask groundLayer;

    public Transform attackPointR, attackPointL;
    public LayerMask Enemy;
    public float attackRadius, damage, attackDuration, attackCurrentTime;
    public bool attackReady = true;

    public Vector2 KnockForce;

    public GameObject WindBulletPrefab;
    public float bulletSpeed;

    public GameObject EarthShieldPrefab;

    public GameObject SoulFragmentUI;

    //Animation controller----------------------------
    public Animator anim;
    //Attack---------------------
    public int stepAttack;

    private void Awake()
    {
        if (GameManager.instance.isLoaded == false)
        {
            transform.position = new Vector2(GameManager.instance.playerPosX, GameManager.instance.playerPosY);
            GameManager.instance.isLoaded = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        r2D = gameObject.GetComponent<Rigidbody2D>();
        isDoubleJump = true;
        isDashing = false;
        isFaceRight = true;
        attackCurrentTime = attackDuration;
        maxHp = GameManager.instance.maxHp;
        maxMana = GameManager.instance.maxMana;
        maxStamina = GameManager.instance.maxStamina;
        maxDefense = GameManager.instance.maxDefense;
        damage = GameManager.instance.maxDamage;
        currentHealth = maxHp;
        currentMana = maxMana;
        currentStamina = maxStamina;

        //Animation Controller--------------------
        anim = gameObject.GetComponent<Animator>();
        stepAttack = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Run
        h = Input.GetAxis("Horizontal");
        r2D.velocity= new Vector2 (h*speed, r2D.velocity.y);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGround == true)
            {
                r2D.AddForce(Vector2.up * jumpForce);
                isDoubleJump = true;
            }
            else if (isOnWall == true && isWallSliding == true)
            {
                wallJumpTime = startWallJumpTime;
                isWallJump = true;
                isDoubleJump = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentStamina >= 100)
                {
                    if (isDoubleJump == true)
                    {
                        r2D.velocity = new Vector2(0, 0);
                        r2D.AddForce(Vector2.up * jumpForce);
                        isDoubleJump = false;
                        currentStamina -= 100;
                    }
                }
            }
        }

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (currentStamina >= 20)
            {
                anim.Play("Player_Dash");
                currentDashTime = startDashTime;
                isDashing = true;
                GameObject dashDust;
                dashDust = Instantiate(DashDustPrefab, (Vector2)transform.position - leftOffset / 3 + bottomOffset * 4, Quaternion.identity);
                currentStamina -= 200;
            }
        }

        if (isDashing == true)
        {
            currentDashTime -= Time.deltaTime;
            if (currentDashTime <= 0) isDashing = false;
            Dash();
        }

        //CheckFaceRight
        if((r2D.velocity.x<0 && isFaceRight==true)||(r2D.velocity.x>0 && isFaceRight == false))
        {
            Flip();
        }

        //CheckCollision
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset * 4, radius, groundLayer);
        isOnWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, radius, groundLayer) ||
            Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, radius, groundLayer);

        //Wall Interaction
        if (isOnWall == true && isGround == false && h != 0)
        {
            r2D.velocity = new Vector2(r2D.velocity.x, -fallSpeed);
            isWallSliding = true;
        }
        else isWallSliding = false;

        if (isWallJump)
        {
            wallJumpTime -= Time.deltaTime;
            r2D.velocity = new Vector2(-h * xWallJump, yWallJump);
            if (wallJumpTime <= 0)
            {
                isWallJump = false;
                r2D.velocity = Vector2.zero;
            }
        }

        //Attack
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(attackReady == true)
            {
                KnockBack(new Vector2(-150, 0));
                PlayAnimationAttack();
                if (isFaceRight == true)
                {
                    Collider2D[] hitEnemy = Physics2D.OverlapCircleAll((Vector2)transform.position + rightOffset + bottomOffset, attackRadius, Enemy);
                    foreach (Collider2D enemy in hitEnemy)
                    {
                        Debug.Log("Hit" + enemy.name);
                        enemy.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
                else
                {
                    Collider2D[] hitEnemy = Physics2D.OverlapCircleAll((Vector2)transform.position + leftOffset + bottomOffset, attackRadius, Enemy);
                    foreach (Collider2D enemy in hitEnemy)
                    {
                        Debug.Log("Hit" + enemy.name);
                        enemy.GetComponent<Enemy>().TakeDamage(damage);
                    }
                }
                attackReady = false;
            }
        }
        if (attackReady == false)
        {
            if (attackCurrentTime <= 0 && attackReady == false)
            {
                attackReady = true;
                attackCurrentTime = attackDuration;
            }
            attackCurrentTime -= Time.deltaTime;
        }

        //Shoot
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (currentMana >= 100)
            {
                if (isFaceRight)
                {
                    GameObject WindBullet;
                    WindBullet = Instantiate(WindBulletPrefab, (Vector2)attackPointR.position, Quaternion.identity) as GameObject;
                    WindBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
                    KnockBack(KnockForce);
                    currentMana -= 100;
                }
                else
                {
                    GameObject WindBullet;
                    WindBullet = Instantiate(WindBulletPrefab, (Vector2)attackPointL.position, Quaternion.identity) as GameObject;
                    WindBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
                    KnockBack(KnockForce);
                    currentMana -= 100;
                }
            }         
        }

        //Shield
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (currentMana >= 100)
            {
                if (isFaceRight)
                {
                    GameObject EarthShield;
                    EarthShield = Instantiate(EarthShieldPrefab, (Vector2)attackPointR.position + rightOffset * 3, Quaternion.identity) as GameObject;
                    currentMana -= 100;
                }
                else
                {
                    GameObject EarthShield;
                    EarthShield = Instantiate(EarthShieldPrefab, (Vector2)attackPointL.position - leftOffset * -3, Quaternion.identity) as GameObject;
                    currentMana -= 100;
                }
            }
        }

        //Restore hp,mana,stamina
        if (currentMana <= 100)
        {
            currentMana += Time.deltaTime * 30;
        }
        //if (currentHealth <= 100)
        //{
            //currentHealth += Time.deltaTime * 30;
        //}
        if (currentStamina <= 100)
        {
            currentStamina += Time.deltaTime * 100;
        }
    }

    //Dash 
    void Dash()
    {
        if (currentDashTime > 0)
        {
            r2D.velocity = new Vector2(0, 0);
            if (isFaceRight==true) r2D.velocity = Vector2.right * dashSpeed;
            if (isFaceRight==false) r2D.velocity = Vector2.right * -dashSpeed;
        }
    }

    //Flip
    void Flip()
    {
        isFaceRight = !isFaceRight;
        gameObject.GetComponent<SpriteRenderer>().flipX = !isFaceRight;
    }

    //Being hit
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Dead();
        }
    } 

    //Knock back
    void KnockBack(Vector2 KnockForce)
    {
        r2D.velocity = Vector2.zero;
        if (isFaceRight)
        {
            r2D.AddForce(new Vector2(-KnockForce.x, KnockForce.y));
        }
        else r2D.AddForce(new Vector2(KnockForce.x, KnockForce.y));
    }

    //Knock when being hit
    public void DamagedKnockBackForce(Vector2 DamagedKnockBackForce)
    {
        r2D.AddForce(DamagedKnockBackForce);
    }

    //Dead
    void Dead()
    {
        Debug.Log("Player dead");
        currentHealth = 0;
        currentMana = 0;
        currentStamina = 0;
    }

    //Animation Controller--------------------------
    void PlayAnimationAttack()
    {
        switch (stepAttack)
        {
            case 0: 
                anim.Play("Player_Attack1");
                stepAttack++;
                break;
            case 1:
                anim.Play("Player_Attack2");
                stepAttack++;
                break;
            case 2:
                stepAttack++;
                anim.Play("Player_Attack3");
                break;
            case 3:
                stepAttack = 0;
                anim.Play("Player_Attack4");
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset + bottomOffset, attackRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset * 4, radius);
    }
}
