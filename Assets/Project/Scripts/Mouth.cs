using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    private bool mouthMovement;
    
    void Start()
    {
        mouthMovement = true;
    }

    void Update()
    {
        if (mouthMovement) {
            StartCoroutine(ScaleAnimation(0.5f));
        }
    }

    IEnumerator ScaleAnimation(float time) 
    {
        mouthMovement = false;

        float i = 0;
        float rate = 1 / time;

        Vector3 fromScale = new Vector3(0, 0.2f, 0.2f);
        Vector3 toScale = new Vector3(0.3f, 0.2f, 0.2f);

        while (i < 1) {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }

        i = 0;

        fromScale = new Vector3(0.3f, 0.2f, 0.2f);
        toScale = new Vector3(0, 0.2f, 0.2f);

        while (i < 1) {
            i += Time.deltaTime * rate;
            transform.localScale = Vector3.Lerp(fromScale, toScale, i);
            yield return 0;
        }

        mouthMovement = true;
    }
}
