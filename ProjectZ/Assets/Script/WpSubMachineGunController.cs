using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpSubMachineGunController : WeaponController
{
    //공격시 나오는 총알 또는 화염
    public GameObject projectiles;

    protected override void Start()
    {
        base.Start();
    }
    
    public override void Attack(bool shootDown, bool shootUp)
    {
        if (bulletCoolTime >= setbulletCoolTime)
        {
            if (curAmmo >= oneAmmo)
            {
                //총알 쿨타임 시간 시작
                //총알 쿨타임이 설정한 시간을 지나면
                for (int i = 0; i < spawnPosition.Count; i++)
                {
                    GameObject projectile = Instantiate(projectiles, spawnPosition[i].position, spawnPosition[i].rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPosition[i].transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                }
                curAmmo -= oneAmmo; //총알 감소
                bulletCoolTime = 0; //총알 쿨타임 초기화
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
