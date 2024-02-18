using System.Collections.Generic;
using Data.Enums;
using Data.Static.Abilities;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Editor
{
    [CustomEditor(typeof(PassiveAbilityData))]
    public class PassiveAbilityDataEditor : UnityEditor.Editor
    {
        public SerializedProperty Id;
        public SerializedProperty AbilitySprite;
        public SerializedProperty Rarity;
        public SerializedProperty MaxLevel;
        public SerializedProperty PassiveAbilityParameters;

        private PassiveAbilityData _passiveAbilityData;

        private List<bool> _openedHeaderGroups = new();

        private void OnEnable()
        {
            _passiveAbilityData = (PassiveAbilityData)target;

            Id = serializedObject.FindProperty("Id");
            AbilitySprite = serializedObject.FindProperty("AbilitySprite");
            MaxLevel = serializedObject.FindProperty("MaxLevel");
            Rarity = serializedObject.FindProperty("Rarity");
            PassiveAbilityParameters = serializedObject.FindProperty("PassiveAbilityParameters");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            GUILayout.BeginVertical(GUI.skin.window);
            
            DrawIdField();

            DrawSpriteField();

            DrawRarityField();

            DrawMaxLevelField();

            GUILayout.EndVertical();

            while (_openedHeaderGroups.Count < _passiveAbilityData.MaxLevel)
                _openedHeaderGroups.Add(false);

            if(_passiveAbilityData.MaxLevel == 0)
                return;
            
            DrawParametersForEveryLevel();
            
            if (GUI.changed)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                EditorUtility.SetDirty(_passiveAbilityData);
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSpriteField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.Width(50));
            EditorGUIUtility.labelWidth = 75;
            AbilitySprite.objectReferenceValue = (Sprite)EditorGUILayout.ObjectField("AbilityIcon:", _passiveAbilityData.AbilitySprite, typeof(Sprite), false);
            GUILayout.EndHorizontal();
        }

        private void DrawRarityField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.Width(180));
            EditorGUIUtility.labelWidth = 60;
            _passiveAbilityData.Rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", _passiveAbilityData.Rarity);
            GUILayout.EndHorizontal();
        }

        private void DrawParametersForEveryLevel()
        {
            for (int i = 0; i < _passiveAbilityData.MaxLevel; i++)
            {
                EditorGUILayout.BeginVertical();

                EditorGUILayout.Space(10);
                _openedHeaderGroups[i] = EditorGUILayout.BeginFoldoutHeaderGroup(_openedHeaderGroups[i], $"Level {i + 1}");

                if (_openedHeaderGroups[i])
                {
                    GUILayout.BeginVertical(GUI.skin.window);

                    if (PassiveAbilityParameters.arraySize < _passiveAbilityData.MaxLevel)
                        PassiveAbilityParameters.InsertArrayElementAtIndex(i);

                    var parameters = PassiveAbilityParameters.GetArrayElementAtIndex(i);

                    DrawDescriptionProperty(parameters);

                    var toggleValue = DrawToggle(parameters);

                    DrawValueProperty(toggleValue, parameters);

                    GUILayout.EndVertical();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                GUILayout.EndVertical();
            }
        }

        private void DrawIdField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("Id:", GUILayout.Width(30));
            _passiveAbilityData.Id =
                EditorGUILayout.TextField(Id.stringValue, GUILayout.Width(200), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }

        private void DrawMaxLevelField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("MaxAbilityLevel:", GUILayout.Width(120));
            _passiveAbilityData.MaxLevel =
                EditorGUILayout.IntField(MaxLevel.intValue, GUILayout.Width(23), GUILayout.ExpandWidth(false));

            GUILayout.Space(20);

            if (GUILayout.Button("Show all", GUILayout.Width(70)))
            {
                for (int i = 0; i < _openedHeaderGroups.Count; i++)
                    _openedHeaderGroups[i] = true;
            }

            if (GUILayout.Button("Close all", GUILayout.Width(70)))
            {
                for (int i = 0; i < _openedHeaderGroups.Count; i++)
                    _openedHeaderGroups[i] = false;
            }

            GUILayout.EndHorizontal();
        }

        private static bool DrawToggle(SerializedProperty parameters)
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("InPercent: ", GUILayout.Width(80));

            var toggleValue = parameters.FindPropertyRelative("InPercent").boolValue;

            parameters.FindPropertyRelative("InPercent").boolValue = EditorGUILayout.Toggle(toggleValue);

            GUILayout.EndHorizontal();
            return toggleValue;
        }

        private static void DrawValueProperty(bool toggleValue, SerializedProperty parameters)
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            if (toggleValue)
            {
                EditorGUILayout.LabelField("% Value: ", GUILayout.Width(80));
                parameters.FindPropertyRelative("Value").floatValue =
                    EditorGUILayout.FloatField(parameters.FindPropertyRelative("Value").floatValue, GUILayout.Width(30));
            }

            else
            {
                EditorGUILayout.LabelField("Value: ", GUILayout.Width(80));
                parameters.FindPropertyRelative("Value").floatValue =
                    EditorGUILayout.FloatField(parameters.FindPropertyRelative("Value").floatValue, GUILayout.Width(30));
            }

            EditorGUILayout.EndHorizontal();
        }

        private static void DrawDescriptionProperty(SerializedProperty parameters)
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("Description:", GUILayout.Width(80));
            parameters.FindPropertyRelative("Description").stringValue = EditorGUILayout.TextField(parameters.FindPropertyRelative("Description").stringValue, GUILayout.Width(200));
            GUILayout.EndHorizontal();
        }
    }
}