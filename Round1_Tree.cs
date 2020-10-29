using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    HPDefenders hpDefender;
    SoundManager soundManager;

    public int StartHp;
    public int IncreasingHealth;

    public int GrowingTime;

    public int MaxStage;
    public int CurStage;

    public GameObject SaplingModel;
    public GameObject GrownTreeModel;

    private Vector3 MoveVector3 = new Vector3(0, 0.3f, 0);
    private Vector3 ScaleVector3 = new Vector3(10, 17.5f, 10);

    // Start is called before the first frame update
    void Start()
    {
        hpDefender = this.GetComponent<HPDefenders>();
        hpDefender.health = 2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // plant tree
    public void StartGrowing()
    {
        IncreasingHealth = 1;
        GrowingTime = 2;
        MaxStage = 2;
        CurStage = 0;
        // Move Object -y direction
        if (CurStage == 0)
        {
            SaplingModel.SetActive(true);
            GrownTreeModel.SetActive(false);
        }

        soundManager = GameObject.FindObjectOfType<SoundManager>();
        soundManager.PlantATreeSound();
        StartCoroutine(CoroutineTreeGrowing());
    }

    IEnumerator CoroutineTreeGrowing()
    {
        while (CurStage < MaxStage)
        {
            yield return new WaitForSeconds(GrowingTime);
            CurStage++;
            hpDefender.health += IncreasingHealth;
            
            // Modify Position and Scale
            if (CurStage == 1)
            {
                SaplingModel.SetActive(false);
                GrownTreeModel.SetActive(true);
                this.transform.localScale -= ScaleVector3;
                this.transform.position -= MoveVector3;
            }
            if (CurStage == 2)
            {
                this.transform.localScale += ScaleVector3;
                this.transform.position += MoveVector3;
            }
        }
    }
}
