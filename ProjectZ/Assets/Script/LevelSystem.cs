using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
접촉한 GameObject가 ExpObject이면 경험치 상승
*/

public class LevelSystem : MonoBehaviour
{
    public int level;
    public int expMaxVal;
    public int expNowVal = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ExpObject")
        {
            ExpObject expObj = other.gameObject.GetComponent<ExpObject>();
            if (expObj != null)
                CalcExp(expObj.expPoint);
            else
                Debug.Log("ExpObject가 없습니다.");
        }
    }

    private void CalcExp(int expPoint)
    {
        int expBuf = expNowVal + expPoint;
        if (expBuf >= expMaxVal)
        {   // 경험치가 Max수치를 넘어가면 레벨업
            RiseLevel(expPoint);
        }
        else
        {   // 경험치가 Max수치에 미달이면 현재 경험치 상승
            RiseExp(expPoint);
        }
    }

    private void RiseExp(int expPoint)
    {   // 경험치가 상승하고 UI에 반영
        expNowVal += expPoint;
        Debug.Log(expNowVal);
    }
    private void RiseLevel(int expPoint)
    {   // 레벨이 상승하고 UI에 반영
        // 패시브를 1포인트 올릴 수 있음
        // 레벨에 필요한 총 경험치량이 계속 증가
        int expBuf;

        expBuf = expNowVal + expPoint - expMaxVal;
        expMaxVal += expMaxVal / 2;

        level++;
        Debug.Log(level);
    }
}
