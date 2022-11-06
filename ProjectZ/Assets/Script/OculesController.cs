using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OculesController : MonoBehaviour
{
    //����� �׽�Ʈ
    //public Text _debugText;
    //public Text _debugText2;

    //��Ʈ�ѷ� ������
    public List<Laser> laser = new List<Laser>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�޼� Ʈ���� �Է�
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            //������ �浹ü ���
            //laser[0].LaserGripUpdate(true);
            //_debugText.text = "�޼� Ʈ���� �Է�";
        }

        //�޼� Ʈ���� ����
        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            //������ �浹ü ����
            //laser[0].LaserGripUpdate(false);
        }
        //������ Ʈ���� �Է�
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            //������ �浹ü ���
            //laser[1].LaserGripUpdate(true);
            //_debugText.text = "������ Ʈ���� �Է�";
        }

        //������ Ʈ���� ����
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            //������ �浹ü ����
            //laser[1].LaserGripUpdate(false);
            //_debugText.text = "������ Ʈ���� �Է�";
        }

        //�޼� �׸� ��ư �Է�
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            //_debugText.text = "�޼� �׸� ��ư �Է�";
        }

        //������ �׸� ��ư �Է�
        if(OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            //_debugText.text = "������ �׸� ��ư �Է�";
        }

        //������ A��ư �Է�
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            //_debugText.text = "������ A��ư �Է�";
        }

        //������ B��ư �Է�
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            //_debugText.text = "������ B��ư �Է�";
        }

        //�޼� X��ư �Է�
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            //_debugText.text = "�޼� X��ư �Է�";
        }

        //�޼� Y��ư �Է�
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            //_debugText.text = "�޼� Y��ư �Է�";
        }

        //�޼� ���̽�ƽ Ŭ��
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            //_debugText.text = "�޼� ���̽�ƽ Ŭ��";

        }

        //������ ���̽�ƽ Ŭ��
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            //_debugText.text = "������ ���̽�ƽ Ŭ��";
        }

        //�޼� ���̽�ƽ �̵�
        if(OVRInput.Get(OVRInput.Touch.PrimaryThumbstick))
        {
            //Vector2 pos = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            //_debugText2.text = pos.ToString();
        }


        //������ ���̽�ƽ �̵�
        if(OVRInput.Get(OVRInput.Touch.SecondaryThumbstick))
        {
            Vector2 pos = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
            //_debugText2.text = pos.ToString();
        }

        //�޼� Ʈ���� ������
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            //_debugText2.text = trigger.ToString();
        }

        //������ Ʈ���� ������
        if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            //_debugText2.text = trigger.ToString();
        }

        //�޼� �׸���ư ������
        if(OVRInput.Get(OVRInput.Button.PrimaryHandTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
            //_debugText2.text = trigger.ToString();
        }

        //������ �׸���ư ������
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            //float trigger = OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger);
            //_debugText2.text = trigger.ToString();
        }
    }
}
