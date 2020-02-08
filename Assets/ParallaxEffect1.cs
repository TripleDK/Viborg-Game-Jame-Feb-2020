using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect1 : MonoBehaviour
{
    public bool staticBackground = true;

    public float scrollingFactor = 2f;

    Transform cameraTransform;
    Vector3 ownStartPosition;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        ownStartPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (staticBackground)
        {
            transform.position = new Vector3(cameraTransform.position.x, ownStartPosition.y, ownStartPosition.z);
        }
        else
        {
            transform.position = new Vector3(cameraTransform.position.x / scrollingFactor, cameraTransform.position.y / scrollingFactor, ownStartPosition.z);

        }
    }
}
