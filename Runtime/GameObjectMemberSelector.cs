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
        [SerializeField] public GameObjectKey targetObject = new();

        public Dictionary<string, T> availableMembers = new();

        public T SelectedMember { get => availableMembers.GetValueOrDefault(selectedMemberName); }


        public override void SetSelectedMember(string name)
        {
            selectedMemberName = name;
        }

        protected void SetMemberNames()
        {
            MemberNames.Clear();
            MemberNames.AddRange(availableMembers.Keys.ToList());
        }

        public override string GetSelectedMemberName()
        {
            return selectedMemberName;
        }
    }
}
