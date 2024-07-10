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

    private void OnEnable()
    {
        tutorialSection = (TutorialSectionBase)target;
        commonObjectsProperty = serializedObject.FindProperty("commonObjects");
        tasksProperty = serializedObject.FindProperty("tasks");
    }

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        // Create a PropertyField for commonObjects
        var commonObjectsField = new PropertyField(commonObjectsProperty)
        {
            label = "Common Objects"
        };
        root.Add(commonObjectsField);

        // Create a PropertyField for tasks
        var tasksField = new PropertyField(tasksProperty)
        {
            label = "Tasks"
        };
        root.Add(tasksField);

        // Register callbacks for property changes
        commonObjectsField.RegisterCallback<ChangeEvent<SerializedProperty>>(evt => OnCommonObjectsChanged(evt));
        tasksField.RegisterCallback<ChangeEvent<SerializedProperty>>(evt => OnTasksChanged(evt));

        // Initial call to ensure everything is up to date
        //UpdateCommonGameObjects();

        return root;
    }

    private void OnCommonObjectsChanged(ChangeEvent<SerializedProperty> evt)
    {
        ApplyChanges();
        UpdateCommonGameObjects();
    }

    private void OnTasksChanged(ChangeEvent<SerializedProperty> evt)
    {
        ApplyChanges();
        UpdateCommonGameObjects();
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
