using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStateController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject key;
    [HideInInspector]
    public EnemyState currentState;
    [HideInInspector]
    public EnemyState patrolBehavior;
    [HideInInspector]
    public EnemyState attackBehavior;
    [HideInInspector]
    public EnemyState chaseBehavior;
    [HideInInspector]
    public GameObject detectedPlayer;

    private AudioSource audioSource;

    public float timeBetweenPatrolTurn = 5;
    public float reactionTime = 1;
    public float timeBetweenAttacks = 3;



    public void ChangeState(EnemyState newState)
    {
        if (currentState != null)
            currentState.StateExit(newState: newState);
        currentState = newState;
        currentState.StateEnter();
    }

    public void GoToAttackState()
    {
        if (currentState != attackBehavior)
        {
            Debug.Log("Let's attack!");
            currentState = attackBehavior;
            currentState.StateEnter();
        }
    }

    public void Die()
    {
        if(key != null)
        {
            GameObject.Instantiate(key, transform.position, Quaternion.identity);
        }
        audioSource.Play(); // death sound
        Destroy(gameObject);    
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (currentState != null) currentState.StateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != null) currentState.StateExecute();

    }
}
