using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyLightBehavior : MonoBehaviour
{

    [SerializeField]
    GameObject lightToActivate;

    private int obstaclesOnTop = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " entered!");
        obstaclesOnTop++;
        lightToActivate.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + " exited!");
        obstaclesOnTop--;
        if (obstaclesOnTop == 0)
        {
            lightToActivate.SetActive(true);
        }
    }

}
