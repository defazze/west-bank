using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ClickCountLabel : MonoBehaviour
{

    private IMessageBroker _messageBroker;

    private void OnEnable()
    {
        _messageBroker = MessageBroker.Instance;
        _messageBroker.Subscribe<int>(Events.Player.CIRCLE_CLICK, OnCircleClick);
    }

    private void OnDisable()
    {
        _messageBroker.Unsubscribe<int>(Events.Player.CIRCLE_CLICK, OnCircleClick);
    }

    public void OnCircleClick(int clickCount)
    {
        var text = GetComponent<Text>();
        text.text = clickCount.ToString();
    }
}
