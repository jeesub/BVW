using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    public float PlayerSpeed = 10.0f;
    public float MouseSensitivity = 2.0f;
    private bool isFreezed = false;

    private Vector3 InitialPosition;
    private Quaternion InitialRotation;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = this.transform.position;
        InitialRotation = this.transform.rotation;
        animator = gameObject.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isFreezed)
        {
            // Get User Input
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            float mouseX = Input.GetAxisRaw("Mouse X");

            // Move Player by User Input
            this.transform.position += (transform.right * horizontal + transform.forward * vertical) * PlayerSpeed * Time.deltaTime;
            this.transform.Rotate(Vector3.up, mouseX * MouseSensitivity);

            if (vertical != 0 || horizontal != 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
    }

    public void ResetPlayer()
    {
        this.transform.position = InitialPosition;
        this.transform.rotation = InitialRotation;
        animator.SetBool("isWalking", false);
    }

    public void FreezePlayer()
    {
        this.isFreezed = true;
        animator.SetBool("isWalking", false);
    }

    public void UnfreezePlayer()
    {
        this.isFreezed = false;
    }
}
