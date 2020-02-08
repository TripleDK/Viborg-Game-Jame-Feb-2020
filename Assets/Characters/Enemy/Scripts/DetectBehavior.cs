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
    private bool seeingEnemy = false;

    void Start()
    {
        stateController = transform.parent.GetComponent<EnemyStateController>();
        detectCollider = GetComponent<Collider2D>();
        enemyCollider = transform.parent.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(detectCollider, enemyCollider);
    }

    void SpottedEnemy()
    {
        //Hey, Tony!
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            RaycastHit2D detectCheck = Physics2D.Raycast(transform.position, collider.transform.position - transform.position, detectRange);
            if (collider.gameObject.tag == "Player")
            {

                WerewolfStateController wolfController = collider.gameObject.GetComponent<WerewolfStateController>();
                if (wolfController.wolfForm)
                {

                    stateController.detectedPlayer = collider.gameObject;

                    stateController.GoToAttackState();
                }
                else
                {
                    if (seeingEnemy == false)
                    {
                        seeingEnemy = true;
                        SpottedEnemy();
                    }
                }
            }
            else
            {
                seeingEnemy = false;
            }
        }
        else
        {
            seeingEnemy = false;
        }

    }

}
