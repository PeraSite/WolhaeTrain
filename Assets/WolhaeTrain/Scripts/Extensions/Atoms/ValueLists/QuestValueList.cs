using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `Quest`. Inherits from `AtomValueList&lt;Quest, QuestEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Quest", fileName = "QuestValueList")]
    public sealed class QuestValueList : AtomValueList<Quest, QuestEvent> {

    }
}
