using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationaryPatrolState : EnemyState
{
    float lastTurn;
    float lastPatrol;
    Collider2D detectCollider;
    GameObject enemy;
    Animator anim;
    EnemyStateController stateController;

    public EnemyStationaryPatrolState(GameObject enemy) { this.enemy = enemy;  anim = enemy.transform.Find("Graphics").GetComponent<Animator>(); }
    public override void StateEnter()
    {
        detectCollider = enemy.transform.Find("DetectCollider").GetComponent<Collider2D>();
        stateController = enemy.GetComponent<EnemyStateController>();
      //  Debug.Log("Looking for the enemy");
        lastTurn = stateController.timePatrolling;
    }

    public override void StateExecute()
    {
        lastTurn -= Time.deltaTime;
        lastPatrol -= Time.deltaTime;
        if (lastTurn <= 0)
        {
            TurnAround();
            lastTurn = stateController.timeBetweenPatrolTurn;
        }
        if (lastPatrol >= 0)
        {
            anim.SetFloat("Speed", stateController.movementSpeed);    
            enemy.transform.Translate(Mathf.Sign(enemy.transform.localScale.x) * stateController.movementSpeed*Time.deltaTime, 0, 0);
        }
        else
        {
            anim.SetFloat("Speed", 0);
        }
    }
    public override void StateExit(EnemyState newState = null)
    {
    }

    void TurnAround()
    {
        lastPatrol = stateController.timePatrolling;
        enemy.transform.localScale = new Vector3(-enemy.transform.localScale.x, enemy.transform.localScale.y, enemy.transform.localScale.z);
    }

}
