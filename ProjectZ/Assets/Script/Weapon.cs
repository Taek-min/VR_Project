using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range, FireGun };  //B43 ��������
    public Type type;                   //B43 �������� ����Ÿ��
    public int damage;                  //B43 �������� ������
    public float rate;                  //B43 �������� ���ݼӵ�
    public BoxCollider meleeArea;       //B43 �������� 
    public TrailRenderer trailEffect;   //B43 ��������

    public Transform bulletPos;//B44 �߻籸��
    public GameObject bullet;//B44 �߻籸��

    public GameObject eventsys;
    public GameObject Gun;




    public float maxAmmo; //B44 ����������
    public float curAmmo; //B44 ����������
    public float oneAmmo; //B44 ����������

    public Animator anim;

    public void Awake()
    {
        UI UI = eventsys.GetComponent<UI>();
        maxAmmo += UI.AddAmmo;
        curAmmo += UI.AddAmmo;

    }



    // Start is called before the first frame update

    public void Use()                   //B43 ���ݷ���(�ڷ�ƾ)
    {
        if (type == Type.Range && curAmmo > oneAmmo) //B44 �߻籸�� 
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
    IEnumerator Swing() //B43 ���ݷ���(�ڷ�ƾ)
    {
        //1
        yield return new WaitForSeconds(0.0f);//�־��� �ð���ŭ ���
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        //2
        yield return new WaitForSeconds(0.0f);
        meleeArea.enabled = false;

        //3
        yield return new WaitForSeconds(0.0f);
        trailEffect.enabled = false;

    }


    //Use() ���η�ƾ -> Swing() �����ƾ -> Use() ���η�ƾ //B43 ���ݷ���(�ڷ�ƾ)
    //Use() ���η�ƾ + Swing() �ڷ�ƾ (Co-Op)              //B43 ���ݷ���(�ڷ�ƾ)
    IEnumerator Shot() //B44 �߻籸��
    {
        //1 �Ѿ˹߻�
        GameObject intantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = intantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletPos.forward * 50; //�Ѿ� �ӵ�

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
