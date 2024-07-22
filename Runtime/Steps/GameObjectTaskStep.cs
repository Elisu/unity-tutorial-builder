using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public abstract class GameObjectTaskStep : TaskStep
{
    [HideInInspector] public IReadOnlyList<GameObjectKey> gameObjectDictionary;

    public abstract void LoadObject();
}
