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
    public class WaitForEventStep : GameObjectTaskStep
    {
        [SerializeReference] EventSelector waitFor = new();

        public override void LoadObject()
        {
            waitFor.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == waitFor.targetObject.Key)?.loadedGameObject;
            waitFor.FillMembers();
        }

        public override async Task PerformStep(CancellationToken cancellationToken)
        {
            await WaitForEvent(cancellationToken);
        }

        public async Task WaitForEvent(CancellationToken cancellationToken)
        {
            if (waitFor.SelectedMember == null)
            {
                Debug.LogWarning("Cannot await event that is null");
                return;
            }

            var tcs = new TaskCompletionSource<bool>();

            void action()
            {
                waitFor.SelectedMember.RemoveListener(action);
                tcs.SetResult(true);
            }

            // Register cancellation to remove the listener and cancel the TCS
            cancellationToken.Register(() =>
            {
                waitFor.SelectedMember.RemoveListener(action);
                tcs.TrySetCanceled();
            });

            waitFor.SelectedMember.AddListener(action);

            try
            {
                await tcs.Task;
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Event was cancelled");
            }
        }



    }

}