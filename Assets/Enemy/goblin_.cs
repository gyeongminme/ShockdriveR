using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class goblin_ : MonoBehaviour
{
    public int Hp;

    private float curTime = 0f; // ���� ������ �ð�
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
        //�°� ���� ���� �ڿ� false�� ��ȯ ���?? �� �� �ڿ� . ? animator.SetBool("Hit", false); �ʿ�

    }

    public void TakeDamage(int damage) {// ������ �� 

        

        animator.SetBool("Hit", true);
        Hp = Hp - damage;
       

               

            
       
    } 
}
