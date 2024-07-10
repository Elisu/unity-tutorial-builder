using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameObjectTaskStep : TaskStep
{
    [HideInInspector] public List<GameObjectKey> gameObjectDictionary;

    public abstract void LoadObject();
}
