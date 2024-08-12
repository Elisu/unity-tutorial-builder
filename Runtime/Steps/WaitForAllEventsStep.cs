using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class WaitForAllEventsStep : GameObjectTaskStep
    {
        [SerializeReference] List<EventSelector> waitFor = new();

        public override void LoadObject()
        {
            for (int i = 0; i < waitFor.Count; i++)
            {
                var selector = waitFor[i];

                if (selector == null)
                {
                    selector = new EventSelector();
                    waitFor[i] = selector; // Assign back to the list
                }

                selector.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == selector.targetObject.Key)?.loadedGameObject;
                selector.FillMembers();
            }

        }

        public override async Task PerformStep(CancellationToken cancellationToken)
        {
            await WaitForAllEvents(cancellationToken);
        }

        private async Task WaitForAllEvents(CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            foreach (var selector in waitFor)
            {
                if (selector.selectedMember == null)
                {
                    Debug.LogWarning("Cannot await event that is null");
                    continue;
                }

                var tcs = new TaskCompletionSource<bool>();

                void action()
                {
                    selector.selectedMember.RemoveListener(action);
                    tcs.SetResult(true);
                }

                selector.selectedMember.AddListener(action);

                // Add the task to the list
                tasks.Add(tcs.Task);

                // Register cancellation to remove the listener and cancel the task
                cancellationToken.Register(() =>
                {
                    selector.selectedMember.RemoveListener(action);
                    tcs.TrySetCanceled();
                });
            }

            try
            {
                // Wait for all tasks to complete
                await Task.WhenAll(tasks);
            }
            finally
            {
                // Cleanup: Remove all listeners
                foreach (var selector in waitFor)
                {
                    selector.selectedMember?.RemoveAllListeners();
                }
            }

            // Check for cancellation after the wait completes
            cancellationToken.ThrowIfCancellationRequested();
        }
    }
}
