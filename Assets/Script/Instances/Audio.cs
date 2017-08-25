using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour {
    [SerializeField]
    AudioSource clouds;

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
}
