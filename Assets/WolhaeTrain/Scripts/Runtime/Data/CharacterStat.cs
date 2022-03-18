using System;
using System.Collections.Generic;

[Serializable]
public record CharacterStat {
	public CharacterType Type;
	public int Hunger;
	public int Mental;
	public StatusEffect[] Effects;
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
