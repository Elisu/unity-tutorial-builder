using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;

public class TutorialSectionBase : MonoBehaviour
{
    public UnityEvent OnSectionCompleted;

    public List<GameObjectKey> commonObjects = new();

    public List<SectionTask> tasks = new();

    public void UpdateCommonGameobjects()
    {
        foreach (var task in tasks)
        {
            task.AssignCommonGameObjects(commonObjects);
        }
    }

    private async void Start()
    {
        foreach (var task in tasks)
        {
            await task.BeginTask();
        }
    }
}
