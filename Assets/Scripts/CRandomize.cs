using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CRandomize : MonoBehaviour
{
    public Text rand;
    public float timeElapsed = 0;
    public float timeSpan = 1.2f;
    // Start is called before the first frame update
    void Start()
    {
        rand.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rand.enabled)
        {
            if (timeElapsed < 10)
            {
                timeElapsed += Time.deltaTime;

            }
            else
            {
                rand.enabled = true;
                GameObject.Find("Player").GetComponent<CPlayerController>().randomize();
            }
        }
        else if (rand.enabled)
        {
            if (timeElapsed < 11.2f)
            {
                timeElapsed += Time.deltaTime;
            }
            else
            {
                rand.enabled = false;
                timeElapsed = 0;
            }
        }
    }
}
