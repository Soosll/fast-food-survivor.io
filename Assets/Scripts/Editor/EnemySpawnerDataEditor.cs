using System.Collections.Generic;
using Data.Static.Enemies;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor
{
    [CustomEditor(typeof(EnemySpawnerData))]
    public class EnemySpawnerDataEditor : UnityEditor.Editor
    {
        private EnemySpawnerData _spawnerData;
        private SerializedProperty EnemySpawnConfig;

        private int _addedElementsCount = 0;

        private List<bool> _openedEnemies = new List<bool>();

        private bool _trueAlways = true;

        private void OnEnable()
        {
            _spawnerData = (EnemySpawnerData)target;

            EnemySpawnConfig = serializedObject.FindProperty("EnemySpawnConfig");

            for (int i = 0; i < EnemySpawnConfig.arraySize; i++)
            {
                _openedEnemies.Add(false);
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            for (int i = 0; i < EnemySpawnConfig.arraySize; i++)
            {
                GUILayout.BeginHorizontal();

                _openedEnemies[i] = EditorGUILayout.BeginFoldoutHeaderGroup(_openedEnemies[i], $"{_spawnerData.EnemySpawnConfig[i].EnemyId}");

                DrawRemoveEnemyButton(i);

                GUILayout.EndHorizontal();

                if (_openedEnemies[i])
                {
                    if (_spawnerData.EnemySpawnConfig.Count == 0)
                        continue;

                    DrawEnemyID(i);

                    for (int j = 0; j < EnemySpawnConfig.GetArrayElementAtIndex(i).FindPropertyRelative("SpawnParameters").arraySize; j++)
                    {
                        var spawnParametersSO = _spawnerData.EnemySpawnConfig[i].SpawnParameters[j];

                        var spawnParametersProperty = EnemySpawnConfig.GetArrayElementAtIndex(i).FindPropertyRelative("SpawnParameters").GetArrayElementAtIndex(j);

                        var fromMinuteProperty = spawnParametersProperty.FindPropertyRelative("From").intValue;
                        var toMinuteProperty = spawnParametersProperty.FindPropertyRelative("To").intValue;
                        var spawnCount = spawnParametersProperty.FindPropertyRelative("SpawnCount").intValue;
                        var spawnRate = spawnParametersProperty.FindPropertyRelative("SpawnRate").intValue;

                        GUILayout.BeginVertical(GUI.skin.window);

                        GUILayout.BeginHorizontal(GUI.skin.box);
                        EditorGUILayout.LabelField("Minutes:", GUILayout.Width(60));
                        EditorGUIUtility.labelWidth = 40;
                        spawnParametersSO.From = EditorGUILayout.IntField("From:", fromMinuteProperty, GUILayout.Width(68), GUILayout.ExpandWidth(false));
                        EditorGUIUtility.labelWidth = 30;
                        spawnParametersSO.To = EditorGUILayout.IntField("To:", toMinuteProperty, GUILayout.Width(60), GUILayout.ExpandWidth(false));

                        GUILayout.Space(60);
                        GUILayout.EndHorizontal();

                        GUILayout.BeginHorizontal(GUI.skin.box);
                        EditorGUILayout.LabelField("Params:", GUILayout.Width(60));
                        
                        EditorGUIUtility.labelWidth = 40;
                        spawnParametersSO.SpawnCount = EditorGUILayout.IntField("Count:", spawnCount, GUILayout.Width(80), GUILayout.ExpandWidth(false));
                       
                        EditorGUIUtility.labelWidth = 40;
                        spawnParametersSO.SpawnRate = EditorGUILayout.IntField("Rate:", spawnRate, GUILayout.Width(80), GUILayout.ExpandWidth(false));

                        if (!DrawRemoveParameterButton(i, j))
                            return;
                        
                        GUILayout.EndHorizontal();
                        
                        GUILayout.EndVertical();
                    }

                    GUILayout.BeginHorizontal();
                    DrawAddParameterButton(i);
                    GUILayout.EndHorizontal();
                }

                EditorGUILayout.EndFoldoutHeaderGroup();
            }

            GUILayout.Space(30);

            GUILayout.BeginHorizontal();
            DrawAddEnemyButton();
            GUILayout.EndHorizontal();

            if (GUI.changed)
            {
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
            }

            EditorUtility.SetDirty(_spawnerData);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawAddEnemyButton()
        {
            if (GUILayout.Button("Add Enemy", GUILayout.Width(100), GUILayout.ExpandWidth(false)))
            {
                EnemySpawnConfig.InsertArrayElementAtIndex(_addedElementsCount);

                EnemySpawnConfig.GetArrayElementAtIndex(_addedElementsCount).FindPropertyRelative("SpawnParameters").ClearArray();

                bool isOpen = false;
                _openedEnemies.Add(isOpen);

                _addedElementsCount++;
            }
        }

        private void DrawRemoveEnemyButton(int element)
        {
            if (GUILayout.Button("Remove Enemy", GUILayout.Width(120)))
            {
                if (EnemySpawnConfig.arraySize == 0)
                    return;

                EnemySpawnConfig.DeleteArrayElementAtIndex(element);

                _addedElementsCount--;
            }
        }

        private bool DrawRemoveParameterButton(int element, int parameter)
        {
            if (GUILayout.Button("Remove Param", GUILayout.Width(110), GUILayout.ExpandWidth(false)))
            {
                var spawnParameters = EnemySpawnConfig.GetArrayElementAtIndex(element).FindPropertyRelative("SpawnParameters");
                
                spawnParameters.DeleteArrayElementAtIndex(parameter);
            }
            return true;
        }

        private void DrawAddParameterButton(int spawnElement)
        {
            if (GUILayout.Button("Add Param", GUILayout.Width(120)))
            {
                var spawnParameters = EnemySpawnConfig.GetArrayElementAtIndex(spawnElement).FindPropertyRelative("SpawnParameters");
                spawnParameters.InsertArrayElementAtIndex(spawnParameters.arraySize);
            }
        }

        private void DrawEnemyID(int index)
        {
            GUILayout.BeginHorizontal(GUI.skin.box);
            EditorGUIUtility.labelWidth = 70;

            EnemySpawnConfig.GetArrayElementAtIndex(index).FindPropertyRelative("EnemyId").stringValue = EditorGUILayout.TextField("Enemy Id:",
                EnemySpawnConfig.GetArrayElementAtIndex(index).FindPropertyRelative("EnemyId").stringValue,
                GUILayout.Width(280), GUILayout.ExpandWidth(false));
            GUILayout.EndHorizontal();
        }
    }
}