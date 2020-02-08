using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    [SerializeField]
    float floatRange = 1;
    [SerializeField]
    float floatSpeed = 1;

    float startY;

    private void Start()
    {
        startY = transform.position.y;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Who dares pick up key? " + collision.gameObject.name + " tagged " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            ///ESBEN: HER SAMLER VI NØGLEN OP
            GameObject.FindGameObjectWithTag("LevelExit").GetComponent<ExitBehavior>().Unlock();
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        float newY = startY + Mathf.Cos(Time.time * floatSpeed) * floatRange;   
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
