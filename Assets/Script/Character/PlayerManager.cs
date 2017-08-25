﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerManager : MonoBehaviour {

    [SerializeField]
    Animator anim;
    [SerializeField]
    float rotateSpeed = 1.5F;
    [SerializeField]
    float jumpSpeed = 10.0F;
    
     
    public float speed = 15.0F;
    public float gravity = 20.0F;
    public bool InAir = false;
    public bool air = false;

    Vector3 moveDirection = Vector3.zero;
    float counterStart = 10f;
    float theCounter;

    #region Instances
    RagazzaAnimations Anim;
    IEnumerators IEnum;
    #endregion

    void Start()
    {
        anim = GetComponent<Animator>();
        Anim = GetComponent<RagazzaAnimations>();
        IEnum = GetComponent<IEnumerators>();
        theCounter = counterStart;

    }
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 velocity = GetComponent<CharacterController>().velocity;

        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        if (Input.GetAxis("Vertical") > 0)
        {
            Anim.VerticalInputAnim();
        }
        else
        {
            Anim.VerticalInputAnimOff();
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        if (Input.GetAxis("Vertical") < 0)
        {
            Anim.MoonWalkOn();
        }
        else
        {
            Anim.MoonWalkOff();
        }


        // turn left and right


        //if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Horizontal") > 0 && Anim.GetBool("Run") == false)
        if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse X") > 0 && anim.GetBool("Run") == false)
        {
            Anim.TurnrightOn();
        }
        else if (Input.GetAxis("Mouse X") == 0)
        {
            Anim.TurnrightOff();
        }
        if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse X") < 0 && anim.GetBool("Run") == false)
        {
            Anim.TurnleftOn();
        }
        else if (Input.GetAxis("Mouse X") == 0)
        {
            Anim.TurnleftOff();
        }

        //salto da fermo 
        if (Input.GetKeyUp("space") && InAir == false && anim.GetBool("Run") == false && anim.GetBool("backrun") == false)
        {
            Anim.IdleJump();
        }
        //salto in corsa
        else if (Input.GetKeyUp("space") && InAir == false && anim.GetBool("Run") == true && anim.GetBool("backrun") == false)
        {
            Anim.RunJump();
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    //voidsalto
    public void Jump()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection.y = jumpSpeed;
        StartCoroutine(IEnum.DelayIdleJump());
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        Anim.Jump();
    }
    
}