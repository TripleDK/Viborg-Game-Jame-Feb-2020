using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBehavior : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField]
    float floatRange = 1;
    [SerializeField]
    float floatSpeed = 1;

    float startY;
    SpriteRenderer renderer;
    Collider2D coll;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        startY = transform.position.y;
        coll = GetComponent<Collider2D>();
        renderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Who dares pick up key? " + collision.gameObject.name + " tagged " + collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(PickUp());
        }
    }

    IEnumerator PickUp()
    {
        audioSource.Play();
        GameObject.FindGameObjectWithTag("LevelExit").GetComponent<ExitBehavior>().Unlock();
        coll.enabled = false;
        renderer.enabled = false;

        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    private void Update()
    {
        float newY = startY + Mathf.Cos(Time.time * floatSpeed) * floatRange;   
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
