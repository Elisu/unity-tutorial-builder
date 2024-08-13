using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    public class InvokeMethodStep : GameObjectTaskStep
    {
        [SerializeReference] MethodSelector methodSelector = new();

        public override void LoadObject()
        {
            methodSelector.targetObject.loadedGameObject = gameObjectDictionary?.FirstOrDefault((item) => item.Key == methodSelector.targetObject.Key)?.loadedGameObject;
            methodSelector.FillMembers();
        }

        public override Task PerformStep(CancellationToken cancellationToken)
        {
            methodSelector.InvokeMethod();
            return Task.CompletedTask;
        }
    }

}
