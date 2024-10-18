using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeImage : MonoBehaviour
{
    public void initImage(GameObject image)
    {
        Instantiate(image, this.transform);
    }

    public void destroyImage(GameObject image)
    {
        Destroy(image);
    }
}
