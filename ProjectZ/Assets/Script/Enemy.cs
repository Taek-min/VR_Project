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

    Material mat;//B46 피격로직

    public Transform Target;// B48 네비게이션
    NavMeshAgent nav;// B48 네비게이션
    bool isChase;

    Animator anim;// B48 애니매이션

    private void Awake()//B46
    {
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        mat = GetComponentInChildren<MeshRenderer>().material;//B46 피격로직 //GetComponent -> GetComponentInChildren 수정 B48 오브젝트 추가
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();// B48 애니매이션

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
            StartCoroutine(OnDamage(reactVec, false));//B46 피격로직
        }
    }
    private void OnTriggerStay(Collider other)//B46{
    {
        if (other.tag == "Fire")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            StartCoroutine(OnDamage(reactVec, false));//B46 피격로직
        }
    }

    IEnumerator OnDamage(Vector3 reactVec, bool isGrenade)//B46 피격로직
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
            isChase = false;// B48 애니매이션
            nav.enabled = false;// B48 애니매이션
            anim.SetTrigger("doDie");// B48 애니매이션
            Destroy(gameObject, 4);//4초잇다 죽음
        }
    }

    public void HitByGrenade(Vector3 explosionPos)//B47 수류탄피격
    {
        curHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage(reactVec, true));
    }

    void ChaseStart()// B48 애니매이션
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

        if (isChase)// B48 애니매이션
            nav.SetDestination(Target.position);
    }
}
