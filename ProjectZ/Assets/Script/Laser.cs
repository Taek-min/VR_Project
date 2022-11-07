using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    public string[] layerMaskList;
    public int layerMask;
    //���̼� ����
    public enum LaserState
    {
        On, Off
    }
    public LaserState laserState = LaserState.On;

    //

    //������ ���͸���
    public Material laserMat;

    //������ �÷�
    public List<Color> laserColor = new List<Color>();

    //�浹 ������Ʈ
    public GameObject hitObject;
    public List<GameObject> buttonList = new List<GameObject>();

    //������ �׸�
    public GameObject laserGrip;

    //�浹�� ��ġ
    public Vector3 hitPoint;

    //������ ����
    public LineRenderer laserLine;

    //������ ��ġ
    //public Transform laserPos;

    public GameObject laserPoint;
    // Start is called before the first frame update
    void Start()
    {
        //������ �� ����
        laserMat.SetColor("_BaseColor", laserColor[0]);
        //������ �̹̼��÷� �� ����
        laserMat.SetColor("_EmissionColor", laserColor[0] * 2.0f);

        layerMask = LayerMask.GetMask(layerMaskList);
    }

    // Update is called once per frame
    private void Update()
    {
        HitTarget();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || Input.GetButtonDown("Fire2"))
        {
            if(hitObject != null)
                hitObject.GetComponent<Button>().onClick.Invoke();
        }

    }

    void HitTarget()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        if (!Physics.Raycast(ray, out hit, layerMask))
        {
            if(hitObject != null)
            {
                hitObject.GetComponent<UIButton>().Off_HoverBtn();
                hitObject = null;
            }
            return;
        }

        GameObject nowHitObject = hit.transform.gameObject;
        if (nowHitObject.CompareTag("UIButton"))
        {
            nowHitObject.GetComponent<UIButton>().On_HoverBtn();
            if (hitObject != null && hitObject != nowHitObject)
            {
                hitObject.GetComponent<UIButton>().Off_HoverBtn();
            }
            if (hitObject == null || hitObject != nowHitObject)
            {
                hitObject = nowHitObject;
            }
        }
    }


}