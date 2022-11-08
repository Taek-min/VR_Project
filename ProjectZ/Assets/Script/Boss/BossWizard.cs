using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossWizard : Enemy
{
    //회오리(Tornado) - Attack2
    //레이저(Laser) - PolyOrbitalBeam - Attack1
    //애니비아W(Wall)(벽세우기) - PolySpikes - Attack4
    //화살(Arrow) - Attack3

    public GameObject tornado;
    public GameObject wall;
    public GameObject arrow;

    public Transform tornadoPos;
    public Transform wallPos;
    public Transform laserPos;
    public Transform arrowPos;

    public bool isLook;

    Vector3 lookVec; // 예측 

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        //capsuleCollider = GetComponent<CapsuleCollider>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        nav.isStopped = true;
        StartCoroutine(Think());
    }

    void Update()
    {
        if (isDead)
        {
            StopAllCoroutines();
            return;
        }

        if(isLook)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            lookVec = new Vector3(h + v, 0, v - h);
            transform.LookAt(Target.position + lookVec);
        }
    }


    IEnumerator Think()
    {
        yield return new WaitForSeconds(3f);

        int ranAction = Random.Range(0, 8);
        switch (ranAction)
        {
            case 0:
            case 1:
                StartCoroutine(ArrowShot()); // Arrow 발사 패턴
                break;
            case 2:
            case 3:
                StartCoroutine(LaserShot()); // Laser 발사 패턴 
                break;
            case 4:
            case 5:
                StartCoroutine(WallShot()); // Wall 세우기 패턴 
                break;
            case 6:
            case 7:
                StartCoroutine(TornadoShot()); // Tornado 발사 패턴 
                break;
        }
    }


    IEnumerator TornadoShot()
    {
        anim.SetTrigger("doTornado");
        yield return new WaitForSeconds(1f);

        GameObject instantTornado = Instantiate(tornado, tornadoPos.position, tornadoPos.rotation);
        BossTornado bossTornado = instantTornado.GetComponent<BossTornado>();
        bossTornado.target = Target;

        yield return new WaitForSeconds(2f);

        StartCoroutine(Think());
    }

    IEnumerator LaserShot()
    {
        isLook = false;

        anim.SetTrigger("doWall");
        yield return new WaitForSeconds(1f);

        anim.SetTrigger("doLaserMaintain");
        
        Instantiate(bullet, laserPos.position, laserPos.rotation);

        isLook = true;
        
        yield return new WaitForSeconds(2f);
        
        StartCoroutine(Think());
    }

    IEnumerator WallShot()
    {
        anim.SetTrigger("doTornado");
        yield return new WaitForSeconds(1f);

        Instantiate(wall, wallPos.position, wallPos.rotation);

        yield return new WaitForSeconds(2f);

        StartCoroutine(Think());
    }

    IEnumerator ArrowShot()
    {
        anim.SetTrigger("doWall");
        yield return new WaitForSeconds(1f);
        
        GameObject instantArrow = Instantiate(arrow, arrowPos.position, arrowPos.rotation);
        BossArrow bossArrow = instantArrow.GetComponent<BossArrow>();
        bossArrow.target = Target;

        yield return new WaitForSeconds(2f);
        
        StartCoroutine(Think());
    }


}
