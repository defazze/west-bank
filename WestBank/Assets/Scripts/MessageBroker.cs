using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IMessageBroker
{
    void Subscribe(string eventName, Action listener);
    void Unsubscribe(string eventName, Action listener);
    void Send(string eventName);
    void Subscribe<T>(string eventName, Action<T> listener);
    void Unsubscribe<T>(string eventName, Action<T> listener);
    void Send<T>(string eventName, T eventParam);
}

public class MessageBroker : IMessageBroker
{
    private Dictionary<string, Action> _subscribes = new Dictionary<string, Action>();
    private Dictionary<string, List<Delegate>> _subscribeWithParam = new Dictionary<string, List<Delegate>>();

    private MessageBroker()
    {

    }
    public static MessageBroker Instance { get; } = new MessageBroker();

    public void Subscribe(string eventName, Action listener)
    {
        Action thisEvent;
        if (_subscribes.TryGetValue(eventName, out thisEvent))
        {
            thisEvent += listener;
            _subscribes[eventName] = thisEvent;
        }
        else
        {
            thisEvent += listener;
            _subscribes.Add(eventName, thisEvent);
        }
    }

    public void Unsubscribe(string eventName, Action listener)
    {
        Action thisEvent;
        if (_subscribes.TryGetValue(eventName, out thisEvent))
        {
            thisEvent -= listener;
            _subscribes[eventName] = thisEvent;
        }
    }

    public void Send(string eventName)
    {
        Action thisEvent = null;
        if (_subscribes.TryGetValue(eventName, out thisEvent))
        {
            thisEvent?.Invoke();
        }
    }

    public void Subscribe<T>(string eventName, Action<T> listener)
    {
        List<Delegate> subscribers;
        if (!_subscribeWithParam.TryGetValue(eventName, out subscribers))
        {
            subscribers = new List<Delegate>();
            _subscribeWithParam.Add(eventName, subscribers);
        }
        subscribers.Add(listener);
    }

    public void Unsubscribe<T>(string eventName, Action<T> listener)
    {
        List<Delegate> subscribers;
        if (_subscribeWithParam.TryGetValue(eventName, out subscribers))
        {
            if (subscribers.Contains(listener))
                subscribers.Remove(listener);
        }
    }

    public void Send<T>(string eventName, T eventParam)
    {
        List<Delegate> subscribers;
        if (_subscribeWithParam.TryGetValue(eventName, out subscribers))
        {
            foreach (Delegate del in subscribers)
                (del as Action<T>).Invoke(eventParam);
        }
    }
    /*
        public static MessageBroker GetInstance()
        {
            string guid = UnityEditor.AssetDatabase.FindAssets("t:" + typeof(MessageBroker).Name).FirstOrDefault();
            var path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);
            var broker = UnityEditor.AssetDatabase.LoadAssetAtPath<MessageBroker>(path);

            return broker;
        }*/
}