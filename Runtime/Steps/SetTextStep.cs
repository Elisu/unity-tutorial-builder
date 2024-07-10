using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class SetTextStep : TaskStep
{
    [SerializeField] string key;

    public TextAndAudioManagerBase instance;

    public override async Task PerformStep()
    {
       await instance.SetEntryAsync(key);
    }
}
