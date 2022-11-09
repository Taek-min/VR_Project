using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightPlayer : MonoBehaviour
{
    public GameObject MyFightPlayerGmObj;
    private void Awake()
    {
        MyFightPlayerGmObj = Instantiate(GameObject.Find("OVRPlayerControllerFactory"), gameObject.transform);
        //MyFightPlayerGmObj = GameObject.Find("OVRPlayerControllerFactory");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
