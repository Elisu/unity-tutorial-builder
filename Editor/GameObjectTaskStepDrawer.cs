//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using UnityEditor;
//using UnityEngine;
//using UnityEngine.UIElements;

//[CustomPropertyDrawer(typeof(GameObjectTaskStep), true)]
//public class GameObjectTaskStepDrawer : TaskStepDrawer
//{
//    public override VisualElement CreatePropertyGUI(SerializedProperty property)
//    {
//        VisualElement container = base.CreatePropertyGUI(property);

//        // Add a button
//        Button actionButton = new(() => PerformAction(property))
//        {
//            text = "Load object/s"
//        };

//        container.Add(actionButton);

//        return container;
//    }

//    private void PerformAction(SerializedProperty property)
//    {

//        // Find the ObjectTaskStep instance
//        var objectTaskStep = property.managedReferenceValue as GameObjectTaskStep;

//        if (objectTaskStep != null)
//        {
//            Debug.Log("Object loaded");
//            objectTaskStep.LoadObject();
//        }
//        else
//        {
//            Debug.LogWarning("Failed to find ObjectTaskStep instance.");
//        }
//    }
//}
