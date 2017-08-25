using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagazzaAnimations : MonoBehaviour {

    [SerializeField]
    IEnumerators IEnum;
    [SerializeField]
    private Animator Anim;
    
    PlayerManager player;

    void Start()
    {
        //IEnum = GetComponent<IEnumerators>();
        Anim = GetComponent<Animator>();
        player = GetComponent<PlayerManager>();
        if (IEnum == null)
        {
            Debug.Log("No Anim component found.");
        }
        
    }
    
    public void VerticalInputAnim()
    {
        Anim.SetBool("Run", true);
        Anim.SetBool("turnright", false);
        Anim.SetBool("turnleft", false);
        Anim.Play("Run");
    }
    public void VerticalInputAnimOff()
    {
        Anim.SetBool("Run", false);
        player.speed = 5;
    }

    public void MoonWalkOn()
    {
        Anim.SetBool("backrun", true);
    }
    public void MoonWalkOff()
    {
        Anim.SetBool("backrun", false);
    }

    public void TurnrightOn()
    {
        player.speed = 0;
        Anim.SetBool("turnright", true);
    }
    public void TurnrightOff()
    {
        Anim.SetBool("turnright", false);
        StartCoroutine(IEnum.Wait());
        player.speed = 15;
       
    }

    public void TurnleftOn()
    {
        player.speed = 0;
        Anim.SetBool("turnleft", true);
    }
    public void TurnleftOff()
    {
        Anim.SetBool("turnleft", false);
        StartCoroutine(IEnum.Wait());
        player.speed = 15;
    }

    public void IdleJump()
    {
        player.InAir = true;
        player.speed = 0F;
        Anim.Play("Porco");
        Anim.SetBool("Jump", true);
        StartCoroutine(IEnum.DelayRunJump());
    }
    public void RunJump()
    {
        Anim.SetBool("Jumprun", true);
        Anim.Play("jump_inPlace");
        player.InAir = true;
        player.Jump();
    }
    public void Jump()
    {
        Anim.SetBool("Jump", false);
        Anim.SetBool("Jumprun", false);
    }
}
