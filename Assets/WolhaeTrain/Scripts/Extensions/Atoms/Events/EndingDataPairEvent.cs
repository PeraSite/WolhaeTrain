using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `EndingDataPair`. Inherits from `AtomEvent&lt;EndingDataPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/EndingDataPair", fileName = "EndingDataPairEvent")]
    public sealed class EndingDataPairEvent : AtomEvent<EndingDataPair>
    {
    }
}
