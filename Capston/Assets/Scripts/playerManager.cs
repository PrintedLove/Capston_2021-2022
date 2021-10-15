using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    private float movementSpeed;
    private float jumpSpeed;
    private bool isDead;
    private bool isGrounded;
    Rigidbody2D rigidBody;
    SpriteRenderer spriteRenderer;
    Animator animator;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        isDead = false;
        isGrounded = false;
        movementSpeed = 0.1f;
        jumpSpeed = 9f;
    }

    void Update()
    {
        //Horizontal ��ư�� ������ �� �������� ĳ���� ��������Ʈ�� ������ �Լ�.
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }
        //�Ȱ� �ִ��� üũ�ؼ� �ִϸ��̼��� �����ϴ� ������ �ٲٴ� �Լ�.
        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1)
        {
            animator.SetBool("isWalk", true);
        }
        else
        {
            animator.SetBool("isWalk", false);
        }
    }

    void FixedUpdate()
    {
        
        if (!isDead)
        {
            float H_input = Input.GetAxisRaw("Horizontal");
            //Horizontal��ư�� ������ �̵���Ű�� �Լ�
            if(Input.GetButton("Horizontal"))
            {
                if(H_input == 1)
                {
                    if (Physics2D.OverlapBox(new Vector2(transform.position.x + 0.229166675f, transform.position.y), new Vector2(0.45833335f, 1.4f), 0, LayerMask.GetMask("Floor")))
                    {

                    }
                    else
                    {
                        transform.Translate(Vector2.right * H_input * movementSpeed);
                    }
                }
                if (H_input == -1)
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
            
            //���� �پ����� ��쿡�� ������ �����ִ� �Լ�
            if (isGrounded)
            {
                
                float J_input = Input.GetAxisRaw("Jump");

                if (J_input == 1)
                {
                    if(rigidBody.velocity.y < 9)
                    {
                        rigidBody.AddForce(Vector2.up * J_input * jumpSpeed, ForceMode2D.Impulse);
                    }  
                }


            }
        }
        //overlapbox(1,2,3,4) 1�� ��ġ���� 2�� ũ���� ������ 3�� ȸ���� ������Ʈ�� 4�� ���̾��̸� true
        if (Physics2D.OverlapBox(new Vector2(transform.position.x,transform.position.y - 0.8333f), new Vector2(0.7f, 0.1f), 0, LayerMask.GetMask("Floor")))
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
}
