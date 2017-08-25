using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    private CharacterController controller;
    
    bool isTrigger;
    #region [+] Clouds Vectors
    private GameObject[] clouds, clouds1, clouds2, clouds3, clouds4;
    private int destroytime;
    #endregion
    [SerializeField]
    Audio source;
    GameManager1 Gm;
    [SerializeField]
    AirRagazzaAnimations ragazza;
    private void Awake()
    {
        #region [+] Instantiations
        GameManager1 Gm = GetComponent<GameManager1>();
        #endregion
        controller = GetComponent<CharacterController>();
        destroytime = 5;
    }
    // Use this for initialization
    void Start()
    {
        #region [+] Clouds Variables
        clouds = GameObject.FindGameObjectsWithTag("Clouds"); clouds1 = GameObject.FindGameObjectsWithTag("Clouds1"); clouds2 = GameObject.FindGameObjectsWithTag("Clouds2"); clouds3 = GameObject.FindGameObjectsWithTag("Clouds3"); clouds4 = GameObject.FindGameObjectsWithTag("Clouds4");
        #endregion
    }
    private void OnTriggerEnter(Collider col)
    {
        bool Player = col.gameObject.tag == "Player";
        #region [+] Clouds

        #region [+] Cloud
        if (Player && this.gameObject.name == "Cloud")
        {

            foreach (GameObject clouds in clouds)
            {
                clouds.GetComponent<Rigidbody>().isKinematic = false;
                clouds.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("Ciao");
                Destroy(clouds, destroytime);
            }
            ragazza.AnimationEnter();


            source.ActiveCloudAudio();
        }
        #endregion

        #region [+] Cloud1

        else if (Player && this.gameObject.name == "Cloud1")
        {
            foreach (GameObject clouds1 in clouds1)
            {
                clouds1.GetComponent<Rigidbody>().isKinematic = false;
                clouds1.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("Ciao1");
                Destroy(clouds1, destroytime);
            }
            ragazza.AnimationEnter();
            source.ActiveCloudAudio();
        }
        #endregion

        #region [+] Cloud2

        else if (Player && this.gameObject.name == "Cloud2")
        {
            foreach (GameObject clouds2 in clouds2)
            {
                clouds2.GetComponent<Rigidbody>().isKinematic = false;
                clouds2.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("Ciao2");
                Destroy(clouds2, destroytime);
            }
            ragazza.AnimationEnter();
            source.ActiveCloudAudio();
        }
        #endregion

        #region [+] Cloud3

        else if (Player && this.gameObject.name == "Cloud3")
        {
            foreach (GameObject clouds3 in clouds3)
            {
                clouds3.GetComponent<Rigidbody>().isKinematic = false;
                clouds3.GetComponent<BoxCollider>().isTrigger = false;
                Debug.Log("Ciao3");
                Destroy(clouds3, destroytime);
            }
            ragazza.AnimationEnter();
            source.ActiveCloudAudio();
        }

        #endregion

        #region [+] Cloud4

        else if (Player && this.gameObject.name == "Cloud4")
        {
            foreach (GameObject clouds4 in clouds4)
            {
                clouds4.GetComponent<Rigidbody>().isKinematic = false;
                clouds4.GetComponent<BoxCollider>().isTrigger = false;
                Destroy(clouds4, destroytime);
                Debug.Log("Ciao4");
            }
            ragazza.AnimationEnter();
            source.ActiveCloudAudio();
        }

        #endregion

        #endregion
    }
    void OnTriggerExit(Collider col)
    {
        bool Player = col.gameObject.tag == "Player";
        if (Player && this.gameObject.name == "Cloud")
        {
                ragazza.AnimationExit();
        }
        else if (Player && this.gameObject.name == "Cloud1")
        {
                ragazza.AnimationExit();
        }
        else if (Player && this.gameObject.name == "Cloud2")
        {
                ragazza.AnimationExit();
        }
        else if (Player && this.gameObject.name == "Cloud3")
        {
                ragazza.AnimationExit();
        }
        else if (Player && this.gameObject.name == "Cloud4")
        {
                ragazza.AnimationExit();
        }
    }
}
