//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//using UnityEditor;
//using UnityEditor.UIElements;
//using UnityEngine.UIElements;

//[CustomPropertyDrawer(typeof(GameobjectWrapper))]
//public class GameobjectWrapperDrawer : PropertyDrawer
//{
//    public override VisualElement CreatePropertyGUI(SerializedProperty property)
//    {
//        // Create a container for the property drawer
//        var container = new VisualElement();

//        // Create fields for the description and object reference
//        var descriptionProperty = property.FindPropertyRelative("description");
//        var objectReferenceProperty = property.FindPropertyRelative("objectReference");

//        var descriptionField = new PropertyField(descriptionProperty);
//        var objectReferenceField = new PropertyField(objectReferenceProperty);

//        // Add fields to the container
//        container.Add(descriptionField);
//        container.Add(objectReferenceField);

//        return container;
//    }
//}

