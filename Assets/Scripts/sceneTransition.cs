using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneTransition : MonoBehaviour
{
    public GameObject transition;
    public float transitionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        //we're subscribing to the sceneUnloaded event, which is raised whenever a scene is unloaded
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    //this will be the method we will use to transition between scenes
    public void transitionScene(string sceneName)
    {
        StartCoroutine(sceneChange(sceneName));
    }
    
    IEnumerator sceneChange(string sceneName)
    {

        //what if every levelLoader started out transparent/disabled, and so first the animation plays, then wait, then disable the animation, switch scenes, and enable the new animation??

        //so we grab crossfade as a gameobject, then

        //play animation (as in, crossFade start)
        Debug.Log("Hey start is about to trigger!");
        transition.GetComponent<Animator>().SetTrigger("Start");
        

        //wait - essentially just pads out some time for us
        yield return new WaitForSeconds(transitionTime);
        //transition.SetActive(false);
        //turn off the original scene's animator

        //transition.SetActive(true);
        //Load Scene (or unload Scene)
        //if there are currently multiple scenes active, we just want to unload the most recent scene
        if (SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
            
            
            //transition.GetComponent<Animator>().SetTrigger("StartAgain");
            //transition.SetActive(true);
        } else if (SceneManager.sceneCount == 1)
        {
            Debug.Log("in sceneChange, we're loading in a scene! It's " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            //transition.SetActive(true);
        }
    }

    //this method essentially just triggers the room scene's animation to go again after the last scene gets unloaded
    void OnSceneUnloaded(Scene scene)
    {

        //okay so couple of checks. If I'm unloading a scene, I wanna check that there's other scenes involved AND ALSO that the scene I'm unloading isn't the room scene, because that's the only place this code matters
        if(scene.buildIndex != 1 && SceneManager.sceneCount > 1)
        {
            Debug.Log("HEY! I'm about to call the trigger to make the animation work at the start of the scene");
            transition.GetComponent<Animator>().SetTrigger("StartAgain");
        }
    }
}
