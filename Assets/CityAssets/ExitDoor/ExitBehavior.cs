using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitBehavior : MonoBehaviour
{
    bool unlocked = false;
    [SerializeField]
    Sprite closedCoor;
    [SerializeField]
    Sprite openDoor;
    SpriteRenderer renderer;

    private void Start()
    {
        renderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();
    }
    public void Unlock()
    {
        Debug.Log("The door is now unlocked!");
        unlocked = true;
        renderer.sprite = openDoor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && unlocked == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
