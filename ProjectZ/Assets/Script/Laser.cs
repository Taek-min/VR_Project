using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public string[] layerMaskList;
    public int layerMask;
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

        layerMask = LayerMask.GetMask(layerMaskList);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        HitTarget();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonDown("Fire2"))
        {
            hitObject.GetComponent<Button>().onClick.Invoke();
        }

    }

    void HitTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out hit, layerMask))
        {
            //UIButton tag인지 확인한다.
            if(hit.Equals(hitObject))
            //기존에 hover중이던 버튼과 다르면 이미지를 원래대로 되돌린다.
            //tag를 통해 무슨 버튼인지 확인한다. 일반, 패시브
            //버튼의 이미지를 바꾼다.

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