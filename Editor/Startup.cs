using Elisu.TTSLocalizationKitEditor;
using UnityEditor;
using UnityEngine;

namespace Elisu.TutorialBuilderEditor
{
    [InitializeOnLoad]
    public class Startup
    {
        static Startup()
        {
            // TTS package is added as a submodule and we need to override its set path for it to properly find a referenced python script
            TTSGenerator.DefaultTTSPackagePath = "Packages/elisu.tutorial-builder/unity-TTS-localization-kit";
        }
    }

}

