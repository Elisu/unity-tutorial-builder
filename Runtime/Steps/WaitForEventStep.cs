using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    public class WaitForEventStep : GameObjectTaskStep
    {
        [SerializeField] TutorialEvent waitFor;

        public override void LoadObject()
        {
            waitFor.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == waitFor.targetObject.Key)?.loadedGameObject;
            waitFor.FillMethods();
        }

        public override async Task PerformStep()
        {
            //await WaitForEvent(waitFor);
        }

        private async Task WaitForEvent(UnityEvent unityEvent)
        {
            var tcs = new TaskCompletionSource<bool>();

            void action()
            {
                unityEvent.RemoveListener(action);
                tcs.SetResult(true);
            }

            unityEvent.AddListener(action);

            await tcs.Task;
        }

    }

}