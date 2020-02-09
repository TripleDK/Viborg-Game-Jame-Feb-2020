using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public float minimumX = -10f;
    public float maximumX = 10f;
    public float speed = 0.2f;

    public float scrollSpeed = 0.9f;
    public float scrollSpeed2 = 0.9f;

    Renderer rend;
    Vector2 newOffset;
    Vector3 startPosition;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        float offset = Time.time * scrollSpeed;
        float offset2 = Time.time * scrollSpeed2;

        rend.material.mainTextureOffset = new Vector2(offset2, -offset);
        newOffset = rend.material.mainTextureOffset;
        rend.material.SetTextureOffset("_BumpMap", newOffset);

        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.Self);
        if (transform.localPosition.x < minimumX)
        {
            transform.localPosition = new Vector3(maximumX, startPosition.y, startPosition.z);
        }
    }
}