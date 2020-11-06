using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPositionHelper : MonoBehaviour
{
    private Vector3 InitialPosition;
    private Quaternion InitialRotation;
    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = this.transform.position;
        InitialRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPosition()
    {
        this.transform.position = InitialPosition;
        this.transform.rotation = InitialRotation;
    }
}
