using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class WaitForOneOfEventsStep : GameObjectTaskStep
    {
        [SerializeReference] List<EventSelector> waitFor = new();

        public override void LoadObject()
        {
            foreach (EventSelector selector in waitFor)
            {
                selector.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == selector.targetObject.Key)?.loadedGameObject;
                selector.FillMembers();
            }
            
        }

        public override async Task PerformStep(CancellationToken cancellationToken)
        {
            await WaitForOneOfEvents(cancellationToken);
        }

        private async Task WaitForOneOfEvents(CancellationToken cancellationToken)
        {
            var tcsList = new TaskCompletionSource<bool>[waitFor.Count];
            var tasks = new Task[waitFor.Count];

            for (int i = 0; i < waitFor.Count; i++)
            {
                int localIndex = i; // Local copy of i

                if (waitFor[localIndex].selectedMember == null)
                {
                    Debug.LogWarning("Cannot await event that is null");
                    continue;
                }

                var tcs = new TaskCompletionSource<bool>();
                tcsList[localIndex] = tcs;
                

                void action()
                {
                    waitFor[localIndex].selectedMember.RemoveListener(action);
                    tcs.SetResult(true);

                    // Remove other listeners to prevent multiple completions
                    for (int j = 0; j < waitFor.Count; j++)
                    {
                        if (localIndex != j)
                        {
                            waitFor[j].selectedMember.RemoveListener(tcsList[j].SetCanceled);
                        }
                    }
                }

                waitFor[localIndex].selectedMember.AddListener(action);
                tasks[localIndex] = tcs.Task;

                // Add listeners to cancel other TCS when one completes
                for (int j = 0; j < waitFor.Count; j++)
                {
                    if (localIndex != j)
                    {
                        waitFor[j].selectedMember.AddListener(tcs.SetCanceled);
                    }
                }

                // Register cancellation token to cancel the TCS
                cancellationToken.Register(() =>
                {
                    tcs.TrySetCanceled();
                    waitFor[localIndex].selectedMember.RemoveListener(action);
                });
            }

            try
            {
                await Task.WhenAny(tasks);
            }
            finally
            {
                // Cleanup: Remove all listeners
                for (int i = 0; i < waitFor.Count; i++)
                {
                    waitFor[i].selectedMember.RemoveAllListeners();
                }
            }

            // Check for cancellation after the wait completes
            cancellationToken.ThrowIfCancellationRequested();
        }



    }

}
