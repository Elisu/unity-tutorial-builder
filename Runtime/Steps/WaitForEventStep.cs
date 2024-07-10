using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class WaitForEventStep : GameObjectTaskStep
{
    [SerializeField] string objectKey;
    [SerializeField] UnityEvent waitFor;

    public override void LoadObject()
    {
        
    }

    public override async Task PerformStep()
    {
        await WaitForEvent(waitFor);
    }

    private async Task WaitForEvent(UnityEvent unityEvent)
    {
        var tcs = new TaskCompletionSource<bool>();

        void action()
        {
            unityEvent.RemoveListener(action);
            tcs.SetResult(true);
        }

        unityEvent.AddListener(action);

        await tcs.Task;
    }

}
