using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public bool hasWizardHat = false;
    public bool hasKey = false;
    public bool hasWaterBalloon = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetEquipment()
    {
        this.hasWizardHat = false;
        this.hasKey = false;
        this.hasWaterBalloon = false;
    }

    public void GetWizardHat()
    {
        this.hasWizardHat = true;
    }

    public void GetKey()
    {
        this.hasKey = true;
    }

    public void GetWaterBalloon()
    {
        this.hasWaterBalloon = true;
    }
}
