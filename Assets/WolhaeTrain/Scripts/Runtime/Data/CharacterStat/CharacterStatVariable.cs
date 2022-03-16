using UnityAtoms;
using UnityEngine;

[EditorIcon("atom-icon-lush")]
[CreateAssetMenu(menuName = "Unity Atoms/Variables/Character Stat", fileName = "CharacterStatVariable")]
public class CharacterStatVariable : AtomVariable<CharacterStat, CharacterStatPair, CharacterStatEvent,
	CharacterStatPairEvent, CharacterStatCharacterStatFunction> {
	protected override bool ValueEquals(CharacterStat other) {
		return _value.Equals(other);
	}
}
