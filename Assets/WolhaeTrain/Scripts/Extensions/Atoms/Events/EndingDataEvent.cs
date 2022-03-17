using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `EndingData`. Inherits from `AtomEvent&lt;EndingData&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/EndingData", fileName = "EndingDataEvent")]
    public sealed class EndingDataEvent : AtomEvent<EndingData>
    {
    }
}
