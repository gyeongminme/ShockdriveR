using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    private Animator animator;

    bool Player_Moving;
    Vector2 Last_Moving;

    public Transform pos;
    public Vector2 boxSize;

    //�ִϸ��̼� ���� ��� �ӵ��� ��Ÿ�� ��� �ӵ��� ���ƾ� ������ ��Ȯ�� ��, �̹��� �ڿ�������
    private float curTime = 0.3f; // 
    public float coolTime; 

    public int damage = 1; // ���ݷ�

    public float Move_Speed = 3f; // �̵��ӵ�

    public Slider hp_bar;
    private float Cur_Hp = 100;
    private float Max_Hp = 100;


    // Start is called before the first frame update
    void Start()
    {

        animator = GetComponent<Animator>();
            
        hp_bar.value = Cur_Hp / Max_Hp;
    }

    // Update is called once per frame
    void Update()
    {
        Player_Moving = false;

        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);

        //hp�� Ȯ��
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("ü�� -1");
            if (Cur_Hp >= 0)
            {
                Cur_Hp -= 1;
            }
            hp_bar.value = (float) Cur_Hp / (float) Max_Hp;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("ü�� +1");
            if (Cur_Hp <= 100)
            Cur_Hp += 1;
            hp_bar.value = (float)Cur_Hp / (float)Max_Hp;
        }



        //���� �ڽ� ��ġ ����
        // ���߿� set float ������ ��ġ ������� �ڽ� ��ġ�� �ű� �� �ְ� ����
        //�¿�
        if (Input.GetAxisRaw("Horizontal") < 0) // ����
        {

            pos.position = new Vector3(transform.position.x -0.75f, transform.position.y, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) // ������
        {
            pos.position = new Vector3(transform.position.x + 0.75f, transform.position.y, 0);
        }


        // ����
        if (Input.GetAxisRaw("Vertical") < 0) //�Ʒ�
        {
            pos.position = new Vector3(transform.position.x, transform.position.y - 0.75f, 0);
        }
        else if (Input.GetAxisRaw("Vertical") > 0) // ��
        {
            pos.position = new Vector3(transform.position.x, transform.position.y + 0.75f, 0);
        }



        //�̵�
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Horizontal") < 0)
        {
            
            Player_Moving = true;
            Last_Moving = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * Move_Speed * Time.deltaTime, 0f, 0f));
        }

        if (Input.GetAxisRaw("Vertical") > 0 || Input.GetAxisRaw("Vertical") < 0)
        {
            Player_Moving = true;
            Last_Moving = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * Move_Speed * Time.deltaTime, 0f));
        }



        //����

        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                
                foreach (Collider2D collider in collider2Ds)
                 {
                      if(collider.tag == "Enemy")
                    {
                        Debug.Log("����");
                        collider.GetComponent<goblin_>().TakeDamage(damage);
                    }

                 }
                animator.SetTrigger("Attack");
    
                }
                 curTime = coolTime;
        }
        else
        {
            curTime -= Time.deltaTime;

        }


        //�ִϸ��̼�
        animator.SetFloat("Move_X", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("Move_Y", Input.GetAxisRaw("Vertical"));
        animator.SetBool("Player_Moving", Player_Moving);
        animator.SetFloat("Last_Move_X", Last_Moving.x);
        animator.SetFloat("Last_Move_Y", Last_Moving.y);


    }




    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);

    }



}
