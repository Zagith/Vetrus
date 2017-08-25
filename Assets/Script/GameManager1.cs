using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour {

    public AudioSource source;
    public bool isPlayed;
    // private int destroytime;
    public void Awake()
    {
        source = GetComponent<AudioSource>();
        isPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // Audio
    public void ActiveAudio()
    {
        if (isPlayed == true)
        {
            source.Play();
            Debug.Log(gameObject.name + "Audio");
        }
    }
}
