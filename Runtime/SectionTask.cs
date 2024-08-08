using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class SectionTask
    {
        [SerializeField] string taskName = "";
        [SerializeReference] public List<TaskStep> steps = new();

        private CancellationTokenSource cancellationTokenSource;

        public async Task BeginTask()
        {
            cancellationTokenSource = new CancellationTokenSource();

            try
            {
                foreach (var step in steps)
                {
                    await step.PerformStep(cancellationTokenSource.Token);
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Task was cancelled");
            }
        }

        public void SkipTask()
        {
            cancellationTokenSource?.Cancel();
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

}
