using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float lastAttack;
    GameObject bullet;
    GameObject enemy;
    GameObject player;
    EnemyStateController stateController;
    public EnemyAttackState(GameObject enemy) { this.enemy = enemy; }

    public override void StateEnter()
    {
        stateController = enemy.GetComponent<EnemyStateController>();
        bullet = stateController.bullet;
        player = stateController.detectedPlayer;
        lastAttack = stateController.reactionTime;
        Debug.Log("Got you, ya son of a bitch!");

    }

    public override void StateExecute()
    {
        lastAttack -= Time.deltaTime;
        if (lastAttack <= 0)
        {
            Attack();
            lastAttack = stateController.timeBetweenAttacks;
        }
    }

    public override void StateExit(EnemyState newState = null)
    {
        Debug.Log("Lost him :(");
    }

    void Attack()
    {
        Debug.Log("Firing!");
        GameObject firedBullet = GameObject.Instantiate(bullet, enemy.transform.position, Quaternion.identity);
        firedBullet.transform.LookAt(player.transform.position - enemy.transform.position);
        firedBullet.transform.Rotate(-90, 0, 0);
        firedBullet.transform.eulerAngles = new Vector3(0, 0, firedBullet.transform.eulerAngles.z);
    }
}
