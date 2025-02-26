using Elisu.TutorialBuilder;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Elisu.TutorialBuilderEditor
{
    [CustomPropertyDrawer(typeof(TaskStep), true)]
    public class TaskStepDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            // Get the full type name of the managed reference
            string fullTypeName = property.managedReferenceFullTypename;

            // Extract the class name from the full type name
            string className = string.IsNullOrEmpty(fullTypeName) ? "Unknown" : fullTypeName[(fullTypeName.LastIndexOf('.') + 1)..];

            var propertyField = new PropertyField(property, className);
            root.Add(propertyField);

            return root;


        }
    }

}
