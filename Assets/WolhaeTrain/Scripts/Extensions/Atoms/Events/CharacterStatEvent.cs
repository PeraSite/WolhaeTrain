using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `CharacterStat`. Inherits from `AtomEvent&lt;CharacterStat&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/CharacterStat", fileName = "CharacterStatEvent")]
    public sealed class CharacterStatEvent : AtomEvent<CharacterStat>
    {
    }
}
