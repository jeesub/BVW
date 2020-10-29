using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audio;

    public AudioClip PlantAFence;
    public AudioClip PlantATree;
    public AudioClip CutATree;
    public AudioClip Coin;
    public AudioClip Sheep1;
    public AudioClip Wolf1;
    public AudioClip Grab;
    public AudioClip WolvesComing;
    public AudioClip ThunderClap;
    public AudioClip WaveOn;
    public AudioClip ErrorPlanting;
    public AudioClip AttackFence;

    private float Vol;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrabSound()
    {
        audio.clip = Grab;
        audio.Play();
    }

    public void PlantAFenceSound()
    {
        audio.clip = PlantAFence;
        Vol = Random.Range(0.3f, 1);
        audio.volume = Vol;
        audio.PlayOneShot(audio.clip);
    }

    public void PlantATreeSound()
    {
        audio.clip = PlantATree;
        Vol = Random.Range(0.3f, 1);
        audio.volume = Vol;
        audio.PlayOneShot(audio.clip);
    }

    public void CutATreeSound()
    {
        audio.clip = CutATree;
        Vol = Random.Range(0.3f, 1);
        audio.volume = Vol;
        audio.PlayOneShot(audio.clip);
    }

    public void CoinSound()
    {
        audio.clip = Coin;
        audio.PlayOneShot(audio.clip);
    }

    public void Sheep1Sound()
    {
        audio.clip = Sheep1;
        audio.PlayOneShot(audio.clip);
    }

    public void Wolf1Sound()
    {
        audio.clip = Wolf1;
        audio.PlayOneShot(audio.clip);
    }

    public void WolvesComingSound()
    {
        audio.clip = WolvesComing;
        audio.PlayOneShot(audio.clip);
    }

    public void ThunderSound()
    {
        audio.clip = ThunderClap;
        audio.PlayOneShot(audio.clip);
    }

    public void WaveOnSound(bool onWave)
    {
        audio.clip = WaveOn;
        if (onWave)
        {
            audio.PlayOneShot(audio.clip);
        } else
        {
            audio.Stop();
        }
        
    }

    public void ErrorPlantingSound()
    {
        audio.clip = ErrorPlanting;
        audio.PlayOneShot(audio.clip);
    }

    public void AttackFenceSound()
    {
        audio.clip = AttackFence;
        audio.PlayOneShot(audio.clip);
    }
}
