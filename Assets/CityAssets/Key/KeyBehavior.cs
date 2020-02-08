using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Who dares pick up key? " + collision.gameObject.name + " tagged " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelExit").GetComponent<ExitBehavior>().Unlock();
            Destroy(gameObject);
        }
    }
}
