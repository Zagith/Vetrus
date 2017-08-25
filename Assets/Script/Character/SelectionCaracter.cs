using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionCaracter : MonoBehaviour {
    GameObject[] playerObjs;
    public Transform[] players;
    CharSelect[] selectedComponents;

    public static Transform mainPlayer;
    // Use this for initialization
    void Start() {

        playerObjs = new GameObject[GameObject.FindGameObjectsWithTag("Player").Length];
        playerObjs = GameObject.FindGameObjectsWithTag("Player");
        players = new Transform[playerObjs.Length];
        selectedComponents = new CharSelect[playerObjs.Length];
        for(int i = 0; i < playerObjs.Length; i++)
        {
            selectedComponents[i] = playerObjs[i].GetComponent<CharSelect>();
            players[i] = playerObjs[i].transform;
            if(selectedComponents[i].selected)
            {
                mainPlayer = selectedComponents[i].gameObject.transform;
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeCharacter(0);
            Debug.Log("Char 2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeCharacter(1);
            Debug.Log("Char 1");
        }
    }

    void ChangeCharacter(int index)
    {
        for (int i = 0; i < selectedComponents.Length; i++)
        {
            if(i==index)
            {
                selectedComponents[i].selected = true;
                mainPlayer = selectedComponents[i].gameObject.transform;
            }
            else
            {
                selectedComponents[i].selected = false;
            }
        }
    }
}
