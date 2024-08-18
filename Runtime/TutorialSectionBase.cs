using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using Debug = UnityEngine.Debug;

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

        public async Task<List<KeyValuePair<string, double>>> StartAsync(CancellationToken externalCancellationToken)
        {
            sectionCancellationTokenSource = new CancellationTokenSource();
            var linkedCancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(sectionCancellationTokenSource.Token, externalCancellationToken);

            var taskTimes = new List<KeyValuePair<string, double>>();
            Stopwatch stopwatch = new();

            try
            {

                foreach (var task in tasks)
                {
                    currentTask = task;

                    // Start timing the task
                    stopwatch.Reset();
                    stopwatch.Start();

                    await task.BeginTask(linkedCancellationTokenSource.Token);

                    // Stop timing and store the result as a key-value pair
                    stopwatch.Stop();
                    taskTimes.Add(new KeyValuePair<string, double>(task.taskName, stopwatch.Elapsed.TotalSeconds));
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Section was cancelled");
                stopwatch.Stop();
                taskTimes.Add(new KeyValuePair<string, double>(currentTask.taskName, -1));
            }
            finally
            {
                linkedCancellationTokenSource.Dispose();
                sectionCancellationTokenSource.Dispose();
            }

            // Return the list of task names and times
            return taskTimes;
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
