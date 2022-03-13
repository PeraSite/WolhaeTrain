using System.Collections.Generic;
using PeraCore.Editor;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[HideMonoScript]
public class SheetFile : CustomScriptableObject {
	[PropertyOrder(0)]
	public string ID;

	[PropertyOrder(1)]
	[ShowInInspector]
	[ShowIfGroup(nameof(IsValid))]
	public string Name {
		get => name;
		set => name = value;
	}

	[PropertyOrder(2)]
	[ShowIfGroup(nameof(IsValid))]
	public string URL;

	[PropertyOrder(3)]
	[ShowIfGroup(nameof(IsValid))]
	[ListDrawerSettings(HideAddButton = true, CustomRemoveElementFunction = nameof(RemoveWorksheet),
		DraggableItems = false)]
	[InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
	public List<Sheet> Worksheets;

	public bool IsValid() => !ID.IsNullOrWhitespace() && !URL.IsNullOrWhitespace();

	private void RemoveWorksheet(Sheet selected) {
		Worksheets.Remove(selected);

#if UNITY_EDITOR
		selected.DeleteObject();
#endif
	}

#if UNITY_EDITOR
	[ContextMenu("Delete Self")]
	private void DeleteSelf() {
		this.DeleteObject();
	}
#endif
}
