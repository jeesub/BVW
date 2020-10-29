using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartingPageManager : MonoBehaviour
{
    public float slowInt = 1;
    public float medInt = 2;
    public float fastInt = 3;

    public RawImage WhiteScreen;
    public Image YesButton;
    public Image NoButton;
    public Text AskForSkip;
    public RawImage PureBlack;
    public Text WarningText;
    
    private Vector3 slowVector3;
    private Vector3 medVector3;
    private Vector3 fastVector3;

    private int i = 0;

    private bool _shouldFadeIn = false;
    private bool _shouldStart = false;
    private bool _shouldFadeOut = false;
    
    GameObject coin;
    GameObject tree;
    GameObject fence;
    GameObject farmer;
    GameObject title;
    GameObject wolves;
    GameObject sheep;

    // Start is called before the first frame update
    void Start() {
	    _shouldFadeIn = false;
	    _shouldStart = false;
	    _shouldFadeOut = false;
        slowVector3 = new Vector3(0, slowInt, 0);
        medVector3 = new Vector3(0, medInt, 0);
        fastVector3 = new Vector3(0, fastInt, 0);

        // slow
        coin = GameObject.Find("Coin");
        coin.transform.position += 100 * slowVector3;
        tree = GameObject.Find("Tree");
        tree.transform.position -= 100 * slowVector3;
        fence = GameObject.Find("Fence");
        fence.transform.position -= 100 * slowVector3;

        // med
        farmer = GameObject.Find("Farmer");
        farmer.transform.position -= 100 * medVector3;
        title = GameObject.Find("Title");
        title.transform.position += 100 * medVector3;

        // fast
        wolves = GameObject.Find("Wolves");
        wolves.transform.position += 100 * fastVector3;
        sheep = GameObject.Find("Sheep");
        sheep.transform.position -= 100 * fastVector3;

        StartCoroutine(ShouldFadeOut());
        StartCoroutine(ShouldDisapper());
        
    }

    // Update is called once per frame
    void Update()
    {
	    if (_shouldStart == false) {
		    
		    if (_shouldFadeOut) {
			    PureBlack.GetComponent<RawImage>().color = new Color(0, 0, 0,
				    Mathf.MoveTowards(PureBlack.GetComponent<RawImage>().color.a, 0f, 1.0f * Time.deltaTime));
			    WarningText.GetComponent<Text>().color = new Color(1, 1, 1,
				    Mathf.MoveTowards(WarningText.GetComponent<Text>().color.a, 0f, 1.0f * Time.deltaTime));
		    } else {
			    WarningText.GetComponent<Text>().color = new Color(1, 1, 1,
				    Mathf.MoveTowards(WarningText.GetComponent<Text>().color.a, 1f, 1f * Time.deltaTime));
		    }
	    }

	    else if (i < 100)
        {
	        PureBlack.gameObject.SetActive(false);
	        WarningText.gameObject.SetActive(false);
	        
            coin.transform.position -= slowVector3;
            tree.transform.position += slowVector3;
            fence.transform.position += slowVector3;

            farmer.transform.position += medVector3;
            title.transform.position -= medVector3;

            wolves.transform.position -= fastVector3;
            sheep.transform.position += fastVector3;
            i++;
        }

        if (_shouldFadeIn) {
	        WhiteScreen.GetComponent<RawImage>().color = new Color(1, 1, 1, Mathf.MoveTowards(WhiteScreen.GetComponent<RawImage>().color.a, 1f,  2f * Time.deltaTime));
	        YesButton.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.MoveTowards(YesButton.GetComponent<Image>().color.a, 1f,  1.5f * Time.deltaTime));
	        NoButton.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.MoveTowards(NoButton.GetComponent<Image>().color.a, 1f,  1.5f * Time.deltaTime));
	        AskForSkip.GetComponent<Text>().color = new Color(0, 0, 0, Mathf.MoveTowards(AskForSkip.GetComponent<Text>().color.a, 1f,  1.5f * Time.deltaTime));

        }
    }

    // Buttons
    public void StartButtonDown()
    {
	    WhiteScreen.gameObject.SetActive(true);
	    YesButton.gameObject.SetActive(true);
		NoButton.gameObject.SetActive(true);
		AskForSkip.gameObject.SetActive(true);
	    _shouldFadeIn = true;
	    // SceneManager.LoadScene("ComicScene");
    }

    public void SkipTutorial() {
	    SceneManager.LoadScene("SampleScene");
    }

    public void NotSkipTutorial() {
	    SceneManager.LoadScene("ComicScene");
    }

    public void ExitButtonDown()
    {
        Application.Quit();
    }

    IEnumerator ShouldFadeOut() {
	    yield return new WaitForSecondsRealtime(4.0f);
	    _shouldFadeOut = true;
    }

    IEnumerator ShouldDisapper() {
	    yield return new WaitForSecondsRealtime(4.3f);
	    _shouldStart = true;
    }
}
