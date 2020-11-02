using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class MiniGameManager : MonoBehaviour
{
    private List<int> list = new List<int>();
    public int[] sequences = new int[4] {2, 3, 4, 5};
    public bool gettingInputs;

    private float introTime = 10f;
    private float colorTime = 1.3f;

    public AudioSource audioSource;
    public AudioClip introClip;
    public AudioClip[] audioClips = new AudioClip[4];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gettingInputs = false;
        StartMiniGame(); // for the test only! delete this line in the real game
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartMiniGame()
    {
        StartCoroutine(CoroutineManager(0));
    }

    public void PlayIntroAudio()
    {
        audioSource.clip = introClip;
        audioSource.Play();
    }
    public void PlayAudio(int col)
    {
        audioSource.clip = audioClips[col];
        audioSource.Play();
    }

    IEnumerator CoroutineManager(int seq)
    {
        if (seq == 0)
        {
            this.PlayIntroAudio();
            yield return new WaitForSeconds(introTime);
            StartCoroutine(CoroutineManager(1));
            yield return null;
        }
        else if (seq == 5)
        {
            Debug.Log("Game Finished!");
            yield return null;
        }
        else
        {
            GenerateAGame(sequences[seq - 1], list);
            foreach (int i in list)
            {
                this.PlayAudio(i);
                yield return new WaitForSeconds(colorTime);
            }
            gettingInputs = true;
            if (gettingInputs)
            {
                StartCoroutine(CheckTheInputs(seq - 1, 0));
            }
            gettingInputs = false;
            yield return null;
        }
    }

    IEnumerator CheckTheInputs(int sequence, int index)
    {
        bool finished = false;
        // Base Case
        if (sequences[sequence] <= index)
        {
            finished = true;
            StartCoroutine(CoroutineManager(sequence + 2));
            yield return null;
        }
        while (!finished) {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    string objectName = hit.transform.gameObject.name;
                    int objectInt = GetIndex(objectName);
                    if (list[index].Equals(objectInt))
                    {
                        yield return new WaitForSeconds(0.1f);
                        StartCoroutine(CheckTheInputs(sequence, index + 1));
                    } else
                    {
                        yield return new WaitForSeconds(0.1f);
                        StartCoroutine(CoroutineManager(sequence + 1));
                    }
                    finished = true;
                }
            }
            yield return null;
        }
    }

    private void GenerateAGame(int num, List<int> list)
    {
        list.Clear();
        for (int i = num; i > 0; i--)
        {
            int randomNum = Random.Range(0, 4);
            if (randomNum == 4)
            {
                randomNum = 3;
            }
            list.Add(randomNum);
        }
        foreach (int i in list)
        {
            Debug.Log(i);
        }
    }

    private int GetIndex(string name)
    {
        switch (name)
        {
            case "Red":
                return 0;
            case "Green":
                return 1;
            case "Blue":
                return 2;
            case "Yellow":
                return 3;
            default:
                return -1;
        }
    }
}
