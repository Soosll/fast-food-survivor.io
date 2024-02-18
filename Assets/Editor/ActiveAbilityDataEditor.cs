using System;
using System.Collections.Generic;
using Data.Enums;
using Data.Static.Abilities;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zun010.MonoLinks;

namespace Editor
{
    [CustomEditor(typeof(ActiveAbilityData))]
    public class ActiveAbilityDataEditor : UnityEditor.Editor
    {
        private ActiveAbilityData _abilityData;
        
        public SerializedProperty Id;
        public SerializedProperty AbilitySprite;
        public SerializedProperty Rarity;
        public SerializedProperty MaxLevel;
        public SerializedProperty ProjectilePrefab;
        public SerializedProperty ActiveAbilityParameters;
        
        private List<bool> _openedHeaderGroups = new();
        
        public AbilityParametersForEditorEnum parametersForEditorEnum;
        
        private void OnEnable()
        {
            _abilityData = (ActiveAbilityData)target;

            Id = serializedObject.FindProperty("Id");
            AbilitySprite = serializedObject.FindProperty("AbilitySprite");
            ProjectilePrefab = serializedObject.FindProperty("ProjectilePrefab");
            Rarity = serializedObject.FindProperty("Rarity");
            MaxLevel = serializedObject.FindProperty("MaxLevel");
            ActiveAbilityParameters = serializedObject.FindProperty("ActiveAbilityParameters");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            GUILayout.BeginVertical(GUI.skin.window);
            
            DrawIdField();

            DrawSpriteField();
            
            DrawEntityPrefabField();

            DrawRarityField();

            DrawMaxLevelField();

            GUILayout.EndVertical();

            while (_openedHeaderGroups.Count < _abilityData.MaxLevel)
                _openedHeaderGroups.Add(false);

            if(_abilityData.MaxLevel == 0)
                return;
            
            DrawParametersForEveryLevel();
            
            if (GUI.changed)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                EditorUtility.SetDirty(_abilityData);
            }
            
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawEntityPrefabField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUIUtility.labelWidth = 100;
            EditorGUILayout.ObjectField(ProjectilePrefab, typeof(MonoEntity));
            GUILayout.EndHorizontal();
        }

        private void DrawSpriteField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.Width(50));
            EditorGUIUtility.labelWidth = 75;
            AbilitySprite.objectReferenceValue = (Sprite)EditorGUILayout.ObjectField("AbilityIcon:", _abilityData.AbilitySprite, typeof(Sprite), false);
            GUILayout.EndHorizontal();
        }

        private void DrawRarityField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box, GUILayout.Width(180));
            EditorGUIUtility.labelWidth = 60;
            _abilityData.Rarity = (Rarity)EditorGUILayout.EnumPopup("Rarity", _abilityData.Rarity);
            GUILayout.EndHorizontal();
        }

        private void DrawParametersForEveryLevel()
        {
            for (int i = 0; i < _abilityData.MaxLevel; i++)
            {
                EditorGUILayout.BeginVertical();

                EditorGUILayout.Space(10);
                _openedHeaderGroups[i] = EditorGUILayout.BeginFoldoutHeaderGroup(_openedHeaderGroups[i], $"Level {i + 1}");
                
                while (ActiveAbilityParameters.arraySize < MaxLevel.intValue)
                    ActiveAbilityParameters.InsertArrayElementAtIndex(ActiveAbilityParameters.arraySize);

                while (ActiveAbilityParameters.arraySize > MaxLevel.intValue)
                {
                    ActiveAbilityParameters.DeleteArrayElementAtIndex(ActiveAbilityParameters.arraySize - 1);
                    _openedHeaderGroups.Remove(_openedHeaderGroups[_openedHeaderGroups.Count - 1]);
                }
                
                if (_openedHeaderGroups[i])
                {
                    GUILayout.BeginVertical(GUI.skin.window);
                    
                    var parameters = ActiveAbilityParameters.GetArrayElementAtIndex(i);

                    GUILayout.BeginHorizontal(GUI.skin.box);
                    EditorGUILayout.LabelField("Description:", GUILayout.Width(80));
                    parameters.FindPropertyRelative("Description").stringValue = EditorGUILayout.TextField(parameters.FindPropertyRelative("Description").stringValue, GUILayout.Width(300));
                    GUILayout.EndHorizontal();

                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    GUILayout.BeginHorizontal(GUI.skin.box);
                    var activeAbilitiesEnumProperty = parameters.FindPropertyRelative("ForEditorEnum");
                    EditorGUIUtility.labelWidth = 100;
                    EditorGUILayout.PropertyField(activeAbilitiesEnumProperty);
                    GUILayout.EndHorizontal();
                    
                    var enumNames = activeAbilitiesEnumProperty.enumNames;
                    
                    for (int j = 0; j < enumNames.Length; j++)
                    {
                        var enumType = (AbilityParametersForEditorEnum)(1 << j);

                        if (_abilityData.ActiveAbilityParameters[i].ForEditorEnum.HasFlag(enumType))
                        {
                            DrawParameterIntProperty("Amount", parameters, enumType);
                            DrawParameterFloatProperty("Area", parameters, enumType);
                            DrawParameterFloatProperty("Cooldown", parameters, enumType);
                            DrawParameterFloatProperty("CritMultiplyer", parameters, enumType);
                            DrawParameterFloatProperty("CritChance", parameters, enumType);
                            DrawParameterFloatProperty("Damage", parameters, enumType);
                            DrawParameterFloatProperty("Duration", parameters, enumType);
                            DrawParameterFloatProperty("HitboxDelay", parameters, enumType);
                            DrawParameterFloatProperty("Knockback", parameters, enumType);
                            DrawParameterIntProperty("Pierce", parameters, enumType);
                            DrawParameterFloatProperty("Speed", parameters, enumType);
                        }
                    }
                    
                    GUILayout.EndVertical();
                    
                    GUILayout.EndVertical();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();

                GUILayout.EndVertical();
            }
        }

        private void DrawParameterIntProperty(string propertyName, SerializedProperty parameters, AbilityParametersForEditorEnum enumType)
        {
            if (CompareEnumValueWithString(enumType, propertyName))
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);

                var propertyValue = parameters.FindPropertyRelative(propertyName).intValue;
                EditorGUIUtility.labelWidth = 100;
                parameters.FindPropertyRelative(propertyName).intValue = EditorGUILayout.IntField($"{propertyName}:", propertyValue, GUILayout.Width(135));
                
                EditorGUILayout.EndHorizontal();
            }
        }
        
        private void DrawParameterFloatProperty(string propertyName, SerializedProperty parameters, AbilityParametersForEditorEnum enumType)
        {
            if (CompareEnumValueWithString(enumType, propertyName))
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);

                var propertyValue = parameters.FindPropertyRelative(propertyName).floatValue;
                EditorGUIUtility.labelWidth = 100;
                parameters.FindPropertyRelative(propertyName).floatValue = EditorGUILayout.FloatField($"{propertyName}:", propertyValue, GUILayout.Width(135));
                
                EditorGUILayout.EndHorizontal();
            }
        }

        private void DrawIdField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("Id:", GUILayout.Width(30));
            _abilityData.Id = EditorGUILayout.TextField(Id.stringValue, GUILayout.Width(200), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }

        private void DrawMaxLevelField()
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUILayout.LabelField("MaxAbilityLevel:", GUILayout.Width(120));
            _abilityData.MaxLevel =
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

        private bool CompareEnumValueWithString(Enum value, string text)
        {
            if (value.ToString() == text)
                return true;

            return false;
        }
    }
}