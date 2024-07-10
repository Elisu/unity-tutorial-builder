//using System.Collections;
//using System.Collections.Generic;
//using UnityEditor;
//using UnityEditor.UIElements;
//using UnityEngine.UIElements;

//[CustomPropertyDrawer(typeof(List<TaskStep>), true)]
//public class StepListDrawer : PropertyDrawer
//{
//    public override VisualElement CreatePropertyGUI(SerializedProperty property)
//    {
//        VisualElement root = new();
//        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/Editor/Styles.uss"));

//        var elements = property.FindPropertyRelative("Array").GetEnumerator();

//        var listView = root.Q<ListView>();
//        listView.showAddRemoveFooter = false;
//        listView.itemsSource = (IList)elements;

//        return root;
//    }
//}
