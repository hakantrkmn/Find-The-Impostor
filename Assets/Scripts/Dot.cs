using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Dot : MonoBehaviour, IPointerClickHandler
{
    public Image image;
    public bool isDifferent;
    
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        Debug.Log(isDifferent);
    }
}
