// Editor/CustomUnityEventDrawer.cs
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System;
using UnityEditor.Events;
using UnityEditor.UIElements;

[CustomPropertyDrawer(typeof(TutorialEvent))]
public class TutorialEventDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        // Add the targetObject field
        var targetObjectProperty = property.FindPropertyRelative("targetObject");
        var targetObjectSerializedField = new PropertyField(targetObjectProperty);
        container.Add(targetObjectSerializedField);

        var targetObjectProperty2 = property.FindPropertyRelative("unityEvent");
        var targetObjectSerializedField2 = new PropertyField(targetObjectProperty2);
        container.Add(targetObjectSerializedField2);

        // Add a method selection dropdown
        var target = (TutorialEvent)property.boxedValue;
        var eventsDropdown = new DropdownField(target.availableEvents, 0);
        container.Add(eventsDropdown);

        // Update method list based on the selected GameObject
        eventsDropdown.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue != null)
            {
                foreach (var component in target.targetObject.loadedGameObject.GetComponents<MonoBehaviour>())
                {

                    target.unityEvent = (UnityEvent)component.GetType()
                            .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                            .Where(f => typeof(UnityEventBase).IsAssignableFrom(f.FieldType) && $"{component.GetType().Name}.{f.Name}" == evt.newValue)
                            .Select(f => f.GetValue(component) as UnityEventBase).First();
                }
            }
            else
            {
                Debug.Log("Selected value is null");
            }
        });

        //// Add a listener to the UnityEvent based on the selected method
        //methodDropdown.RegisterValueChangedCallback(evt =>
        //{
        //    if (evt.newValue != null && !string.IsNullOrEmpty(evt.newValue))
        //    {
        //        var targetObject = targetObjectField.value as GameObject;
        //        if (targetObject != null)
        //        {
        //            var parts = evt.newValue.Split('.');
        //            if (parts.Length == 2)
        //            {
        //                var componentType = parts[0];
        //                var methodName = parts[1];
        //                var component = targetObject.GetComponents<MonoBehaviour>().FirstOrDefault(c => c.GetType().Name == componentType);
        //                if (component != null)
        //                {
        //                    var method = component.GetType().GetMethod(methodName);
        //                    if (method != null)
        //                    {
        //                        UnityEventTools.AddPersistentListener(
        //                            (UnityEvent)fieldInfo.GetValue(property.serializedObject.targetObject),
        //                            Delegate.CreateDelegate(typeof(UnityAction<string>), component, method) as UnityAction<string>);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //});

        return container;
    }
}
