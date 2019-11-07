using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
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
        var slider = GetComponent<Slider>();
        slider.value = ((float)clickCount) / 100;
    }
}
