﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameEvent onClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        onClick.Raise();
        //Debug.Log("Clicked!!");
    }

    public void OnPointerEnter(PointerEventData eventData)
	{
		//Debug.Log("Enter!!");
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		//Debug.Log("Exit!!");
	}
}
