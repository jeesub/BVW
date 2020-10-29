using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Text HowToPlay;
    public AudioClip WinClip;
    public AudioClip FailClip;
    private GameObject player;
    private WizardHat wizardHat;
    private Alien alien;
    private Key key;
    private Gate gate;
    private Fire fire;
    private WaterBalloon waterBalloon;
    private Hammer hammer;
    private Stone stone;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        wizardHat = GameObject.FindObjectOfType<WizardHat>();        
        alien = GameObject.FindObjectOfType<Alien>();
        key = GameObject.FindObjectOfType<Key>();
        gate = GameObject.FindObjectOfType<Gate>();
        waterBalloon = GameObject.FindObjectOfType<WaterBalloon>();
        fire = GameObject.FindObjectOfType<Fire>();
        hammer = GameObject.FindObjectOfType<Hammer>();
        stone = GameObject.FindObjectOfType<Stone>();

        audioSource = GetComponent<AudioSource>();
        Init();
    }

    void Init()
    {
        player.GetComponent<PlayerMovement>().FreezePlayer();
        player.GetComponent<PlayerEquipment>().ResetEquipment();
        wizardHat.Reset();
        alien.Reset();
        key.Reset();
        gate.Reset();
        waterBalloon.Reset();
        fire.Reset();
        hammer.Reset();
        stone.Reset();

        HowToPlay = GameObject.FindObjectOfType<Text>();
        HowToPlay.text = "Move: WASD \n Action: Space Bar \n View: Mouse \n \n Watch out! \n Danger is everywhere";
        StartCoroutine(ShowHowTo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowHowTo()
    {
        yield return new WaitForSeconds(3);
        HowToPlay.text = "Go!";
        player.GetComponent<PlayerMovement>().UnfreezePlayer();
        yield return new WaitForSecondsRealtime(1);
        HowToPlay.text = "";
    }

    public void Fail()
    {
        player.GetComponent<PlayerMovement>().ResetPlayer();
        audioSource.clip = FailClip;
        audioSource.Play();
        Init();
    }

    public void Win()
    {
        audioSource.clip = WinClip;
        audioSource.Play();
    }
}
