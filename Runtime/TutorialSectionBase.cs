using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    public class TutorialSectionBase : MonoBehaviour
    {
        public UnityEvent OnSectionCompleted;

        public UnityEvent OnSectionCompleted2;

        public List<GameObjectKey> commonObjects = new();

        public List<SectionTask> tasks = new();

        public void UpdateCommonGameobjects()
        {
            foreach (var task in tasks)
            {
                task.AssignCommonGameObjects(commonObjects);
            }
        }

        public async void StartAsync()
        {
            foreach (var task in tasks)
            {
                await task.BeginTask();
            }
        }
    }

}
