using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Thunder : MonoBehaviour
{
    SoundManager soundManager;
    int ThunderDuration;
    RawImage rawImage;
    public byte alpha0;
    public byte alpha1;
    Color color0;
    Color color1;



    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        rawImage = this.GetComponent<RawImage>();
        color0 = new Color32(255, 255, 255, alpha0);
        color1 = new Color32(255, 255, 255, alpha1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartThunder()
    {
        StartCoroutine(WaitForNextThunder());
    }

    IEnumerator WaitForNextThunder()
    {
        while (true)
        {
            soundManager.ThunderSound();
            // main thunder
            rawImage.color = color1;
            yield return new WaitForSeconds(0.3f);
            rawImage.color = color0;
            yield return new WaitForSeconds(0.1f);
            rawImage.color = color1;
            yield return new WaitForSeconds(0.3f);
            rawImage.color = color0;
            // following thunder
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.1f);
                rawImage.color = color1;
                yield return new WaitForSeconds(0.1f);
                rawImage.color = color0;
            }
            ThunderDuration = Random.Range(8, 16);
            yield return new WaitForSeconds(ThunderDuration);
        }
    }
}
