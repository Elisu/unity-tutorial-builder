using Elisu.TutorialBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class EventSelector : GameObjectMemberSelector<UnityEvent>
    {
        public override void FillMembers()
        {
            if (targetObject.loadedGameObject == null)
                return;

            foreach (var component in targetObject.loadedGameObject.GetComponents<MonoBehaviour>())
            {

                var members = component.GetType()
                        .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                        .Where(f => typeof(UnityEventBase).IsAssignableFrom(f.FieldType))
                        .ToDictionary(
                        f => $"{component.GetType().Name}.{f.Name}",
                        f => (UnityEvent)f.GetValue(component)
                    );

                foreach (var member in members)
                {
                    availableMembers[member.Key] = member.Value; // This will overwrite values from dict1 if the key already exists
                }

            }

            SetMemberNames();
        }

    }
}
