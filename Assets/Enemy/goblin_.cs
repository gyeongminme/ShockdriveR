using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class goblin_ : MonoBehaviour
{
    public int Hp;

    private float curTime = 0f; // 공격 딜레이 시간
    public float coolTime = 2f;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        Hp = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //맞고 몇초 지난 뒤에 false로 전환 어떻게?? 몇 초 뒤에 . ? animator.SetBool("Hit", false); 필요

    }

    public void TakeDamage(int damage) {// 때렸을 때 

        

        animator.SetBool("Hit", true);
        Hp = Hp - damage;
       

               

            
       
    } 
}
