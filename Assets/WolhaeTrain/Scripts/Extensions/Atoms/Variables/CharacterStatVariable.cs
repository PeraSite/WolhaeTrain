using UnityEngine;
using System;

namespace UnityAtoms
{
    /// <summary>
    /// Variable of type `CharacterStat`. Inherits from `AtomVariable&lt;CharacterStat, CharacterStatPair, CharacterStatEvent, CharacterStatPairEvent, CharacterStatCharacterStatFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/CharacterStat", fileName = "CharacterStatVariable")]
    public sealed class CharacterStatVariable : AtomVariable<CharacterStat, CharacterStatPair, CharacterStatEvent, CharacterStatPairEvent, CharacterStatCharacterStatFunction>
    {
        protected override bool ValueEquals(CharacterStat other) {
            return _value.Equals(other);
        }
    }
}
