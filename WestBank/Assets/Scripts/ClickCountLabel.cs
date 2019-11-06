using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class ClickCountLabel : MonoBehaviour, IEventListener
{
    private int _clickCount = 0;
    public GameEvent Event;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(object args)
    {
        _clickCount++;
        var text = GetComponent<Text>();
        text.text = _clickCount.ToString();
    }
}
