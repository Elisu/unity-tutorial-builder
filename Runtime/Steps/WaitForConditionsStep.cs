using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class WaitForConditionsStep : GameObjectTaskStep
    {
        [SerializeReference] List<BoolSelector> waitFor = new();

        public override void LoadObject()
        {
            for (int i = 0; i < waitFor.Count; i++)
            {
                var selector = waitFor[i];

                if (selector == null)
                {
                    selector = new BoolSelector();
                    waitFor[i] = selector; // Assign back to the list
                }

                selector.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == selector.targetObject.Key)?.loadedGameObject;
                selector.FillMembers();
            }

        }

        public override async Task PerformStep(CancellationToken cancellationToken)
        {
            var conditions = new List<Func<bool>>();

            for (int i = 0; i < waitFor.Count; i++)
            {
                int localIndex = i; // Capture the current index
                conditions.Add(() => waitFor[localIndex].selectedMember);
            }

            try
            {
                await WaitForConditions(cancellationToken, conditions.ToArray());
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Waiting for all booleans was cancelled.");
            }
        }

        protected async Task WaitForConditions(CancellationToken cancellationToken, params Func<bool>[] conditions)
        {
            while (!conditions.All(condition => condition()))
            {
                // Check if the cancellation token is triggered
                if (cancellationToken.IsCancellationRequested)
                {
                    // Throw a TaskCanceledException to propagate the cancellation
                    cancellationToken.ThrowIfCancellationRequested();
                }

                await Task.Yield(); // Yield execution until the next frame
            }
        }
    }
}

