using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 initPosition = this.transform.position;
        Vector3 targetPosition = initPosition + new Vector3(0f, -0.001f, 0.3f);
        StartCoroutine(moveThePaper(initPosition, targetPosition, 0.6f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator moveThePaper(Vector3 initPosition, Vector3 targetPosition, float totalTime)
    {
        float t = 0f;
        while (t < 0.5)
        {
            t = Time.deltaTime / totalTime;
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, t);
            yield return null;
        }
    }
}
