using UnityEngine;
using System;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `Quest`. Inherits from `AtomVariable&lt;Quest, QuestPair, QuestEvent, QuestPairEvent, QuestQuestFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Quest", fileName = "QuestVariable")]
    public sealed class QuestVariable : AtomVariable<Quest, QuestPair, QuestEvent, QuestPairEvent, QuestQuestFunction>
    {
        protected override bool ValueEquals(Quest other)
        {
            throw new NotImplementedException();
        }
    }
}
