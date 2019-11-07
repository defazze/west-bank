﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ClickCountLabel : MonoBehaviour
{

    public IMessageBroker messageBroker;

    private void OnEnable()
    {
        messageBroker = MessageBroker.GetInstance();
        messageBroker.Subscribe<int>(Events.Player.CIRCLE_CLICK, OnCircleClick);
    }

    private void OnDisable()
    {
        messageBroker.Unsubscribe<int>(Events.Player.CIRCLE_CLICK, OnCircleClick);
    }

    public void OnCircleClick(int clickCount)
    {
        var text = GetComponent<Text>();
        text.text = clickCount.ToString();
    }
}
