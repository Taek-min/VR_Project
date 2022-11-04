using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
접촉한 GameObject가 ExpObject이면 경험치 상승
*/

public class ExpSystem : MonoBehaviour
{
    // 싱글톤
    public static ExpSystem instance;

    public int level;           // 현재 레벨
    public int expMaxVal;       // 최대 경험치값
    public int expNowVal = 0;   // 현재 경험치값
    public GameObject expFactory;

    public Text[] testText;

    public const int Level = 0;
    public const int Exp = 1;
    public const int Log = 2;

    private void Awake()
    {
        // 싱글톤 생성
        if (instance == null)
            instance = this;
    }

    public bool CalcExp(int expPoint)
    {
        int expBuf = expNowVal + expPoint;
        if (expBuf < expMaxVal)
        {   // 경험치가 Max수치에 미달이면 현재 경험치 상승
            RiseExp(expPoint);
            return true;
        }
        else
        {   // 경험치가 Max수치를 넘어가면 레벨업
            RiseLevel(expPoint);
            return false;
        }
    }

    private void RiseExp(int expPoint)
    {   // 경험치가 상승하고 UI에 반영
        expNowVal += expPoint;
        Debug.Log(expNowVal);
        testText[Exp].text = expNowVal.ToString() + ", " + expMaxVal.ToString();
    }
    private void RiseLevel(int expPoint)
    {   // 레벨이 상승하고 UI에 반영
        // 패시브를 1포인트 올릴 수 있음
        // 레벨에 필요한 총 경험치량이 계속 증가
        int expBuf;

        expBuf = expNowVal + expPoint - expMaxVal;
        expMaxVal += expMaxVal / 2;
        expNowVal = 0;
        level++;

        RiseExp(expBuf);

        Debug.Log(level);
        testText[Level].text = level.ToString();

    }

    public void CreateExp(int expPoint, Vector3 enemyPos)
    {
        //1. ExpObject를 생성한다.
        GameObject exp = Instantiate(expFactory);
        //2. enemyVec에 위치시킨다.
        exp.transform.position = new Vector3(enemyPos.x, 2, enemyPos.z);
        //3. expPoint를 입력한다.
        exp.GetComponent<ExpObject>().expPoint = expPoint;
    }
}
