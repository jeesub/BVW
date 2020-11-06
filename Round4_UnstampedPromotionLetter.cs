using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnstampedPromotionLetter : MonoBehaviour
{
    public GameManager gameManager;
    public AudioSource audioSource;
    public AudioClip stampDown;
    public AudioClip stampUp;
    public TextMeshPro textMeshPro;
    public PromotionLetters promotionLetters;

    // Start is called before the first frame update
    void Start()
    {
        
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
            textMeshPro.text = ":-)";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Stamp"))
        {
            audioSource.clip = stampUp;
            audioSource.PlayOneShot(audioSource.clip);
            promotionLetters.SetStampedPromotionLetter();
            gameManager.RejectThePromotion();
        }
    }
}
