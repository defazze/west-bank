using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Circle : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private IMessageBroker _messageBroker;

    private int _clickCount = 0;

    void OnEnable()
    {
        _messageBroker = MessageBroker.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _clickCount++;
        _messageBroker.Send<int>(Events.Player.CIRCLE_CLICK, _clickCount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
