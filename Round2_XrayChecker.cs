using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XrayChecker : MonoBehaviour
{
    public GameObject defaultModel;
    public GameObject xrayModel;

    // Start is called before the first frame update
    void Start()
    {
        this.defaultModel.SetActive(true);
        this.xrayModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sonar"))
        {
            this.defaultModel.SetActive(false);
            this.xrayModel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Sonar"))
        {
            this.defaultModel.SetActive(true);
            this.xrayModel.SetActive(false);
        }
    }
}
