using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public abstract class GameObjectTaskStep : TaskStep
    {
        [HideInInspector] public IReadOnlyList<GameObjectKey> gameObjectDictionary;

        public abstract void LoadObject();
    }

}
