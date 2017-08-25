using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    Transform mainPlayer;
    public Vector3 velocity;
    public float time;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        mainPlayer = SelectionCaracter.mainPlayer;
        Vector3 dest = new Vector3(mainPlayer.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, dest, ref velocity, time);
	}
}
