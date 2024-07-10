using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SetActiveStep : GameObjectTaskStep
{
    [SerializeField] GameObjectKey gameObjectKey;
    [SerializeField] bool active;

    private GameObject gameObject;

    public override void LoadObject()
    {
        gameObjectKey.loadedGameObject = gameObjectDictionary.Find((item) => item.Key == gameObjectKey.Key)?.loadedGameObject;
    }

    public override async Task PerformStep()
    {
        gameObject.SetActive(active);
    }
}
