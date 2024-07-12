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

    //애니메이션 공격 모션 속도와 쿨타임 모션 속도가 같아야 데미지 정확히 들어감, 이미지 자연스러움
    private float curTime = 0.3f; // 
    public float coolTime; 

    public int damage = 1; // 공격력

    public float Move_Speed = 3f; // 이동속도

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

        //hp바 확인
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("체력 -1");
            if (Cur_Hp >= 0)
            {
                Cur_Hp -= 1;
            }
            hp_bar.value = (float) Cur_Hp / (float) Max_Hp;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("체력 +1");
            if (Cur_Hp <= 100)
            Cur_Hp += 1;
            hp_bar.value = (float)Cur_Hp / (float)Max_Hp;
        }



        //공격 박스 위치 조정
        // 나중에 set float 마지막 위치 기반으로 박스 위치를 옮길 수 있게 ㄱㄱ
        //좌우
        if (Input.GetAxisRaw("Horizontal") < 0) // 왼쪽
        {

            pos.position = new Vector3(transform.position.x -0.75f, transform.position.y, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0) // 오른쪽
        {
            pos.position = new Vector3(transform.position.x + 0.75f, transform.position.y, 0);
        }


        // 상하
        if (Input.GetAxisRaw("Vertical") < 0) //아래
        {
            pos.position = new Vector3(transform.position.x, transform.position.y - 0.75f, 0);
        }
        else if (Input.GetAxisRaw("Vertical") > 0) // 위
        {
            pos.position = new Vector3(transform.position.x, transform.position.y + 0.75f, 0);
        }



        //이동
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



        //공격

        if (curTime <= 0)
        {
            if (Input.GetKey(KeyCode.X))
            {
                
                foreach (Collider2D collider in collider2Ds)
                 {
                      if(collider.tag == "Enemy")
                    {
                        Debug.Log("맞음");
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


        //애니메이션
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
