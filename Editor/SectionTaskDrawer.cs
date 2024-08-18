using Elisu.TutorialBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Elisu.TutorialBuilderEditor
{
    [CustomPropertyDrawer(typeof(SectionTask), true)]
    public class SectionTaskDrawer : PropertyDrawer
    {
        private const string ExpandedKeyPrefix = "TaskEditorDrawer_Expanded_";

        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Packages/elisu.tutorial-builder/Editor/Styles.uss");

            VisualElement root = new();
            root.styleSheets.Add(styleSheet);
            root.AddToClassList("panel");

            return BuildUI(root, property);
        }

        private VisualElement BuildUI(VisualElement rootElement, SerializedProperty property)
        {
            VisualElement container = new();
            container.AddToClassList("align-vertical");

            // Caret + title
            VisualElement titleContainer = new();
            titleContainer.AddToClassList("align-horizontal");

            string propertyPath = property.propertyPath;
            string expandedKey = ExpandedKeyPrefix + propertyPath;
            bool isExpanded = SessionState.GetBool(expandedKey, false);

            Label caretLabel = new("▸");
            caretLabel.style.fontSize = 18;
            caretLabel.AddToClassList(isExpanded ? "rotate-90" : "rotate-0");

            // Content
            VisualElement contentPanel = new();
            contentPanel.AddToClassList("pl-16");
            contentPanel.AddToClassList(isExpanded ? "expanded" : "collapsed");

            SerializedProperty taskNameProperty = property.FindPropertyRelative("taskName");
            PropertyField taskNameField = new(taskNameProperty);
            contentPanel.Add(taskNameField);

            // Steps
            SerializedProperty stepsProperty = property.FindPropertyRelative("steps");

            ListView steptListView = new()
            {
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight,
                showFoldoutHeader = true,
                headerTitle = "Steps",
            };
            steptListView.BindProperty(stepsProperty);
            contentPanel.Add(steptListView);

            Button addStepButton = new(() => AddStep(stepsProperty))
            {
                text = "Add Step"
            };
            contentPanel.Add(addStepButton);

            Button removeStepButton = new(() => RemoveStep(stepsProperty, steptListView))
            {
                text = "Remove Step(s)"
            };
            contentPanel.Add(removeStepButton);

            // Set the caret label to display the taskName
            Label title = new(taskNameProperty.stringValue);
            title.BindProperty(taskNameProperty.serializedObject);
            title.AddToClassList("header");

            titleContainer.Add(caretLabel);
            titleContainer.Add(title);

            titleContainer.RegisterCallback<ClickEvent>(evt => HandleTitleClick(evt, propertyPath, caretLabel, contentPanel));

            container.Add(titleContainer);
            container.Add(contentPanel);

            rootElement.Add(container);

            return rootElement;
        }

        private void AddStep(SerializedProperty stepsProperty)
        {
            GenericMenu menu = new();

            var taskStepTypes = GetTaskStepTypes();
            foreach (var type in taskStepTypes)
            {
                menu.AddItem(new GUIContent(type.Name), false, () => AddStepOfType(type, stepsProperty));
            }

            menu.ShowAsContext();
        }

        private IEnumerable<Type> GetTaskStepTypes()
        {
            var taskStepType = typeof(TaskStep);
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(taskStepType) && !type.IsAbstract);
        }

        private void AddStepOfType(Type type, SerializedProperty stepsProperty)
        {
            stepsProperty.arraySize++;
            SerializedProperty newStepProperty = stepsProperty.GetArrayElementAtIndex(stepsProperty.arraySize - 1);

            var newStep = Activator.CreateInstance(type) as TaskStep;
            newStepProperty.managedReferenceValue = newStep;
            stepsProperty.serializedObject.ApplyModifiedProperties();

            stepsProperty.serializedObject.Update();

            var targetObject = stepsProperty.serializedObject.targetObject as TutorialSectionBase;
            targetObject.UpdateCommonGameobjects();


        }

        private void HandleTitleClick(ClickEvent evt, string propertyPath, Label caretLabel, VisualElement contentPanel)
        {
            string expandedKey = ExpandedKeyPrefix + propertyPath;
            bool isExpanded = !SessionState.GetBool(expandedKey, false);

            if (isExpanded)
            {
                caretLabel.RemoveFromClassList("rotate-0");
                caretLabel.AddToClassList("rotate-90");

                contentPanel.AddToClassList("expanded");
                contentPanel.RemoveFromClassList("collapsed");
            }
            else
            {
                caretLabel.RemoveFromClassList("rotate-90");
                caretLabel.AddToClassList("rotate-0");

                contentPanel.RemoveFromClassList("expanded");
                contentPanel.AddToClassList("collapsed");
            }

            SessionState.SetBool(expandedKey, isExpanded);
        }

        private void RemoveStep(SerializedProperty stepsProperty, ListView listView)
        {
            var selectedIndices = listView.selectedIndices.ToList();

            if (selectedIndices.Count > 0)
            {
                for (int i = selectedIndices.Count - 1; i >= 0; i--)
                {
                    stepsProperty.DeleteArrayElementAtIndex(selectedIndices[i]);
                }
            }
            else if (stepsProperty.arraySize > 0)
            {
                stepsProperty.DeleteArrayElementAtIndex(stepsProperty.arraySize - 1);
            }

            stepsProperty.serializedObject.ApplyModifiedProperties();
            stepsProperty.serializedObject.Update();

        }
    }

}
