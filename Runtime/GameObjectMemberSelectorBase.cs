using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    public abstract class GameObjectMemberSelectorBase
    {
        public abstract void FillMembers();

        public List<string> MemberNames = new();

        public abstract void SetSelectedMember(string name);

    }
}
