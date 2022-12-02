using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelectorButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Material material;
    public Color color;

    //Detect current clicks on the GameObject
    public void OnPointerDown(PointerEventData eventData)
    {
        material.SetColor("_Color", color);
        Debug.Log(name + "Game Object Click in Progress");
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log(name + "No longer being clicked");
    }


    void Update()
    {
        transform.GetComponent<Image>().color = color;
    }
}
