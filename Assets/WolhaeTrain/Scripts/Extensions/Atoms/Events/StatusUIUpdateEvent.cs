using System;
using UnityAtoms;
using UnityEngine;

[EditorIcon("atom-icon-cherry")]
[CreateAssetMenu(menuName = "Unity Atoms/Events/StatusUIUpdate", fileName = "StatusUIUpdateEvent")]
public class StatusUIUpdateEvent : AtomEvent<StatusUIUpdatePayload> { }

[Serializable]
public struct StatusUIUpdatePayload {
	public bool ShowPanel;
	public CharacterStat Stat;
	public Vector3 Position;
}
