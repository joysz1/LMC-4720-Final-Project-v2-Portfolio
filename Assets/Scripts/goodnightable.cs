using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;


//this will just hold the scene that inputHandler runs based on the collision
public class goodnightable : MonoBehaviour
{
    [SerializeField] public string goodnightScene;
    [SerializeField] public int order;
    // Start is called before the first frame update

    public GameObject childObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Color startcolor;
    void OnMouseEnter()
    {
        childObject.GetComponent<Renderer>().enabled = true;
    }
    void OnMouseExit()
    {
        childObject.gameObject.GetComponent<Renderer>().enabled = false;
    }
}
