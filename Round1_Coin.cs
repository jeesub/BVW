using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameManager gameManager;
    SoundManager soundManager;
    private int CoinIncreasedBy;
	public int CoinIncreasedPer;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        soundManager = GameObject.FindObjectOfType<SoundManager>();
        CoinIncreasedPer = 10;
        StartCoroutine(ShowMeTheMoney());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowMeTheMoney()
    {
        yield return new WaitForSecondsRealtime(CoinIncreasedPer);
        gameManager.numOfCoin += (int) gameManager.numOfSheep / 2;
        soundManager.CoinSound();
        StartCoroutine(ShowMeTheMoney());
    }
}
