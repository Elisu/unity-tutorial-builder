//using UnityEditor;
//using UnityEditor.Graphs;
//using UnityEditor.UIElements;
//using UnityEngine;
//using UnityEngine.UIElements;

//[CustomPropertyDrawer(typeof(GameObjectKey))]
//public class GameObjectKeyDrawer : PropertyDrawer
//{
//    public override VisualElement CreatePropertyGUI(SerializedProperty property)
//    {
//        // Create a container for the property drawer
//        var container = new VisualElement();

//        var keyProperty = property.FindPropertyRelative("gameObjectKey");
//        var keyField = new PropertyField(property.FindPropertyRelative("gameObjectKey"));


//        var gameObjectProperty = property.FindPropertyRelative("loadedGameObject");
//        var gameObjectField = new PropertyField(property.FindPropertyRelative("loadedGameObject"));

//        // Create a button to find the GameObject based on the key
//        var findButton = new Button(() =>
//        {
//            // Find the GameObject based on the key (implement your logic here)
//            var foundGameObject = FindGameObjectByKey(keyProperty.stringValue);
//            gameObjectProperty.objectReferenceValue = foundGameObject;
//            property.serializedObject.ApplyModifiedProperties();
//        })
//        {
//            text = "Find GameObject"
//        };

//        // Add fields and button to the container
//        container.Add(keyField);
//        container.Add(gameObjectField);
//        container.Add(findButton);

//        return container;
//    }

//    private GameObject FindGameObjectByKey(string key)
//    {
//        // Implement your logic to find the GameObject based on the key
//        // For example, you might have a manager or database that stores these references
//        // This is a placeholder implementation
//        Debug.Log("DD");
//        return null;
//    }
//}
