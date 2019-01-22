using UnityEditor;

[CustomEditor(typeof(CharacterStats))]
public class CharacterStatsEditor : Editor {

    private bool showStats = false;

    // TODO When saving changes to code all base values reset to 0 (new StatSheet created)
    // Consider loading from a ScriptableObject?
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CharacterStats stats = (CharacterStats) target;
        drawStatSheet(stats.Stats);
    }

    private void drawStatSheet(StatSheet sheet)
    {
        showStats = EditorGUILayout.Foldout(showStats, "Stats");
        if(showStats)
        {
            foreach (StatType type in StatFormulas.STAT_TYPES)
            {
                Stat stat = sheet.Get(type);
                EditorGUILayout.LabelField(type.ToString());
                EditorGUI.indentLevel++;
                stat.BaseValue = EditorGUILayout.FloatField("Base", stat.BaseValue);
                // TODO Add view for StatModifier list
                EditorGUILayout.LabelField("Final", stat.Value.ToString());
                EditorGUI.indentLevel--;
            }
        }
    }

}
