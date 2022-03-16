using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

public class CharacterDataContainer : MonoBehaviour {
	public CharacterStatVariable Stat;

	[Button]
	public void Test() {
		Debug.Log(Stat.Value.Hunger);
	}
}
