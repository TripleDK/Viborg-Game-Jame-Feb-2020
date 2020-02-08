using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryEnemyBehavior : EnemyStateController
{

    void Awake()
    {
        patrolBehavior = new EnemyStationaryPatrolState(gameObject);
        attackBehavior = new EnemyAttackState(gameObject);
        currentState = patrolBehavior;
    }

}
