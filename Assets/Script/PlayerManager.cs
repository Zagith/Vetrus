using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float speed = 15.0F;
    public float rotateSpeed = 1.5F;
    public float jumpSpeed = 10.0F;
    public float gravity = 20.0F;
    public Animator Anim;
    public bool InAir = false;
    public bool air = false;
    private Vector3 moveDirection = Vector3.zero;
    private float counterStart = 10f;
    private float theCounter;
    void Start()
    {
        Anim = GetComponent<Animator>();
        theCounter = counterStart;

    }
    void Update()
    {



        CharacterController controller = GetComponent<CharacterController>();
        Vector3 velocity = GetComponent<CharacterController>().velocity;

        transform.Rotate(0, Input.GetAxis("Mouse X") * rotateSpeed, 0);
        if (Input.GetAxis("Vertical") > 0)
        {
            Anim.SetBool("Run", true);
            Anim.SetBool("turnright", false);
            Anim.SetBool("turnleft", false);
            Anim.Play("Run");

        }
        else
        {
            Anim.SetBool("Run", false);
            speed = 5;

        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);

        if (Input.GetAxis("Vertical") < 0)
        {
            speed = 5;
            Anim.SetBool("backrun", true);
        }
        else
        {
            Anim.SetBool("backrun", false);

        }


        // turn left and right


        //if (Input.GetAxis("Horizontal") != 0 && Input.GetAxis("Horizontal") > 0 && Anim.GetBool("Run") == false)
        if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse X") > 0 && Anim.GetBool("Run") == false)
        {
            speed = 0;
            Anim.SetBool("turnright", true);
        }
        else if (Input.GetAxis("Mouse X") == 0)
        {
            Anim.SetBool("turnright", false);
            StartCoroutine(Wait());
            speed = 15;
        }
        if (Input.GetAxis("Mouse X") != 0 && Input.GetAxis("Mouse X") < 0 && Anim.GetBool("Run") == false)
        {
            speed = 0;
            Anim.SetBool("turnleft", true);
        }
        else if (Input.GetAxis("Mouse X") == 0)
        {
            Anim.SetBool("turnleft", false);
            StartCoroutine(Wait());
            speed = 15;
        }

        //salto da fermo 
        if (Input.GetKeyUp("space") && InAir == false && Anim.GetBool("Run") == false && Anim.GetBool("backrun") == false)
        {
            InAir = true;
            speed = 0F;
            Anim.Play("Porco");
            Anim.SetBool("Jump", true);
            StartCoroutine(waitSeconds());
        }
        //salto in corsa
        else if (Input.GetKeyUp("space") && InAir == false && Anim.GetBool("Run") == true && Anim.GetBool("backrun") == false)
        {
            Anim.SetBool("Jumprun", true);
            Anim.Play("jump_inPlace");
            InAir = true;
            Jump();
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
    //voidsalto
    void Jump()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection.y = jumpSpeed;
        StartCoroutine(waitTwoSeconds());
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        Anim.SetBool("Jump", false);
        Anim.SetBool("Jumprun", false);
    }
    //ritardo salto da fermo
    IEnumerator waitTwoSeconds()
    {
        yield return new WaitForSeconds(1.2F);
        InAir = false;
        speed = 15.0F;
    }
    //ritardo salto in corsa
    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(0.15F);
        Jump();
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1F);
        speed = 15.0F;

    }
}
