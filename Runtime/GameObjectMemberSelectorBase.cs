using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public abstract class GameObjectMemberSelectorBase
    {
        public abstract void FillMembers();

        public List<string> MemberNames = new();

        public abstract void SetSelectedMember(string name);

        public abstract string GetSelectedMemberName();

        public string selectedMemberName;

    }
}
