using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System;

public class InputHandler : MonoBehaviour
{
    //public GameObject voicePrefab;
    private Camera _mainCamera;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private sceneTransition transitioner;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Input Handler up and running!");
        _mainCamera = Camera.main;
    }

    void Update() 
    {
        if (Input.GetMouseButtonDown(0) && SceneManager.sceneCount == 1) // Left mouse button is clicking and also the scene is active
        {
            // Handle left mouse button click...
            Debug.Log("Left Mouse Button Clicking!!");
            //clear the warning text
            warningText.text = "";
            var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
            if (!rayHit.collider) return;
            Debug.Log("Hey! You clicked something - ");
            GameObject clickedObj = rayHit.collider.gameObject;
            Debug.Log("collider gameObject name: " + clickedObj.name);
            
            //if you're able to actually interact with the object, it'll let you enter the scene!
            if (clickedObj.GetComponent<goodnightable>() != null )
            {
                //but first it checks what's the current order
                int clickedObjOrder = clickedObj.GetComponent<goodnightable>().order;
                Debug.Log("you clicked on " + questHandler.goodnighters[clickedObjOrder]);
                Debug.Log("and the current thing to say goodnight to is " + questHandler.goodnighters[questHandler.currGoodnighter]);

                //if the clicked object is after the currentQuest in order, then it should print...
                if (questHandler.currGoodnighter < clickedObjOrder)
                {
                    warningText.text = "That's the " + questHandler.goodnighters[clickedObjOrder] + "! You need to say goodnight to the " + questHandler.goodnighters[questHandler.currGoodnighter] + " first!";
                //otherwise, if its before in order, it should it's already been clicked
                } else if (questHandler.currGoodnighter > clickedObjOrder) {
                    warningText.text = "You've already said goodnight to the " + questHandler.goodnighters[clickedObjOrder] + "!";
                //so if it's equal, it should load the proper scene!
                } else
                {
                    questText.text = "";
                    //SceneManager.LoadScene(clickedObj.GetComponent<goodnightable>().goodnightScene, LoadSceneMode.Additive);
                    transitioner.transitionScene(clickedObj.GetComponent<goodnightable>().goodnightScene);
                }
                
            } else if (clickedObj.name == "Bed" && questHandler.currGoodnighter >= questHandler.goodnighters.Length) {
                transitioner.transitionScene("Conclusion");
            } 
            else
            {
                Debug.Log("wop wop");
            }
        }
    }
}
