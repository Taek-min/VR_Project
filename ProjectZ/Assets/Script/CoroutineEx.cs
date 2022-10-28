using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineEx : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartCoroutineA()
    {
        if (coA != null)
        {
            StopCoroutine(coA);            
        }

        coA = StartCoroutine(AAA());
    }


    Coroutine coA;

    IEnumerator AAA()
    {
        yield return null;

        while (true)
        {
            yield return null;

            break;
        }
    }
}
