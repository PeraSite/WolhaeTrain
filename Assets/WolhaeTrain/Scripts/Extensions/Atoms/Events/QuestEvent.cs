using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `Quest`. Inherits from `AtomEvent&lt;Quest&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Quest", fileName = "QuestEvent")]
    public sealed class QuestEvent : AtomEvent<Quest>
    {
    }
}
