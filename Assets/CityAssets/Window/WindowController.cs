using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowController : Interactable
{

    [SerializeField]
    GameObject roomLight;

    [SerializeField]
    Sprite openWindows;
    [SerializeField]
    Sprite closedWindows;

    SpriteRenderer renderer;

    private void Start()
    {
        renderer = transform.Find("Graphics").GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        renderer.sprite = roomLight.activeSelf ? openWindows : closedWindows;
        roomLight.SetActive(!roomLight.activeSelf);
    }

}
