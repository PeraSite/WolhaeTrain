using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `QuestPair`. Inherits from `AtomEvent&lt;QuestPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/QuestPair", fileName = "QuestPairEvent")]
    public sealed class QuestPairEvent : AtomEvent<QuestPair>
    {
    }
}
