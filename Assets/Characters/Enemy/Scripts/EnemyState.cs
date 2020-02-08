using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class EnemyState : ScriptableObject
{
    public abstract void StateEnter();
    public abstract void StateExecute();
    public abstract void StateExit(EnemyState newState = null);


}
