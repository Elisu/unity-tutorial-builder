using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    public class TutorialSectionBase : MonoBehaviour
    {
        public UnityEvent OnSectionCompleted;

        public List<GameObjectKey> commonObjects = new();

        public List<SectionTask> tasks = new();

        private SectionTask currentTask;

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
                currentTask = task;
                await task.BeginTask();
            }

            OnSectionCompleted?.Invoke();
        }

        public void SkipCurrentTask()
        {
            if (currentTask != null)
            {
                currentTask.SkipTask();
            }
            else
            {
                Debug.LogError("No current task to skip.");
            }
        }
    }

}
