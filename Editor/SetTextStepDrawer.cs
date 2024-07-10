using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomPropertyDrawer(typeof(SetTextStep))]
public class SetTextStepDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        // Create a root element
        VisualElement root = new();

        // Get the full type name of the managed reference
        string fullTypeName = property.managedReferenceFullTypename;

        // Extract the class name from the full type name
        string className = string.IsNullOrEmpty(fullTypeName) ? "Unknown" : fullTypeName.Substring(fullTypeName.LastIndexOf(' ') + 1);

        Label caretLabel = new(className);
        root.Add(caretLabel);

        // Create the instance dropdown
        var instanceProperty = property.FindPropertyRelative("instance");

        var types = GetTextAndAudioManagerTypes();

        var typeNames = new List<string>();
        foreach (var type in types)
        {
            typeNames.Add(type.Name);
        }

        string currentTypeName = instanceProperty.objectReferenceValue != null ? instanceProperty.objectReferenceValue.GetType().Name : typeNames[0];
        var dropdown = new DropdownField("Options", typeNames, currentTypeName);

        dropdown.RegisterValueChangedCallback(evt =>
        {
            string selectedTypeName = evt.newValue;
            Type selectedType = types.FirstOrDefault(type => type.Name == selectedTypeName);
            if (selectedType != null)
            {
                var instance = GetTextAndAudioManagerInstance(selectedType);

                if (instance != null)
                {
                    SetInstance(instanceProperty, instance);
                }
                
            }
        });

        root.Add(dropdown);

        // Create the key property field
        var keyProperty = property.FindPropertyRelative("key");
        var keyField = new PropertyField(keyProperty);
        root.Add(keyField);

        return root;
    }

    private IEnumerable<Type> GetTextAndAudioManagerTypes()
    {
        var baseType = typeof(TextAndAudioManagerBase);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(baseType) && !type.IsAbstract);
    }

    private TextAndAudioManagerBase GetTextAndAudioManagerInstance(Type type)
    {
        var property = type.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
        return property?.GetValue(null) as TextAndAudioManagerBase;
    }

    private void SetInstance(SerializedProperty instanceProperty, TextAndAudioManagerBase instance)
    {
        if (instanceProperty != null)
        {
            instanceProperty.objectReferenceValue = instance;
            instanceProperty.serializedObject.ApplyModifiedProperties();
        }
        else
        {
            Debug.LogError("instanceProperty is null.");
        }
    }
}
