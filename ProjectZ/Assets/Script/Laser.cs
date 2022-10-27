using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    //레이서 상태
    public enum LaserState
    {
        On, Off
    }
    public LaserState laserState = LaserState.On;

    //

    //레이저 머터리얼
    public Material laserMat;

    //레이저 컬러
    public List<Color> laserColor = new List<Color>();

    //충돌 오브젝트
    public GameObject hitObject;
    public List<GameObject> buttonList = new List<GameObject>();

    //레이저 그립
    public GameObject laserGrip;

    //충돌한 위치
    public Vector3 hitPoint;

    //레이저 라인
    public LineRenderer laserLine;

    //레이저 위치
    //public Transform laserPos;

    public GameObject laserPoint;
    // Start is called before the first frame update
    void Start()
    {
        //레이저 색 변경
        laserMat.SetColor("_BaseColor", laserColor[0]);

        //레이저 이미션컬러 색 변경
        laserMat.SetColor("_EmissionColor", laserColor[0] * 2.0f);
    }

    
    void SetLaserPos()
    {
        HitTarget();
    }
    // Update is called once per frame
    void Update()
    {
        SetLaserPos();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonDown("Fire2"))
        {
            hitObject.GetComponent<Button>().onClick.Invoke();
        }
    }

    void HitTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            hitObject = hit.transform.gameObject;
            if (hitObject.CompareTag("UIButton"))
            {
                hitObject.GetComponent<Image>().color = laserColor[1];
            }
        }
        else if(hitObject)
        {
            for (int i = 0; i < buttonList.Count; i++)
            {
                buttonList[i].GetComponent<Image>().color = laserColor[0];
            }
            hitObject = null;
        }
    }


}