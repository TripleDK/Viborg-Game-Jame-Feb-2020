using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect1 : MonoBehaviour
{
    public bool staticBackground = true;

    public float scrollingFactor = 2f;

    public bool freezeY = false;

    Transform cameraTransform;
    Vector3 ownStartPosition;
    Vector3 offset;
    Vector3 cameraStartPosition;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        ownStartPosition = transform.position;
        offset = transform.position - Camera.main.transform.position;
        cameraStartPosition = Camera.main.transform.position;
    }


    void LateUpdate()
    {
        if (!freezeY)
        { 
        if (staticBackground)
        {
            transform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, ownStartPosition.z);
        }
        else
        {
            transform.position = new Vector3(cameraTransform.position.x / scrollingFactor, cameraTransform.position.y / scrollingFactor, ownStartPosition.z) + offset;
        }
        }
        if(freezeY)
        {
            transform.position = new Vector3(cameraTransform.position.x / scrollingFactor + offset.x, ownStartPosition.y, ownStartPosition.z);
        }
    }
}
