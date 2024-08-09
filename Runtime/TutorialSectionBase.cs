using System;
using System.Collections.Generic;
using System.Threading;
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

        private CancellationTokenSource sectionCancellationTokenSource;

        public void UpdateCommonGameobjects()
        {
            foreach (var task in tasks)
            {
                task.AssignCommonGameObjects(commonObjects);
            }
        }

        public async void StartAsync()
        {
            sectionCancellationTokenSource = new CancellationTokenSource();

            try
            {
                foreach (var task in tasks)
                {
                    currentTask = task;
                    await task.BeginTask(sectionCancellationTokenSource.Token);
                }
                OnSectionCompleted?.Invoke();
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Section was cancelled");
            }
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

        public void SkipSection()
        {
            sectionCancellationTokenSource?.Cancel();
        }
    }

}
