using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    public class WaitForEventStep : GameObjectTaskStep
    {
        [SerializeField] EventSelector waitFor;

        public override void LoadObject()
        {
            waitFor.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == waitFor.targetObject.Key)?.loadedGameObject;
            waitFor.FillMembers();
        }

        public override async Task PerformStep()
        {
            await waitFor.WaitForEvent();
        }

        

    }

}