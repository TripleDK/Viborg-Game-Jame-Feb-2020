using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WerewolfStateController : MonoBehaviour
{

    public bool wolfForm = false;

    [Header("Human stats")]
    [SerializeField]
    private float humanSpeed;
    [SerializeField]
    private float humanJump;
    [SerializeField]
    private float humanStrength;

    [Header("Wolf stats")]
    [SerializeField]
    private float wolfSpeed;
    [SerializeField]
    private float wolfJump;
    [SerializeField]
    private float wolfStrength;

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Hello");

    }
 
}
