using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] TutorialSectionBase[] sections;

    [SerializeField] UnityEvent[] onSectionChanged;


    private int currentSectionIndex = 0;

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
