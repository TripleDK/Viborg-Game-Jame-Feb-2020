using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float minimumX = -10f;
    public float maximumX = 10f;
    public float speed = 0.2f;

    Vector3 startPosition;
  



    void Start()
    {
        startPosition = transform.localPosition;
    }


    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
            if(transform.localPosition.x < minimumX)
            {
            transform.localPosition = new Vector3(maximumX, startPosition.y, startPosition.z);
            }
    }
}

