using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class MethodSelector : GameObjectMemberSelector<MethodInfo>
    {
        public override void FillMembers()
        {
            if (targetObject.loadedGameObject == null)
                return;

            foreach (var component in targetObject.loadedGameObject.GetComponents<MonoBehaviour>())
            {

                var methods = component.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .Where(m => !m.IsSpecialName && m.GetParameters().Length == 0)
                    .ToDictionary(
                        m => $"{component.GetType().Name}.{m.Name}",
                        m => m
                    );

                foreach (var method in methods)
                {
                    availableMembers[method.Key] = method.Value; // This will overwrite values from dict1 if the key already exists
                }

            }

            SetMemberNames();
        }

        public void InvokeMethod()
        {
            selectedMember.Invoke(targetObject.loadedGameObject, null);
        }
    }
}
