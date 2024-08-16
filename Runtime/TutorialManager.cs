using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] bool startOnPlay = false;
        [SerializeField] bool trackAnalytics = true;
        [SerializeField] TutorialSectionBase[] sections;

        [SerializeField] public UnityEvent OnSectionChanged;


        int currentSectionIndex = -1;
        bool tutorialCompleted = false;

        private Dictionary<string, List<KeyValuePair<string, double>>> allTaskTimes = new();

        private void Start()
        {
            if (startOnPlay)
            {
                StartTutorial();
            }
        }

        public async void StartTutorial()
        {
            StartTorialLoopAsync();
        }

        private async void StartTorialLoopAsync()
        {
            for (int i = 0; i < sections.Length; i++)
            {
                currentSectionIndex = i;
                var taskTimes = await sections[currentSectionIndex].StartAsync();
                allTaskTimes[sections[currentSectionIndex].name] = taskTimes;
                OnSectionChanged?.Invoke();
            }

            tutorialCompleted = true;
            SaveTaskTimesToFile();
        }

        private bool TutorialCompleted()
        {
            if (currentSectionIndex >= sections.Length - 1)
            {
                return true;
            }

            return false;
        }

        public void SkipCurrentTask()
        {
            if (tutorialCompleted == false)
            {
                sections[currentSectionIndex].SkipCurrentTask();
            }
        }

        void SkipCurrentSection()
        {
            if (tutorialCompleted == false)
            {
                sections[currentSectionIndex].SkipSection();
            }
        }

        // Save all task times to a single JSON file
        private void SaveTaskTimesToFile()
        {
            // Get the current timestamp formatted as "yyyy-MM-dd_HH-mm" (year, month, day, hour, minute)
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm");

            // Append the timestamp to the filename
            string fileName = $"tutorial_task_times_{timestamp}.json";
            string filePath = Path.Combine(Application.persistentDataPath, fileName);

            try
            {
                // Serialize the task times dictionary to JSON
                string json = JsonConvert.SerializeObject(allTaskTimes, Formatting.Indented);

                File.WriteAllText(filePath, json);

                Debug.Log($"All task times saved to {filePath}");
            }
            catch (Exception ex)
            {
                // Log the error message if there was an issue
                Debug.LogError($"Failed to save task times: {ex.Message}");
            }
        }
    }

}