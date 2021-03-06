﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float lastAttack;
    GameObject bullet;
    GameObject enemy;
    GameObject player;
    EnemyStateController stateController;
    Animator anim;
    public EnemyAttackState(GameObject enemy) { this.enemy = enemy; anim = enemy.transform.Find("Graphics").GetComponent<Animator>(); }

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
        anim.SetTrigger("Fire");
        firedBullet.transform.up = player.transform.position - enemy.transform.position;
        firedBullet.transform.eulerAngles = new Vector3(firedBullet.transform.eulerAngles.x, 0, firedBullet.transform.eulerAngles.z);
    }
}
