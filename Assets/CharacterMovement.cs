using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovemen : MonoBehaviour {
    public float speed = 3.0F;
    public float rotateSpeed = 5.0F;
    public float jumpSpeed = 10.0F;
    public float gravity = 20.0F;
    public Animator Anim;
    public bool InAir = false;
    public bool air=false;
    private Vector3 moveDirection = Vector3.zero;
    void Start()
    {
        Anim = GetComponent<Animator>();

    }
    void Update()
    {


  
        CharacterController controller = GetComponent<CharacterController>();
        Vector3 velocity = GetComponent<CharacterController>().velocity;
 if(air==false) {
        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        if (Input.GetAxis("Vertical") != 0)
        {
            Anim.SetBool("Run", true);
        }
        else
        {
            Anim.SetBool("Run", false);
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
        }

        else if(air==true) {
        transform.Rotate(0, Input.GetAxis("Horizontal") * 0, 0);
        if (Input.GetAxis("Vertical") != 0)
        {
            Anim.SetBool("Run", false);
        }
        else
        {
            Anim.SetBool("Run", false);
        }
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        float curSpeed = speed * Input.GetAxis("Vertical");
        controller.SimpleMove(forward * curSpeed);
        }

        if (Input.GetKeyUp("space") && InAir == false  && Anim.GetBool("Run") == false)
        {     InAir = true;
            air=true;
            speed = 0F;
            Anim.Play("Porco");
            Anim.SetBool("Jump", true);
            StartCoroutine(waitSeconds());
           
        }

        else if (Input.GetKeyUp("space") && InAir == false  && Anim.GetBool("Run") == true)
        {    Anim.SetBool("Jumprun", true);
            Anim.Play("jump_inPlace");
            InAir = true;
            Jump();
            
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        CharacterController controller = GetComponent<CharacterController>();
        moveDirection.y = jumpSpeed;

        StartCoroutine(waitTwoSeconds());
        moveDirection.y += gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
        Anim.SetBool("Jump", false);
        Anim.SetBool("Jumprun", false);
        InAir = false;
        air=false;
    }


    IEnumerator waitTwoSeconds()
    {
        yield return new WaitForSeconds(1);
        
        speed = 10.0F;

    }


    IEnumerator waitSeconds()
    {
        yield return new WaitForSeconds(0.15F);
        speed = 5.0F;
        Jump();

    }





}