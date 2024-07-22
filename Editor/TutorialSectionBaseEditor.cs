using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(TutorialSectionBase))]
public class TutorialSectionBaseEditor : Editor
{
    private SerializedProperty commonObjectsProperty;
    private SerializedProperty tasksProperty;
    private TutorialSectionBase tutorialSection;
    private List<SerializedProperty> commonObjectSerializedObjects = new();


    private void OnEnable()
    {
        tutorialSection = (TutorialSectionBase)target;
        commonObjectsProperty = serializedObject.FindProperty("commonObjects");
        tasksProperty = serializedObject.FindProperty("tasks");
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        var commonObjectsListView = new ListView
        {
            itemsSource = tutorialSection.commonObjects,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            makeItem = () => new PropertyField(),
            showAddRemoveFooter = true,
            showFoldoutHeader = true,
            headerTitle = "Common Objects",
            bindItem = (element, i) =>
            {
                var propertyField = (PropertyField)element;

                // Needs to be updated to register the newly added element, outherwise throws i out of bounds
                commonObjectsProperty.serializedObject.ApplyModifiedProperties();
                commonObjectsProperty.serializedObject.Update();
                var itemProperty = commonObjectsProperty.GetArrayElementAtIndex(i);

                if (itemProperty != null)
                {
                    propertyField.BindProperty(itemProperty);
                    propertyField.RegisterCallback<SerializedPropertyChangeEvent>(e => UpdateCommonGameObjects());
                }
                else
                {
                    Debug.LogWarning("Item property is null");
                }
            }
        };

        root.Add(commonObjectsListView);

        // Create a PropertyField for tasks
        //var tasksListView = new ListView
        //{
        //    itemsSource = tutorialSection.tasks,
        //    virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
        //    makeItem = () => new PropertyField(),
        //    showAddRemoveFooter = true,
        //    showFoldoutHeader = true,
        //    headerTitle = "Tasks",
        //    bindItem = (element, i) =>
        //    {
        //        var taskField = (PropertyField)element;

        //        // Needs to be updated to register the newly added element, outherwise throws i out of bounds
        //        tasksProperty.serializedObject.ApplyModifiedProperties();
        //        tasksProperty.serializedObject.Update();
        //        var task = tasksProperty.GetArrayElementAtIndex(i);

        //        if (task != null)
        //        {
        //            taskField.BindProperty(tasksProperty);
        //            taskField.RegisterCallback<SerializedPropertyChangeEvent>(e => UpdateCommonGameObjects());
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Item property is null");
        //        }
        //    }
        //};

        var tasksListView = new ListView() {
            itemsSource = tutorialSection.tasks,
            virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
            showAddRemoveFooter = true,
            showFoldoutHeader = true,
            headerTitle = "Tasks",
            makeItem = () => new PropertyField(),
            bindItem = (element, i) =>
            {
                var taskField = (PropertyField)element;

                // Needs to be updated to register the newly added element, outherwise throws i out of bounds
                tasksProperty.serializedObject.ApplyModifiedProperties();
                tasksProperty.serializedObject.Update();
                var task = tasksProperty.GetArrayElementAtIndex(i);

                if (task != null)
                {
                    taskField.BindProperty(task);
                    taskField.RegisterCallback<SerializedPropertyChangeEvent>(e => UpdateCommonGameObjects());
                }
                else
                {
                    Debug.LogWarning("Item property is null");
                }
            }
        };

        //tasksListView.RegisterValueChangeCallback(evt => UpdateCommonGameObjects());
        root.Add(tasksListView);

        // Initial call to ensure everything is up to date
        UpdateCommonGameObjects();

        return root;
    }

    private void UpdateCommonGameObjects()
    {
        ApplyChanges();
        tutorialSection.UpdateCommonGameobjects();
        EditorUtility.SetDirty(tutorialSection);
    }

    private void ApplyChanges()
    {
        serializedObject.ApplyModifiedProperties();
        serializedObject.Update();
    }
}