using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class WeaponController : MonoBehaviour
{

    //공격이 나오는 총구
    public List<Transform> spawnPosition = new List<Transform>();

    //총알의 공격속도
    public float speed = 1000;

    //
    public bool isAttack;
    public bool shootUp;

    //총
    public GameObject gun;

    //발사 지연 시간 (공격 속도)
    protected float bulletCoolTime;

    //설정할 발사 지연 시간
    public float setbulletCoolTime;

    //데미지
    //public int damage;

    //무기의 종류
    public enum Type { Grenade, FireGun, SubGun };

    public Type type;

    //최대 총탄
    public float maxAmmo;
    //현재 총탄
    public float curAmmo;
    //총탄 단위
    public float oneAmmo = 1.0f;
    //패시브로 인한 상승값
    public float psvAmmo;
    //장전 속도
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
                    Debug.Log("부모 진입");
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
                //총알 쿨타임 시간 시작
                //총알 쿨타임이 설정한 시간을 지나면

                for (int i = 0; i < spawnPosition.Count; i++)
                {
                    GameObject projectile = Instantiate(projectiles, spawnPosition[i].position, Quaternion.identity) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPosition[i].transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                    curAmmo--;
                }

            }
            //총알 쿨타임 초기화
            bulletCoolTime = 0;
        }
    }*/

    //public void GrenadeAttack()
    //{
    //    //if (curAmmo > 0)
    //    //{
    //        //파워 상태
    //        switch (powerMode)
    //        {
    //            //파워가 올라갈 때
    //            case PowerMode.Up:
    //                {
    //                    //Mathf.MoveTowards (A(power)에서 B(1.0f)까지 2.5f의 일정한 속도로 움직인다.)
    //                    power = Mathf.MoveTowards(power, 1.0f, 2.5f * Time.deltaTime);
    //                    if (power == 1.0f)
    //                    {
    //                        powerMode = PowerMode.Down;
    //                    }
    //                    break;
    //                }

    //            //파워가 내려갈 때
    //            case PowerMode.Down:
    //                {
    //                    //Mathf.MoveTowards (A(power)에서 B(1.0f)까지 2.5f의 일정한 속도로 움직인다.)
    //                    power = Mathf.MoveTowards(power, 0, 2.5f * Time.deltaTime);
    //                    if (power == 0)
    //                    {
    //                        powerMode = PowerMode.Up;
    //                    }
    //                    break;
    //                }
    //        }
    //        //파워슬라이더의 value값에 power를 대입받는다.
    //        powerSlider.value = power;
    //    //}
    //}
    //public void GrenadeShot()
    //{
    //    //수류탄 생성 -> Instantiate(생성자) => (원본, 생성 위치, 생성 시 회전 값)
    //    GameObject GranadePrfab = Instantiate(GrenadePrefab, spawnPosition[0].position, transform.rotation);
    //    //GranadePrfab.AddComponent<Rigidbody>();
    //    //수류탄을 AddForce로 쏜다. AddForce(힘을 보낼 방향, 충격 타입)
    //    GranadePrfab.GetComponent<Rigidbody>().AddForce(spawnPosition[0].transform.forward * power * 40, ForceMode.Impulse);

    //    //파워 초기화
    //    power = 0;
    //    //파워 슬라이더 초기화
    //    powerSlider.value = power;
    //}

    /*public void FireAttack()
    {
        if (curAmmo > 0 && shotUp)
        {
            isAttack = true;
            //화염방사기 파티클 실행
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
        //화염방사기 파티클 정지
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
    //                //파티클 ON
    //                if (curAmmo <= 0)
    //                {
    //                    //파티클 Off

    //                    gunState = GunState.None;
    //                }
    //                    break;
    //            }
    //    }
     
    //}
}