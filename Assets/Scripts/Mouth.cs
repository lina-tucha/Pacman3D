using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouth : MonoBehaviour
{
    private bool mouthMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        mouthMovement = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mouthMovement) {
            StartCoroutine(ScaleDownAnimation(0.5f));
        }
    }

    IEnumerator ScaleDownAnimation(float time) 
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
