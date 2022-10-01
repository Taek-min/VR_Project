using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WpFireGunController : WeaponController
{
    public enum FireParticleState
    {
        Off, On
    }
    public FireParticleState fireParticleState = FireParticleState.Off;

    //ȭ�������� �� ��ƼŬ;
    public List<ParticleSystem> fire = new List<ParticleSystem>();

    //ȭ������ �ݶ��̴� �ִϸ��̼�
    //public Animation FireCollider;

    public GameObject rangeOfAttackFire;

    public override void Reload()
    {
        if(!isAttack)
        {
            base.Reload();
        }
        else
        {
            bulletCoolTime += Time.deltaTime;
        }
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //fire.Stop();
        //Debug.Log("Start Stop");
    }

    protected void OnEnable()
    {
        Debug.Log("OnEnable Stop");
        for(int i = 0; i < fire.Count; i++)
        {
            fire[i].Stop();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void Attack(bool shootDown, bool shootUp)
    {
        if (shootDown && !shootUp)
        {
            FireAttackStart(); 
        }
        else if (shootUp)
        {
            this.shootUp = true;
            FireAttackStop();
        }
    }

    public void FireAttackStart()
    {
        if(curAmmo > oneAmmo && shootUp && bulletCoolTime >= setbulletCoolTime)
        {
            isAttack = true;
            switch(fireParticleState)
            {
                case FireParticleState.Off:
                    {
                        FireParticle(true);
                        fireParticleState = FireParticleState.On;
                        StartCoroutine("ROAFire");
                        break;
                    }
            }
            //curAmmo -= Time.deltaTime * oneAmmo;
        }
        else if(curAmmo <= setbulletCoolTime)
        {
            shootUp = false;
            FireAttackStop();
        }
    }

    public void FireAttackStop()
    {
        isAttack = false;
        //ȭ������ ��ƼŬ ����
        
        switch(fireParticleState)
        {
            case FireParticleState.On:
                {
                    FireParticle(false);
                    fireParticleState = FireParticleState.Off;
                    break;
                }
        }
    }

    public void FireParticle(bool b)
    {
        switch(b)
        {
            case true:
                {
                    for (int i = 0; i < fire.Count; i++)
                    {
                        fire[i].Play();
                    }
                    break;
                }
            case false:
                {
                    for (int i = 0; i < fire.Count; i++)
                    {
                        fire[i].Stop();
                    }
                    break;
                }
        } 
    }

    IEnumerator ROAFire()
    {
        while(fireParticleState == FireParticleState.On)
        {
            //�Ѿ� ��Ÿ���� ������ �ð��� ������
            for (int i = 0; i < spawnPosition.Count; i++)
            {
                GameObject roa = Instantiate(rangeOfAttackFire, spawnPosition[i].position, spawnPosition[i].rotation) as GameObject; //Spawns the selected projectile
                roa.GetComponent<Rigidbody>().AddForce(spawnPosition[i].transform.forward * speed); //Set the speed of the projectile by applying force to the rigidbody
            }
            curAmmo -= setbulletCoolTime; //�Ѿ� ����
            bulletCoolTime = 0; //�Ѿ� ��Ÿ�� �ʱ�ȭ
            yield return new WaitForSeconds(setbulletCoolTime);
        }
    }
}
