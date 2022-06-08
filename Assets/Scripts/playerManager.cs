using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    private float movementSpeed;
    private float jumpSpeed;
    private float skill2_movement = 15;
    //쿨타임 변수
    private float coolTime1_start;
    private float coolTime1_current;
    private float coolTime1_skill = 4;
    private bool isCoolTime1;

    private float coolTime2_start;
    private float coolTime2_current;
    private float coolTime2_skill = 3;
    private bool isCoolTime2;

    private float coolTime3_start;
    private float coolTime3_current;
    private float coolTime3_skill = 10;
    private bool isCoolTime3;

    private bool isDead;
    private bool isGrounded;
    [SerializeField] private Transform pos;
    private Vector2 attackRange;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    [SerializeField] private GameObject Bullet1;
    [SerializeField] private GameObject Bullet2;
    [SerializeField] private GameObject Bullet3;
    public static bool flipx;
    GameManager GM;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        GM = GetComponent<GameManager>();

        isDead = false;
        isGrounded = false;
        movementSpeed = 0.1f;
        jumpSpeed = 9f;
        attackRange = new Vector2(0.9166667f, 1.666667f);
        Physics.IgnoreLayerCollision(8, 8, true);
    }

    void Update()
    {
        //방향전환시 스프라이트 뒤집기
        if (Input.GetButton("Horizontal") && !animator.GetBool("isAttack"))
        {

            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            flipx = spriteRenderer.flipX;
        }
        //방향전환시 공격범위 변경
        if (spriteRenderer.flipX == true)
        {
            pos.position = new Vector2(transform.position.x - 0.9166667f, pos.position.y);
        }
        else
        {
            pos.position = new Vector2(transform.position.x + 0.9166667f, pos.position.y);
        }
        //이동시 걷는 애니메이션 출력
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && !animator.GetBool("isAttack") && isGrounded)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }

        checkCool1();
        checkCool2();
        checkCool3();
    }

    void FixedUpdate()
    {
        
        if (!isDead)
        {
            float H_input = Input.GetAxisRaw("Horizontal");


            //땅에 닿아있을때
            if (isGrounded)
            {
                animator.SetBool("isGround", true);
                animator.SetBool("isJump", false);
                //공중에 있을시 player_jump2 애니메이션 출력
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump2") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump2_skill")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    animator.SetBool("isFall", true);
                }
                //착지시 player_jump3 애니메이션 출력
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump3") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump3_skill")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    animator.SetBool("isFall", false);
                }
                //점프
                float J_input = Input.GetAxisRaw("Jump");

                if (J_input == 1 && !animator.GetBool("isAttack"))
                {
                    if (rigidBody.velocity.y < 9)
                    {
                        rigidBody.AddForce(Vector2.up * J_input * jumpSpeed, ForceMode2D.Impulse);
                        animator.SetBool("isJump", true);
                    }
                }
                //attackCount 0 1 2 각각 1 2 3타 각 애니메이션이 50퍼센트 ~ 끝나기 전에 공격키 누를시 다음 공격
                if (animator.GetInteger("attackCount") == 0 && !animator.GetBool("isAttack"))
                {
                    if (animator.GetBool("isSkill3") == true)
                    {
                        if (Input.GetButton("Attack1"))
                        {
                            animator.SetBool("isAttack", true);
                            animator.SetInteger("attackCount", 1);
                            //1타뎀
                            attackDamage(GameManager.Instance.Return1AD());
                        }
                    }
                    else
                    {
                        if (Input.GetButton("Attack1"))
                        {
                            animator.SetBool("isAttack", true);
                            animator.SetInteger("attackCount", 1);
                            //1타뎀
                            attackDamage(GameManager.Instance.Return1AD());
                            
                        }
                    }

                }
                else if (animator.GetInteger("attackCount") == 1)
                {
                    if (animator.GetBool("isSkill3") == true)
                    {
                        //1타뎀 3번째 
                        attacker("player_attack1_skill", 2, GameManager.Instance.Return1AD());
                    }
                    else
                    {
                        //1타뎀 3번째
                        attacker("player_attack1", 2, GameManager.Instance.Return1AD());
                    }
                }
                else if (animator.GetInteger("attackCount") == 2)
                {
                    if (animator.GetBool("isSkill3") == true)
                    {
                        //2타뎀 3번째
                        attacker("player_attack2_skill", 3, GameManager.Instance.Return2AD());                  
                    }
                    else
                    {
                        //2타뎀 3번째
                        attacker("player_attack2", 3, GameManager.Instance.Return2AD());
                    }
                }
                else if (animator.GetInteger("attackCount") == 3)
                {
                    if (animator.GetBool("isSkill3") == true)
                    {
                        //3타뎀 3번째
                        attacker("player_attack3_skill", 1, GameManager.Instance.Return3AD());
                    }
                    else
                    {
                        //3타뎀 3번째
                        attacker("player_attack3", 1, GameManager.Instance.Return3AD());
                    }
                }
                //공격끝

                //스킬1
                if ((Input.GetButton("Skill1")) && (animator.GetBool("isSkill1") == false) && (animator.GetBool("isAttack") == false) && !isCoolTime1)
                {
                    animator.SetBool("isSkill1", true);
                    animator.SetBool("isAttack", true);
                    //Q스킬뎀
                    attackDamage(GameManager.Instance.ReturnSlash());
                    coolTime1_start = Time.time;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_skill1") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isSkill1", false);
                }

                //스킬2
                if (Input.GetButton("Skill2") && (animator.GetBool("isSkill2") == false) && (animator.GetBool("isAttack") == false) && !isCoolTime2)
                {
                    animator.SetBool("isSkill2", true);
                    animator.SetBool("isAttack", true);
                    //W스킬뎀
                    attackDamage(GameManager.Instance.ReturnRush());
                    skill2Move();
                    coolTime2_start = Time.time;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_skill2") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isSkill2", false);
                    rigidBody.velocity = Vector2.zero;
                    gameObject.layer = 7;
                }

                //스킬3
                if (Input.GetButton("Skill3") && (animator.GetBool("isSkill3") == false) && (animator.GetBool("isAttack") == false) && !isCoolTime3)
                {
                    //E데미지는 bulletManager 참고 
                    animator.SetBool("isSkill3", true);
                    animator.SetBool("isAttack", true);
                    coolTime3_start = Time.time;
                }
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("player_skill3") && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    animator.SetBool("isAttack", false);
                    Invoke("skill3_Off", 5);
                }
            }
            //isGrounded
            else
            {
                //점프 시작 애니메이션 완료시 
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump1") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump1_skill")) && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
                {
                    animator.SetBool("isJump", false);
                }
                //착지 애니메이션 완료시
                if ((animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump3") || animator.GetCurrentAnimatorStateInfo(0).IsName("player_jump3_skill")) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
                {
                    animator.SetBool("isFall", false);
                }
                animator.SetBool("isGround", false);
            }

            //이동
            if (Input.GetButton("Horizontal"))
            {
                //오른쪽
                if (H_input == 1 && !animator.GetBool("isAttack"))
                {
                    //벽이 있는지 체크
                    if (Physics2D.OverlapBox(new Vector2(transform.position.x + 0.229166675f, transform.position.y), new Vector2(0.45833335f, 1.4f), 0, LayerMask.GetMask("Floor")))
                    {

                    }
                    else
                    {
                        transform.Translate(Vector2.right * H_input * movementSpeed);
                    }
                }
                //왼쪽
                if (H_input == -1 && !animator.GetBool("isAttack"))
                {
                    if (Physics2D.OverlapBox(new Vector2(transform.position.x - 0.229166675f, transform.position.y), new Vector2(0.45833335f, 1.4f), 0, LayerMask.GetMask("Floor")))
                    {

                    }
                    else
                    {
                        transform.Translate(Vector2.right * H_input * movementSpeed);
                    }
                }


            }


        }
        //바닥 체크
        if (Physics2D.OverlapBox(new Vector2(transform.position.x, transform.position.y - 0.8333f), new Vector2(0.7f, 0.1f), 0, LayerMask.GetMask("Floor")))
        {
            isGrounded = true;

            rigidBody.drag = 3;
            rigidBody.gravityScale = 3;
        }
        else
        {
            isGrounded = false;

        }


    }

    //연속 공격 관련 함수
    void attacker(string animation, int next, int damage)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            if (Input.GetButton("Attack1"))
            {
                animator.SetBool("isAttack", true);
                animator.SetInteger("attackCount", next);

                attackDamage(damage);

            }
        }
        //애니메이션이 끝났을 경우 attackCount 초기화 
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animation) && (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f))
        {
            animator.SetBool("isAttack", false);
            animator.SetInteger("attackCount", 0);
        }
    }
    //enemy 데미지 받게하는 함수 !추후 스킬 범위도 개별적용할 예정
    void attackDamage(int damage)
    {
        //hitEnemy = OverlapBox 안에 있는 Enemy 레이어의 모든 오브젝트
        Collider2D[] hitEnemy = Physics2D.OverlapBoxAll(pos.position, attackRange, 0, LayerMask.GetMask("Enemy"));
        //hitEnemy 안에 있는 모든 오브젝트에게 enemyDamaged 실시
        foreach (Collider2D collider in hitEnemy)
        {
            collider.gameObject.GetComponent<enemyManager>().enemyDamaged(damage);

        }
    }

    void skill3_Off()
    {
        animator.SetBool("isSkill3", false);
        animator.SetBool("isAttack", false);
        animator.SetInteger("attackCount", 0);
    }

    //OnCollisionEnter2D - 현재 오브젝트가 다른 오브젝트의 콜라이더2d와 닿을때 호출
    //Enemy 태그를 가진 오브젝트와 충돌시 onDamaged
    void OnCollisionEnter2D(Collision2D other)
    {
        if(gameObject.layer == 7)
        {
            if (other.gameObject.tag == "Enemy" && other.gameObject.layer == 8)
            {
                onDamaged(other.transform.position.x, 10);
            }
        }
        if(gameObject.layer == 11) // W스킬 데미지
        {
            other.gameObject.GetComponent<enemyManager>().enemyDamaged(GameManager.Instance.ReturnRush());
            rigidBody.velocity = Vector2.zero;
        }       

    }
    //몬스터와 반대방향으로 튀어오르고 레이어 변경 - 일정시간 후 OffDamaged 함수 호출 !추후 데미지 추가 
    public void onDamaged(float Enemy_X,int Enemy_damage)
    {
        int dirc = transform.position.x - Enemy_X > 0 ? 1 : -1;
        gameObject.layer = 9;
        //무적색상 변경
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        rigidBody.AddForce(new Vector2(dirc * 7, 5), ForceMode2D.Impulse);
        //무적시간
        Invoke("OffDamaged", 1);

        GameManager.Instance.PlayerDamage(Enemy_damage);
    }
    //레이어,캐릭터 색상 변경
    void OffDamaged()
    {
        gameObject.layer = 7;
        
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void fire(int a)
    {
        switch (a)
        {
            case 1:
                Instantiate(Bullet1, new Vector2(transform.position.x,transform.position.y + 0.3f), transform.rotation);
                break;
            case 2:
                Instantiate(Bullet2, transform.position, transform.rotation);
                break;
            case 3:
                Instantiate(Bullet3, new Vector2(transform.position.x, transform.position.y + 0.3f), transform.rotation);
                break;
            default:
                break;
        }
        
    }
    //스킬2 이동
    void skill2Move()
    {
        int dirc = spriteRenderer.flipX == true ? -1 : 1;
        rigidBody.AddForce(new Vector2(dirc * skill2_movement, 0), ForceMode2D.Impulse);
        gameObject.layer = 11;
    }
    //쿨타임
    void checkCool1()
    {
        coolTime1_current = Time.time - coolTime1_start;
        if (coolTime1_current < coolTime1_skill)
        {
            isCoolTime1 = true;
        }
        else
        {
            isCoolTime1 = false;
        }
    }

    void checkCool2()
    {
        coolTime2_current = Time.time - coolTime2_start;
        if (coolTime2_current < coolTime2_skill)
        {
            isCoolTime2 = true;
        }
        else
        {
            isCoolTime2 = false;
        }
    }

    void checkCool3()
    {
        coolTime3_current = Time.time - coolTime3_start;
        if (coolTime3_current < coolTime3_skill)
        {
            isCoolTime3 = true;
        }
        else
        {
            isCoolTime3 = false;
        }
    }

    void ting()
    {
        Application.Quit();
    }
    //기즈모
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(pos.position, attackRange);
    }


}