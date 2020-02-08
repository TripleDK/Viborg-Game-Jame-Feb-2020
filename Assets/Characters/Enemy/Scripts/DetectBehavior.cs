using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectBehavior : MonoBehaviour
{

    [SerializeField]
    float detectRange;

    private Collider2D detectCollider;
    private Collider2D enemyCollider;
    private EnemyStateController stateController;

    void Start()
    {
        stateController = transform.parent.GetComponent<EnemyStateController>();
        detectCollider = GetComponent<Collider2D>();
        enemyCollider = transform.parent.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(detectCollider, enemyCollider);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            RaycastHit2D detectCheck = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, detectRange);
            Debug.Log("Detection hits " + collider.gameObject.name);
            if (collider.gameObject.tag == "Player")
            {
                stateController.detectedPlayer = collider.gameObject;
                stateController.GoToAttackState();
            }
        }
    }
}
