using Elisu.TutorialBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class TutorialEvent
{
    [SerializeField] public GameObjectKey targetObject;
    [SerializeField] public List<string> availableEvents = new ();

    public UnityEvent unityEvent = null;

    public void FillMethods()
    {
        if (targetObject.loadedGameObject == null)
            return;
        
        foreach (var component in targetObject.loadedGameObject.GetComponents<MonoBehaviour>())
        {

            availableEvents = component.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(f => typeof(UnityEventBase).IsAssignableFrom(f.FieldType))
                    .Select(m => $"{component.GetType().Name}.{m.Name}")
                    .ToList();

        }
    }

}
