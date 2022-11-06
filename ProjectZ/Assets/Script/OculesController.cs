using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OculesController : MonoBehaviour
{
    //디버깅 테스트
    //public Text _debugText;
    //public Text _debugText2;

    //콘트롤러 레이저
    public List<Laser> laser = new List<Laser>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //왼손 트리거 입력
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            //레이저 충돌체 잡기
            //laser[0].LaserGripUpdate(true);
            //_debugText.text = "왼손 트리거 입력";
        }

        //왼손 트리거 떼기
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            //레이저 충돌체 놓기
            //laser[0].LaserGripUpdate(false);
        }
        //오른손 트리거 입력
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //레이저 충돌체 잡기
            //laser[1].LaserGripUpdate(true);
            //_debugText.text = "오른손 트리거 입력";
        }

        //오른손 트리거 떼기
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            //레이저 충돌체 놓기
            //laser[1].LaserGripUpdate(false);
            //_debugText.text = "오른손 트리거 입력";
        }

        //왼손 그립 버튼 입력
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            //_debugText.text = "왼손 그립 버튼 입력";
        }

        //오른손 그립 버튼 입력
        if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            //_debugText.text = "오른손 그립 버튼 입력";
        }

        //오른손 A버튼 입력
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //_debugText.text = "오른손 A버튼 입력";
        }

        //오른손 B버튼 입력
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            //_debugText.text = "오른손 B버튼 입력";
        }

        //왼손 X버튼 입력
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            //_debugText.text = "왼손 X버튼 입력";
        }

        //왼손 Y버튼 입력
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            //_debugText.text = "왼손 Y버튼 입력";
        }

        //왼손 조이스틱 클릭
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            //_debugText.text = "왼손 조이스틱 클릭";

        }

        //오른손 조이스틱 클릭
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            //_debugText.text = "오른손 조이스틱 클릭";
        }

        //왼손 조이스틱 이동
        if(OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            //Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            //_debugText2.text = pos.ToString();
        }


        //오른손 조이스틱 이동
        if(OVRInput.Get(OVRInput.Touch.SecondaryThumbstick))
        {
            Vector2 pos = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            //_debugText2.text = pos.ToString();
        }

        //왼손 트리거 누르기
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            //_debugText2.text = trigger.ToString();
        }

        //오른손 트리거 누르기
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            //_debugText2.text = trigger.ToString();
        }

        //왼손 그립버튼 누르기
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            //_debugText2.text = trigger.ToString();
        }

        //오른손 그립버튼 누르기
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            //_debugText2.text = trigger.ToString();
        }
    }
}
