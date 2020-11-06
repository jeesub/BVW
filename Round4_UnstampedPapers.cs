using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnstampedPapers : MonoBehaviour
{
    MiniWorkManager miniWorkManager;

    private Quaternion rotation = Quaternion.Euler(0, -90, 0);

    public GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip stampDown;
    public AudioClip stampUp;
    public GameObject paper;
    public int numOfStamp;
    public TextMeshPro textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        miniWorkManager = GameObject.FindObjectOfType<MiniWorkManager>();
        numOfStamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Stamp"))
        {
            audioSource.clip = stampDown;
            audioSource.PlayOneShot(audioSource.clip);
            numOfStamp++;
            textMeshPro.text = numOfStamp.ToString();
            gameManager.GoingSad();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Stamp"))
        {
            audioSource.clip = stampUp;
            audioSource.PlayOneShot(audioSource.clip);
            float height = this.transform.localScale.y / 2 + 0.001f;
            Instantiate(paper, this.transform.position + new Vector3(0f, height, 0f), rotation);
        }
    }
}
