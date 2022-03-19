using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Value List of type `UsableResource`. Inherits from `AtomValueList&lt;UsableResource, UsableResourceEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/UsableResource", fileName = "UsableResourceValueList")]
    public sealed class UsableResourceValueList : AtomValueList<UsableResource, UsableResourceEvent> { }
}
