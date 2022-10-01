using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpSubMachineGunController : WeaponController
{
    //���ݽ� ������ �Ѿ� �Ǵ� ȭ��
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
                //�Ѿ� ��Ÿ�� �ð� ����
                //�Ѿ� ��Ÿ���� ������ �ð��� ������
                for (int i = 0; i < spawnPosition.Count; i++)
                {
                    GameObject projectile = Instantiate(projectiles, spawnPosition[i].position, spawnPosition[i].rotation) as GameObject; //Spawns the selected projectile
                    projectile.GetComponent<Rigidbody>().AddForce(spawnPosition[i].transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
                }
                curAmmo -= oneAmmo; //�Ѿ� ����
                bulletCoolTime = 0; //�Ѿ� ��Ÿ�� �ʱ�ȭ
            }
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
