using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


[Serializable]
public class SetActiveStep : GameObjectTaskStep
{
    [SerializeField] GameObjectKey gameObjectKey;
    [SerializeField] bool active;

    private GameObject gameObject;

    public override void LoadObject()
    {
        gameObjectKey.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == gameObjectKey.Key)?.loadedGameObject;
    }

    public override async Task PerformStep()
    {
        gameObject.SetActive(active);
    }
}