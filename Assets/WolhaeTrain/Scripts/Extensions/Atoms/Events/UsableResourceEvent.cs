using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `UsableResource`. Inherits from `AtomEvent&lt;UsableResource&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/UsableResource", fileName = "UsableResourceEvent")]
    public sealed class UsableResourceEvent : AtomEvent<UsableResource>
    {
    }
}
