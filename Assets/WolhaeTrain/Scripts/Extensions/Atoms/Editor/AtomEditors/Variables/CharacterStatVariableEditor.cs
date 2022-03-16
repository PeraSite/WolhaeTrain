using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `CharacterStat`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(CharacterStatVariable))]
    public sealed class CharacterStatVariableEditor : AtomVariableEditor<CharacterStat, CharacterStatPair> { }
}
