using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniWorkManager : MonoBehaviour
{
    private Quaternion rotation = Quaternion.Euler(0, -90, 0);

    public GameObject stamp;
    public GameObject unstampedpapers;
    public GameObject stampedpapers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWork()
    {
        Instantiate(stamp, this.transform.position + new Vector3(0f, 0.1f, -0.2f), Quaternion.identity);
        Instantiate(umstampedpapers, this.transform.position + new Vector3(0f, 0f, 0f), rotation);
        Instantiate(stampedpapers, this.transform.position + new Vector3(0f, 0f, 0.3f), rotation);
    }

    public void StopWork()
    {
        GameObject[] papers = GameObject.FindGameObjectsWithTag("Paper");
        foreach(GameObject paper in papers)
        {
            GameObject.Destroy(paper);
        }
        GameObject stamp = GameObject.FindGameObjectWithTag("Stamp");
        GameObject.Destroy(stamp);
    }

    public void RemoveUnstampedPapers()
    {
        unstampedpapers.SetActive(false);
    }

}
