using Elisu.TTSLocalizationKit;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
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

}
