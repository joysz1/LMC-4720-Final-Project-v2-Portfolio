using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


//the main goal here is simply to load the static dictionary with stuff
public class questHandler : MonoBehaviour
{
    
    //theoretically we don't even need this dictionary
    public static Dictionary<string, bool> questLog = new Dictionary<string, bool>() { };
    public static bool loadedDictionary = false;
    
    public static string[] goodnighters = { 
        "moon", 
        "cow jumping over the moon", 
        "red balloon", 
        "three bears in three chairs", 
        "two kittens", 
        "mittens", 
        "clock", 
        "socks", 
        "dollhouse", 
        "mouse", 
        "comb", 
        "brush", 
        "mush", 
        "old lady", 
        "stars", 
        "lamp"
        };


    public static int currGoodnighter = 0;
    public static int prevGoodnighter = -1;
    [SerializeField] private TextMeshProUGUI questText;
    [SerializeField] private EventSystem eventSystem;
    
    
    // Start is called before the first frame update
    void Start() {
        /*if (!loadedDictionary) {
            foreach (string goodnighter in goodnighters)
            {
                Debug.Log(questLog);
                questHandler.questLog.Add(goodnighter, false);

            };
            loadedDictionary = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //this just sets the questText to the right text
        if (prevGoodnighter != currGoodnighter)
        {
            if (currGoodnighter < goodnighters.Length)
            {
                questText.text = "say goodnight to the " + goodnighters[currGoodnighter];
                Debug.Log("say goodnight to " + goodnighters[currGoodnighter]);
            }
            else
            {
                questText.text = "head to bed!";
            }            
        }

        //this is TEST CORDE so I can easily go to certain scenes
        /*if (Input.GetKeyDown("1"))
        {
            Debug.Log("YIP!");
            SceneManager.LoadScene("Moon", LoadSceneMode.Additive);
        }*/

        if (Input.GetKeyDown("2"))
        {
            Debug.Log("Yo!, the scene under is still running!");
        }


        prevGoodnighter = currGoodnighter;

        //if there's only one loaded scene (roomScene) AND eventSystem is disabled, turn it on
        if (SceneManager.sceneCount == 1 && !eventSystem.enabled)
        {
            eventSystem.enabled = true;
            Debug.Log("roomScene is currently active");
            //otherwise, if there's now multiple scenes and event system is enabled, turn it off
        }
        else if (SceneManager.sceneCount > 1 && eventSystem.enabled)
        {
            Debug.Log("there is another scene added");
            eventSystem.enabled = false;
        }

        if (Input.GetKeyDown("3"))
        {
            questHandler.currGoodnighter = 25;
            Debug.Log("Hey we're changing currGoodnighter");
        }
    }
}
