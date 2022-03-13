using Sirenix.OdinInspector;
using UnityEngine;

public class Select : MonoBehaviour {

	public Sheet<SelectData> Sheet;

	[Button]
	public void Test() {
		foreach (var selectData in Sheet.Data) {
			Debug.Log(selectData.Name);
		}
	}
}
