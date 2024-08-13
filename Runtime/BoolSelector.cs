using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class BoolSelector : GameObjectMemberSelector<bool>
    {
        public override void FillMembers()
        {
            if (targetObject.loadedGameObject == null)
                return;

            foreach (var component in targetObject.loadedGameObject.GetComponents<MonoBehaviour>())
            {

                // Get all boolean fields
                var boolFields = component.GetType()
                    .GetFields(BindingFlags.Instance | BindingFlags.Public)
                    .Where(f => f.FieldType == typeof(bool))
                    .ToDictionary(
                        f => $"{component.GetType().Name}.{f.Name}",
                        f => (bool)f.GetValue(component)
                    );

                // Get all boolean properties
                var boolProperties = component.GetType()
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(p => p.PropertyType == typeof(bool) && p.CanRead)
                    .ToDictionary(
                        p => $"{component.GetType().Name}.{p.Name}",
                        p => (bool)p.GetValue(component)
                    );

                // Combine fields and properties into the availableMembers dictionary
                foreach (var field in boolFields)
                {
                    availableMembers[field.Key] = field.Value;
                }

                foreach (var property in boolProperties)
                {
                    availableMembers[property.Key] = property.Value;
                }

            }

            SetMemberNames();
        }

    }
}
