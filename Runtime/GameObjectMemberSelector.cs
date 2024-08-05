using Elisu.TutorialBuilder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public abstract class GameObjectMemberSelector<T> : GameObjectMemberSelectorBase
    {
        [SerializeField] public GameObjectKey targetObject;

        public Dictionary<string, T> availableMembers = new();

        public T selectedMember = default;


        public override void SetSelectedMember(string name)
        {
            selectedMember = availableMembers.GetValueOrDefault(name);
        }

        protected void SetMemberNames()
        {
            MemberNames = availableMembers.Keys.ToList();
        }
    }
}