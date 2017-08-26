using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {
    
    public AudioSource clouds;
    [SerializeField]
    AudioSource wind;
    [SerializeField]
    AudioSource walk;
    [SerializeField]
    AudioSource run;

    private bool isPlayed;

    // Use this for initialization
    void Start () {
        isPlayed = false;
    }
    public void ActiveCloudAudio()
    {
        clouds.Play();
        isPlayed = true;
        Debug.Log(gameObject.name + "Audio");
    }
    public void ActiveWindAudio()
    {
        wind.Play();
        isPlayed = true;
        Debug.Log(gameObject.name + "Audio");
    }
    public void ActiveWalkdAudio()
    {
        walk.Play();
        isPlayed = true;
        Debug.Log(gameObject.name + "Audio");
    }
    public void ActiveRunAudio()
    {
        run.Play();
        isPlayed = true;
        Debug.Log(gameObject.name + "Audio");
    }

}
