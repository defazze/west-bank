using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Прослушиватель произвольного события
/// </summary>
public interface IEventListener
{
    /// <summary>
    /// Метод, вызывемый при наступлении события
    /// </summary>
    void OnEventRaised(object args);
}
