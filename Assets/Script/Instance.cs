using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instance : MonoBehaviour {

    public AudioSource source;
    public bool isPlayed;
    // private int destroytime;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        isPlayed = false;
        // destroytime = 5;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Audio
    public void ActiveAudio()
    {
        source.Play();
        SetisPlayed();
        Debug.Log(gameObject.name + "Audio");
    }
   

    public void SetisPlayed()
    {
        isPlayed = true;
    }
}
