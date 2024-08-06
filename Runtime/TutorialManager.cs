using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Elisu.TutorialBuilder
{
    public class TutorialManager : MonoBehaviour
    {
        [SerializeField] bool StartOnPlay = false;
        [SerializeField] TutorialSectionBase[] sections;

        [SerializeField] public UnityEvent onSectionChanged;


        private int currentSectionIndex = 0;

        private void Start()
        {
            if (StartOnPlay)
            {
                StartTutorial();
            }
        }

        public async void StartTutorial()
        {
            foreach (var section in sections)
            {
                section.OnSectionCompleted.AddListener(StartNextSection);
            }

            sections[currentSectionIndex].StartAsync();
        }

        private void StartNextSection()
        {
            StartCoroutine(ContinueTutorial());
        }

        private bool TutorialCompleted()
        {
            if (currentSectionIndex >= sections.Length - 1)
            {
                return true;
            }

            return false;
        }

        IEnumerator ContinueTutorial()
        {
            Debug.Log("Indicate completion");

            if (TutorialCompleted())
            {
                Debug.Log("Completed");
                yield break;
            }

            currentSectionIndex += 1;
            //NpcController.MoveToView();
            sections[currentSectionIndex].StartAsync();
        }
    }

}