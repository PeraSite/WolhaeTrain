using System;
using System.Collections.Generic;
using System.Linq;
using PeraCore.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using PeraCore.Editor;
#endif

public class Sheet : CustomScriptableObject {
	[HideInInspector]
	[PropertyOrder(1)]
	public SheetFile Parent;

	[ShowInInspector]
	[PropertyOrder(2)]
	public string Name {
		get => name;
		set => name = value;
	}

	[PropertyOrder(3)]
	[Required("시트 타입을 설정해주세요!")]
	public Type Type;

	[PropertyOrder(3)]
	[ListDrawerSettings(DraggableItems = false)]
	[InlineEditor(InlineEditorObjectFieldModes.Boxed)]
	[HideReferenceObjectPicker]
	public List<object> WeakData = new List<object>();

	public string FullName => $"{Parent.Name}/{Name}";

#if UNITY_EDITOR
	[ContextMenu("Delete Self")]
	private void DeleteSelf() {
		this.DeleteObject();
	}
#endif
}

[Serializable]
public struct Sheet<T> where T : struct {
	public Sheet Worksheet;

	public Sheet(Sheet worksheet) {
		Worksheet = worksheet;
	}

	public List<T> Data => Worksheet.WeakData.Select(obj => (T) obj).ToList();
}
