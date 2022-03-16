using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Event of type `CharacterStatPair`. Inherits from `AtomEvent&lt;CharacterStatPair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/CharacterStatPair", fileName = "CharacterStatPairEvent")]
    public sealed class CharacterStatPairEvent : AtomEvent<CharacterStatPair>
    {
    }
}
