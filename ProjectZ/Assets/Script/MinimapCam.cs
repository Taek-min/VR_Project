using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour
{
    public Transform ovrCam;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(90f, ovrCam.transform.eulerAngles.y, -ovrCam.transform.eulerAngles.z);
    }
}
