using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Elisu.TutorialBuilder
{
    [Serializable]
    public abstract class TaskStep
    {
        public abstract Task PerformStep(CancellationToken cancellationToken);

    }

}
