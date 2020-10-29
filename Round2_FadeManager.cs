using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public Animator animator;

    private int sceneIndex;
    public Text loadingText;

    void Update()
    {
        if (loadingText != null && loadingText.text != "")
        {
            loadingText.color = new Color(loadingText.color.r, loadingText.color.g, loadingText.color.b, Mathf.PingPong(Time.time, 1));
        }
    }

    public void OpenNextScene()
    {
        FadeOutAndOpenNextScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeOutAndOpenNextScene(int idx)
    {
        sceneIndex = idx;
        animator.SetTrigger("FadeOut");
    }

    public void FadeComplete()
    {
        StartCoroutine(loadingStart());
    }

    IEnumerator loadingStart()
    {
       

        if (loadingText != null)
        {
            StartCoroutine(ShowLoadingText());
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneIndex);

        
        while (!async.isDone)
        {
            yield return null;
        }
    }

    IEnumerator ShowLoadingText()
    {
        yield return new WaitForSeconds(0.1f);
        loadingText.text = "Loading...";
    }
}
