using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour
{

    //������ ������ �ѱ�
    public List<Transform> spawnPosition = new List<Transform>();

    //�Ѿ��� ���ݼӵ�
    public float speed = 1000;

    //
    public bool isAttack;
    public bool shootUp;

    //��
    public GameObject gun;

    //�߻� ���� �ð� (���� �ӵ�)
    protected float bulletCoolTime;

    //������ �߻� ���� �ð�
    public float setbulletCoolTime;

    //������
    //public int damage;

    //������ ����
    public enum Type { Grenade, FireGun, SubGun };

    public Type type;

    //�ִ� ��ź
    public float maxAmmo;
    //���� ��ź
    public float curAmmo;
    //��ź ����
    public float oneAmmo = 1.0f;
    //�нú�� ���� ��°�
    public float psvAmmo;
    //���� �ӵ�
    public float reloadSpeed = 1.0f;

    public virtual void Reload()
    {
        //if(isAttack) return;

        if (curAmmo < maxAmmo)
            curAmmo += Time.deltaTime * reloadSpeed;
        else
            curAmmo = maxAmmo;

        bulletCoolTime += Time.deltaTime;
    }


    protected virtual void Start()
    {
        bulletCoolTime = 0;
        //if (type == Type.FireGun) fire.Stop();
        //setbulletCoolTime = bulletCoolTime;
    }

    public virtual void Attack(bool shootDown, bool shootUp)
    {
        switch(type)
        {
            case Type.FireGun:
                {
                    //if (shotUp)
                    //{
                    //    FireAttackStop();
                    //    this.shotUp = true;
                    //}
                    //else FireAttack();
                    break;
                }
            case Type.Grenade:
                {
                    Debug.Log("�θ� ����");
                    //if(shotUp) GrenadeShot();
                    //else        GrenadeAttack();
                    break;
                }
            case Type.SubGun:
                {
                    //(WpSubMachineGunController)this.Attack(shotDown, shotUp);
                    //SubGunAttack();
                    break;
                }
        }
    }
    public void UpdateAmmo(float ammoVal)
    {
        maxAmmo += psvAmmo * ammoVal;
    }
/*    public void SubGunAttack()
    {
        if (bulletCoolTime >= setbulletCoolTime)
        {
            if (curAmmo >= 2)
            {
                //�Ѿ� ��Ÿ�� �ð� ����
                //�Ѿ� ��Ÿ���� ������ �ð��� ������

                for (int i = 0; i < spawnPosition.Count; i++)
                {
                    GameObject projectile = Instantiate(projectiles, spawnPosition[i].position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPosition[i].transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                    curAmmo--;
                }

            }
            //�Ѿ� ��Ÿ�� �ʱ�ȭ
            bulletCoolTime = 0;
        }
    }*/

    //public void GrenadeAttack()
    //{
    //    //if (curAmmo > 0)
    //    //{
    //        //�Ŀ� ����
    //        switch (powerMode)
    //        {
    //            //�Ŀ��� �ö� ��
    //            case PowerMode.Up:
    //                {
    //                    //Mathf.MoveTowards (A(power)���� B(1.0f)���� 2.5f�� ������ �ӵ��� �����δ�.)
    //                    power = Mathf.MoveTowards(power, 1.0f, 2.5f * Time.deltaTime);
    //                    if (power == 1.0f)
    //                    {
    //                        powerMode = PowerMode.Down;
    //                    }
    //                    break;
    //                }

    //            //�Ŀ��� ������ ��
    //            case PowerMode.Down:
    //                {
    //                    //Mathf.MoveTowards (A(power)���� B(1.0f)���� 2.5f�� ������ �ӵ��� �����δ�.)
    //                    power = Mathf.MoveTowards(power, 0, 2.5f * Time.deltaTime);
    //                    if (power == 0)
    //                    {
    //                        powerMode = PowerMode.Up;
    //                    }
    //                    break;
    //                }
    //        }
    //        //�Ŀ������̴��� value���� power�� ���Թ޴´�.
    //        powerSlider.value = power;
    //    //}
    //}
    //public void GrenadeShot()
    //{
    //    //����ź ���� -> Instantiate(������) => (����, ���� ��ġ, ���� �� ȸ�� ��)
    //    GameObject GranadePrfab = Instantiate(GrenadePrefab, spawnPosition[0].position, transform.rotation);
    //    //GranadePrfab.AddComponent<Rigidbody>();
    //    //����ź�� AddForce�� ���. AddForce(���� ���� ����, ��� Ÿ��)
    //    GranadePrfab.GetComponent<Rigidbody>().AddForce(spawnPosition[0].transform.forward * power * 40, ForceMode.Impulse);

    //    //�Ŀ� �ʱ�ȭ
    //    power = 0;
    //    //�Ŀ� �����̴� �ʱ�ȭ
    //    powerSlider.value = power;
    //}

    /*public void FireAttack()
    {
        if (curAmmo > 0 && shotUp)
        {
            isAttack = true;
            //ȭ������ ��ƼŬ ����
            fire.Play();
            curAmmo -= Time.deltaTime * oneAmmo;
        }
        else if(curAmmo <= 0)
        {
            shotUp = false;
            FireAttackStop();
        }
    }*/

/*    public void FireAttackStop()
    {
        isAttack = false;
        //ȭ������ ��ƼŬ ����
        fire.Stop();
    }*/

    protected virtual void Update()
    {
        Reload();


        ////
        //if(Input.GetMouseButtonDown(0))
        //{
        //    gunState = GunState.Firing;
        //}
    }

    //public void FireS()
    //{
    //    switch(gunState)
    //    {
    //        case GunState.Firing:
    //            {
    //                //��ƼŬ ON
    //                if (curAmmo <= 0)
    //                {
    //                    //��ƼŬ Off

    //                    gunState = GunState.None;
    //                }
    //                    break;
    //            }
    //    }
     
    //}
}