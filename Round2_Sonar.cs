using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour
{
    private PitchDetect pitchDetect;
    public Vector3 scaleChange;
    private float dbMax;

    // Start is called before the first frame update
    void Start()
    {
        pitchDetect = GameObject.FindObjectOfType<PitchDetect>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(pitchDetect.dbVal);
        dbMax = pitchDetect.dbVal + 10;
        if (pitchDetect.dbVal < -15)
        {
            if (this.transform.localScale.x > 0)
            {
                this.transform.localScale -= scaleChange;
            }
        } else
        {
            if (this.transform.localScale.x < dbMax)
            {
                this.transform.localScale += scaleChange;
            }
        }
    }
}
