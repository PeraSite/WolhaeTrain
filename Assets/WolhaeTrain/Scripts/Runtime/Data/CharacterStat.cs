using System;
using System.Collections.Generic;

[Serializable]
public record CharacterStat {
	public CharacterType Type;
	public int Hunger;
	public int Mental;
	public StatusEffect[] Effects;

	// public bool Equals(CharacterStat other) {
	// 	return Type == other.Type && Hunger == other.Hunger && Mental == other.Mental && Equals(Effects, other.Effects);
	// }
	//
	// public override bool Equals(object obj) {
	// 	return obj is CharacterStat other && Equals(other);
	// }
	//
	// public override int GetHashCode() {
	// 	return HashCode.Combine((int) Type, Hunger, Mental, Effects);
	// }
}

public enum CharacterType {
	None,
	Dad,
	Mom,
	Son,
	Daughter
}

public enum StatusEffect {
	Exhaust,
	Cold,
	Infect,
	Hurt,
	Crazy,
}
