using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range, FireGun };  //B43 변수생성
    public Type type;                   //B43 변수생성 무기타입
    public int damage;                  //B43 변수생성 데미지
    public float rate;                  //B43 변수생성 공격속도
    public BoxCollider meleeArea;       //B43 변수생성 
    public TrailRenderer trailEffect;   //B43 변수생성

    public Transform bulletPos;//B44 발사구현
    public GameObject bullet;//B44 발사구현

    public GameObject eventsys;
    public GameObject Gun;




    public float maxAmmo; //B44 재장전구현
    public float curAmmo; //B44 재장전구현
    public float oneAmmo; //B44 재장전구현

    public Animator anim;

    public void Awake()
    {
        UI UI = eventsys.GetComponent<UI>();
        maxAmmo += UI.AddAmmo;
        curAmmo += UI.AddAmmo;

    }



    // Start is called before the first frame update

    public void Use()                   //B43 공격로직(코루틴)
    {
        if (type == Type.Range && curAmmo > oneAmmo) //B44 발사구현 
        {
            curAmmo -= oneAmmo;
            StartCoroutine("Shot");
        }
    }


    void Reload()
    {
        if (type == Type.Range)
        {
            if (curAmmo < maxAmmo)
                curAmmo += Time.deltaTime;
            else
                curAmmo = maxAmmo;
        }
    }
    IEnumerator Swing() //B43 공격로직(코루틴)
    {
        //1
        yield return new WaitForSeconds(0.0f);//주어진 시간만큼 대기
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //2
        yield return new WaitForSeconds(0.0f);
        meleeArea.enabled = false;

        //3
        yield return new WaitForSeconds(0.0f);
        trailEffect.enabled = false;

    }


    //Use() 메인루틴 -> Swing() 서브루틴 -> Use() 메인루틴 //B43 공격로직(코루틴)
    //Use() 메인루틴 + Swing() 코루틴 (Co-Op)              //B43 공격로직(코루틴)
    IEnumerator Shot() //B44 발사구현
    {
        //1 총알발사
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50; //총알 속도

        yield return null;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Reload();
    }
}
