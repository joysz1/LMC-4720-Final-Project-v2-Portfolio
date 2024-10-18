using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//Gets us into the main game
public class loader : MonoBehaviour
{

    public String NextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("YIP!");
            SceneManager.LoadScene(NextScene);
        }
    }
}
