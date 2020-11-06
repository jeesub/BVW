using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Timeline;

public class GameManager : MonoBehaviour
{
    public Dog chichi;
    public PostProcessVolume volume;
    public GameObject IntroTimeline;
    public GameObject MainTimeline;
    public GameObject PreendTimeline;
    public GameObject EndTimeline;
    public MiniWorkManager miniWorkManager;
    public PromotionLetters promotionLetters;
    public AudioSource phoneRingAudioSource;
    public AudioSource phoneCallAudioSource;
    public AudioClip phoneRingingClip;
    public AudioClip friendsCallClip_1;
    public AudioClip friendsCallClip_2;
    public AudioClip bosssCallClip_1;
    public AudioClip bosssCallClip_2;
    public AudioClip bosssCallClip_3;

    public AudioSource[] musicElements;
    public int musicState = 0;
    public int musicDelay = 0;

    private int stage;
    private bool isOnThePhone;
    private float clipLength;

    // Start is called before the first frame update
    void Start()
    {
        stage = 0;
        isOnThePhone = true;
        StartCoroutine(WaitAndRingingStart(10));
        musicState = 1;
        setupBGM();

    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitAndRingingStart(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        isOnThePhone = false;
        PhoneRingingStart();
    }

    IEnumerator BeginBGM(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        AddMusic();

    }

        public void PhoneCall()
    {
        if (!isOnThePhone)
        {
            switch (stage)
            {
                case 0:
                    // Phone call from the friend. She is talking about Chichi.
                    phoneCallAudioSource.clip = friendsCallClip_1;
                    phoneCallAudioSource.PlayOneShot(phoneCallAudioSource.clip);
                    clipLength = phoneCallAudioSource.clip.length;
                    StartCoroutine(SomeoneStartsToTalk(clipLength));
                    // Start Coroutine -> After the phone call, Chichi Comes up.
                    StartCoroutine(ActivateTimeline(IntroTimeline, clipLength, stage));
                    break;
                case 1:
                    // Phone call from the boss. Phone flys away.
                    clipLength = 0f;
                    // Start Coroutine -> After the phone call, Chichi Comes up.
                    StartCoroutine(ActivateTimeline(MainTimeline, clipLength, stage));
                    break;
                case 2:
                    StartCoroutine(changeHappiness(-0.3f, 2f));
                    // Pick up the phone from Chichi. Phone call from the boss. Makes you reject and reject.
                    phoneCallAudioSource.clip = bosssCallClip_1;
                    phoneCallAudioSource.PlayOneShot(phoneCallAudioSource.clip);
                    clipLength = phoneCallAudioSource.clip.length;
                    StartCoroutine(SomeoneStartsToTalk(clipLength));
                    // need to wait until the boss finish his first voice line.
                    StartCoroutine(ActivateTimeline(PreendTimeline, 1f, stage));
                    StartCoroutine(ToTheClimax(clipLength + 4f));
                    break;
                case 3:
                    // Phone call from the friend.
                    phoneCallAudioSource.Stop();
                    StartCoroutine(changeHappiness(-0.3f, 2f));
                    phoneCallAudioSource.clip = friendsCallClip_2;
                    phoneCallAudioSource.PlayOneShot(phoneCallAudioSource.clip);
                    clipLength = phoneCallAudioSource.clip.length;
                    StartCoroutine(SomeoneStartsToTalk(clipLength));
                    StartCoroutine(EndTheGame(clipLength + 4f));
                    break;
                default:
                    break;
            }
            stage++;
        }
    }

    IEnumerator ActivateTimeline(GameObject timeline, float clipLength, int stg)
    {
        Debug.Log("TIMELINE STARTED: " + timeline.transform.name);
        yield return new WaitForSeconds(clipLength);
        timeline.SetActive(true);

        if (stg == 0)
        {
            StartCoroutine(changeHappiness(-0.4f, 2f));
            StartCoroutine(WaitAndRingingStart(14));
        }
    }

    IEnumerator SomeoneStartsToTalk(float len)
    {
        isOnThePhone = true;
        yield return new WaitForSeconds(len);
        isOnThePhone = false;
    }

    IEnumerator ToTheClimax(float lengthOfFirstVoiceLine)
    {
        // need to wait until the boss finish his first voice line.
        yield return new WaitForSeconds(lengthOfFirstVoiceLine);
        // The boss talks about promotion.
        phoneCallAudioSource.clip = bosssCallClip_2;
        phoneCallAudioSource.Play();
        // MiniWork Ends, Promotion Letter Comes
        yield return new WaitForSeconds(4f);
        miniWorkManager.RemoveUnstampedPapers();
        promotionLetters.SetPromotionLetter();
    }

    public void RejectThePromotion()
    {
        // You rejected his offer.
        StartCoroutine(changeHappiness(-0.5f, 2f));
        phoneCallAudioSource.Stop();
        phoneCallAudioSource.clip = bosssCallClip_3;
        phoneCallAudioSource.PlayOneShot(phoneCallAudioSource.clip);
        StartCoroutine(ActivateTimeline(EndTimeline, 0, 100));
        StartCoroutine(LastCallFromTheFriend());
    }

    IEnumerator LastCallFromTheFriend()
    {
        yield return new WaitForSeconds(14);
        chichi.enabled = true;
        PhoneRingingStart();
    }

    public void PhoneRingingStart()
    {
        phoneRingAudioSource.loop = true;
        phoneRingAudioSource.clip = phoneRingingClip;
        phoneRingAudioSource.Play();
    }

    public void PhoneRingingStop()
    {
        phoneRingAudioSource.Stop();
    }

    public void GoingSad()
    {
        if (volume.weight < 0.9f)
        {
            StartCoroutine(changeHappiness(0.1f, 1f));
        }
    }

    IEnumerator EndTheGame(float len)
    {
        yield return new WaitForSeconds(len);
        Application.Quit();
    }

    /**
     * CHANGE HAPPINESS
     * 1: See chichi ++
     * 2. Chichi pick up ++
     * 3. boss call (Reject dreams) --
     * 4. reject ++
     * 5. friend call ++
    **/
    IEnumerator changeHappiness(float amount, float timeToChange)
    {
        if (volume.weight + amount > 0 || volume.weight + amount < 1)
        {
            float timeElapsed = 0;
            float lerpDuration = timeToChange;
            float startValue = volume.weight;
            float endValue = volume.weight + amount;

            HappinessChanged((int)amount*10, endValue);

            while (timeElapsed < lerpDuration)
            {
                volume.weight = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    public void HappinessChanged(int valence, float value)
    {
        if (valence<0)
        {
            //WhenItsHappier.Invoke();
            AddMusic();
        }
        else
        {
            //WhenItsSadder.Invoke();
            LoseMusic();
        }
    }

    public void setupBGM()
    {

        foreach (AudioSource source in musicElements)
        {
            //var newSource = gameObject.AddComponent<AudioSource>();

            //newSource.clip = clip;
            source.loop = true;
            source.mute = true;
            source.Play();
        }
        StartCoroutine(BeginBGM(musicDelay));

    }


    public void AddMusic()
    {
        musicElements[musicState].mute = false;
        musicState++;
    }


    public void LoseMusic()
    {
        if (musicState == 1) return; 
        musicState--;
        musicElements[musicState].mute = true;
    }

}
