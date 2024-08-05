using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using Elisu.TutorialBuilder;

[CustomPropertyDrawer(typeof(GameObjectMemberSelector<>), true)]
public class GameObjectMemebrSelectorDrawer : PropertyDrawer
{
    public override VisualElement CreatePropertyGUI(SerializedProperty property)
    {
        var container = new VisualElement();

        // Add the targetObject field
        var targetObjectProperty = property.FindPropertyRelative("targetObject");
        var targetObjectSerializedField = new PropertyField(targetObjectProperty);
        container.Add(targetObjectSerializedField);

        // Add a method selection dropdown
        var target = (GameObjectMemberSelectorBase)property.boxedValue;
        var eventsDropdown = new DropdownField(target.MemberNames, 0);
        container.Add(eventsDropdown);

        // Update method list based on the selected GameObject
        eventsDropdown.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue != null)
            {
                target.SetSelectedMember(evt.newValue);
            }
            else
            {
                Debug.Log("Selected value is null");
            }
        });

        return container;
    }

    private static GameObjectMemberSelector<T> Cast<T>(object obj)
    {
        return (GameObjectMemberSelector<T>)obj;
    }
}
