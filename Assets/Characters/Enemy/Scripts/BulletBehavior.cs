using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletBehavior : MonoBehaviour
{

    [SerializeField]
    float speed = 10f;
    [SerializeField]
    float damage = 50;

    float lifeTime = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * speed * Time.deltaTime,Space.World);
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            WerewolfStateController playerController = collision.gameObject.GetComponent<WerewolfStateController>();
            playerController.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
