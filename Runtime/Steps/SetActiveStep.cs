using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class SetActiveStep : GameObjectTaskStep
    {
        [SerializeField] GameObjectKey gameObjectKey;
        [SerializeField] bool active;

        public override void LoadObject()
        {
            gameObjectKey.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == gameObjectKey.Key)?.loadedGameObject;
        }

        public override async Task PerformStep()
        {
            gameObjectKey.loadedGameObject.SetActive(active);
        }
    }
}