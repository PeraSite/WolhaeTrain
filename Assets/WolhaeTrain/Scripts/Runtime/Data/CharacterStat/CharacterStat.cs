using System.Collections.Generic;

public class CharacterStat {
	public int Hunger = 50;
	public int Mental = 50;
	public List<StatusEffect> Effects = new();
}

public enum StatusEffect {
	Exhaust,
	Cold,
	Infect,
	Hurt,
	Crazy,
}
