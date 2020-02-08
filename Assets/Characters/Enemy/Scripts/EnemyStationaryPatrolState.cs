using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationaryPatrolState : EnemyState
{
     float lastTurn;
    Collider2D detectCollider;
    GameObject enemy;
    EnemyStateController stateController;

    public EnemyStationaryPatrolState(GameObject enemy) { this.enemy = enemy; }
    public override void StateEnter()
    {
        detectCollider = enemy.transform.Find("DetectCollider").GetComponent<Collider2D>();
        stateController = enemy.GetComponent<EnemyStateController>();
        Debug.Log("Looking for the enemy");
    }

    public override void StateExecute()
    {
        lastTurn -= Time.deltaTime;
        if (lastTurn <= 0)
        {
            TurnAround();
            lastTurn = stateController.timeBetweenPatrolTurn;
        }
    }
    public override void StateExit(EnemyState newState = null)
    {
    }

    void TurnAround()
    {
        enemy.transform.localScale = new Vector3(-enemy.transform.localScale.x, enemy.transform.localScale.y, enemy.transform.localScale.z);
    }

}
