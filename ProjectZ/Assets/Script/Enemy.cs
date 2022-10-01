using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public int curHealth;

    Rigidbody rigid;
    BoxCollider boxCollider;

    Material mat;//B46 �ǰݷ���

    public Transform Target;// B48 �׺���̼�
    NavMeshAgent nav;// B48 �׺���̼�
    bool isChase;

    Animator anim;// B48 �ִϸ��̼�

    private void Awake()//B46
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;//B46 �ǰݷ��� //GetComponent -> GetComponentInChildren ���� B48 ������Ʈ �߰�
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();// B48 �ִϸ��̼�

        Invoke("ChaseStart", 2);
    }

    private void OnTriggerEnter(Collider other)//B46
    {
        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);
            StartCoroutine(OnDamage(reactVec, false));//B46 �ǰݷ���
        }
    }
    private void OnTriggerStay(Collider other)//B46{
    {
        if (other.tag == "Fire")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec, false));//B46 �ǰݷ���
        }
    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)//B46 �ǰݷ���
    {
        mat.color = Color.red;
        yield return new WaitForSeconds(0.1f);

        if(curHealth > 0)
        {
            mat.color = Color.white;
            reactVec = reactVec.normalized;
            reactVec += Vector3.up;

            rigid.AddForce(reactVec * 5, ForceMode.Impulse);
        }
        else
        {
            mat.color = Color.gray;
            gameObject.layer = 14;
            isChase = false;// B48 �ִϸ��̼�
            nav.enabled = false;// B48 �ִϸ��̼�
            anim.SetTrigger("doDie");// B48 �ִϸ��̼�
            Destroy(gameObject, 4);//4���մ� ����
        }
    }

    public void HitByGrenade(Vector3 explosionPos)//B47 ����ź�ǰ�
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));
    }

    void ChaseStart()// B48 �ִϸ��̼�
    {
        isChase = true;
        anim.SetBool("isWalk", true);
    }

    void Start()
    {
        
    }    
    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    void FixedUpdate()
    {
        FreezeVelocity();
    }

    // Update is called once per frame
    void Update()
    {

        if (isChase)// B48 �ִϸ��̼�
            nav.SetDestination(Target.position);
    }
}
