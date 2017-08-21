using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class ActiveCutScene : MonoBehaviour {
    public GameObject Canvas;
    private Time time;
    private GameObject FPCam;
    bool Active, isPlayed;
    public AudioSource source;
    Test player;
    CameraShake CamShake;
    private void Awake()
    {
        FPCam = GameObject.Find("FPCam");
        source = GetComponent<AudioSource>();
        isPlayed = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        bool Player = other.name == "bear";
        if (Player && gameObject.name == "Fine")
        {
            Canvas.SetActive(true);
            Debug.Log("BlackCanvas");
            if (!isPlayed)
            {
                source.Play();
                isPlayed = true;
                Debug.Log("LastAudio");
            }
        }
        if (Player && gameObject.name == "SwitchCam")
        {
            Debug.Log("SwitchCam");
            FPCam.SetActive(false);
        }
    }
}
