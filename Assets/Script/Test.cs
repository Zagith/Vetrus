using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private CharacterController controller;
    public Animator ragazza;
    

    public float speed = 1.0F;
    public float jumpSpeed = 1.0F;
    public float gravity = 1.0F;
    public float airFriction = 0.5f;


    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        ragazza = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        moveDirection = new Vector3(0, 0, 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= speed;
        if (controller.isGrounded)
        {
            return;
        }
        else 
        {
            moveDirection *= airFriction;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
     void FixedUpdate()
    {
        RightLeft();
    }
    void RightLeft()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed, Space.Self); //LEFT
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed, Space.Self); //RIGHT
        }
    }

    public void AnimationEnter()
    {
        Debug.Log("Animation");
        ragazza.SetBool("Protegge", true);
        ragazza.SetBool("Schianto", true);
    }

    public void AnimationExit()
    {
        ragazza.SetBool("Schianto", false);
        ragazza.SetBool("Caduta", true);
    }

}
