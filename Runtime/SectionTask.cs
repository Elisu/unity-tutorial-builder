using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SectionTask
{
    [SerializeField] string taskName = "";
    [SerializeReference] public List<TaskStep> steps = new();

    public async Task BeginTask()
    {
        foreach (var step in steps)
            {
                await step.PerformStep();
            }
    }

    public void AssignCommonGameObjects(List<GameObjectKey> gameObjects = null)
    {
        foreach (var gameObjectTaskStep in steps.OfType<GameObjectTaskStep>())
        {
            gameObjectTaskStep.gameObjectDictionary = gameObjects.AsReadOnly();
            gameObjectTaskStep.LoadObject();
        }
    }
}
