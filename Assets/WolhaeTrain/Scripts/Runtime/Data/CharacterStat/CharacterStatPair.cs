using System;
using UnityAtoms;
using UnityEngine;

public struct CharacterStatPair : IPair<CharacterStat> {
	public CharacterStat Item1 {
		get => _item1;
		set => _item1 = value;
	}

	public CharacterStat Item2 {
		get => _item2;
		set => _item2 = value;
	}

	[SerializeField]
	private CharacterStat _item1;

	[SerializeField]
	private CharacterStat _item2;

	public void Deconstruct(out CharacterStat item1, out CharacterStat item2) {
		item1 = Item1;
		item2 = Item2;
	}
}
