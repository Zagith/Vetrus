using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirRagazzaAnimations : MonoBehaviour {
    [SerializeField]
    Animator ragazza;
	// Use this for initialization
	void Start () {
        ragazza = GetComponent<Animator>();
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
