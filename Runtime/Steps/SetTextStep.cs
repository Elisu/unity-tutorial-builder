using Elisu.TTSLocalizationKit;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public class SetTextStep : TaskStep
    {
        [SerializeField] string key;

        public TextAndAudioManagerBase instance;

        public override async Task PerformStep(CancellationToken cancellationToken)
        {
            await instance.SetEntryAsync(key:key, cancellationToken: cancellationToken);
        }
    }

}
