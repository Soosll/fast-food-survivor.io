using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Test
{
    [CreateAssetMenu(menuName = "TestSO", fileName = "TestSo", order = 51)]
    public class TestSo : ScriptableObject
    {
        public List<TestData> Datas;
    }

    [Serializable]
    public class TestData
    {
        public string Name;
    }

    public class ForTest
    {
    //     private EnemySpawnerData _spawnerData;
    //     private SerializedProperty EnemySpawnConfig;
    //     
    //     private int _addedElementsCount = 0;
    //
    //     private List<bool> _openedEnemies = new List<bool>();
    //
    //     private bool _trueAlways = true;
    //
    //     private void OnEnable()
    //     {
    //         _spawnerData = (EnemySpawnerData)target;
    //
    //         EnemySpawnConfig = serializedObject.FindProperty("EnemySpawnConfig");
    //         
    //         EnemySpawnConfig.InsertArrayElementAtIndex(0);
    //     }
    //
    //     public override void OnInspectorGUI()
    //     {
    //         serializedObject.Update();
    //         
    //         for (int i = 0; i < 1; i++)
    //         {
    //             var currentConfig = _spawnerData.EnemySpawnConfig[i];
    //             
    //             Debug.Log($"Получил {currentConfig}");
    //             
    //             GUILayout.BeginHorizontal();
    //             _trueAlways = EditorGUILayout.BeginFoldoutHeaderGroup(_trueAlways, "sdasdasdas");
    //             
    //             DrawRemoveEnemyButton(i);
    //             
    //             GUILayout.EndHorizontal();
    //             
    //           //  if(_openedEnemies.Count == 0)
    //            //     continue;
    //             
    //             if (_trueAlways)
    //             {
    //                 EnemySpawnConfig.GetArrayElementAtIndex(i);
    //                 
    //                 DrawEnemyID(i);
    //
    //                 for (int j = 0; j < _spawnerData.EnemySpawnConfig[i].SpawnParameters.Count; j++)
    //                 {
    //                     EnemySpawnParameters currentParameters;
    //                     
    //                     currentParameters = currentConfig.SpawnParameters[j] ?? new EnemySpawnParameters();
    //                     
    //                     GUILayout.BeginVertical(GUI.skin.window);
    //                     
    //                     GUILayout.BeginHorizontal(GUI.skin.box);
    //                     EditorGUIUtility.labelWidth = 70;
    //                     currentParameters.SpawnMinutes = EditorGUILayout.Vector2IntField("Minutes:", currentParameters.SpawnMinutes, GUILayout.Width(220), GUILayout.ExpandWidth(false));
    //                     
    //                     if(!DrawRemoveParameterButton(i, j))
    //                         return;
    //                     
    //                     GUILayout.EndHorizontal();
    //
    //                     for (int k = 0; k < currentParameters.SpawnMinutes.y - currentParameters.SpawnMinutes.x; k++)
    //                     {
    //                         
    //                         GUILayout.BeginHorizontal(GUI.skin.box);
    //                         EditorGUIUtility.labelWidth = 90;
    //                         EditorGUILayout.LabelField($"{currentParameters.SpawnMinutes.x + k} min:", GUILayout.Width(45));
    //                         
    //                         currentParameters.SpawnCount = EditorGUILayout.IntField("Spawn Count:", currentParameters.SpawnCount, GUILayout.Width(125), GUILayout.ExpandWidth(false));
    //                         currentParameters.SpawnRate = EditorGUILayout.IntField("Spawn Rate:", currentParameters.SpawnRate, GUILayout.Width(115), GUILayout.ExpandWidth(false));
    //                        
    //                         GUILayout.EndHorizontal();
    //                     }
    //
    //
    //                     GUILayout.EndVertical();
    //                 }
    //
    //                 GUILayout.BeginHorizontal();
    //                 
    //                 DrawAddParameterButton(i);
    //
    //                 
    //                 
    //                 GUILayout.EndHorizontal();
    //             }
    //
    //             EditorGUILayout.EndFoldoutHeaderGroup();
    //
    //         }
    //         
    //         GUILayout.Space(30);
    //
    //         GUILayout.BeginHorizontal();
    //         
    //         DrawAddEnemyButton();
    //
    //         GUILayout.EndHorizontal();
    //
    //         if (GUI.changed)
    //         {
    //             EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    //         }
    //         EditorUtility.SetDirty(_spawnerData);
    //
    //         serializedObject.ApplyModifiedProperties();
    //     }
    //
    //     private void DrawAddEnemyButton()
    //     {
    //         if (GUILayout.Button("Add Enemy", GUILayout.Width(100), GUILayout.ExpandWidth(false)))
    //         {
    //             EnemySpawnConfig.InsertArrayElementAtIndex(_addedElementsCount);
    //             var parameters = new EnemySpawnParameters();
    //
    //           //  _spawnerData.EnemySpawnConfig.Add(data);
    //           //  _spawnerData.EnemySpawnConfig[_addedElementsCount].SpawnParameters = new List<EnemySpawnParameters>();
    //           //  _spawnerData.EnemySpawnConfig[_addedElementsCount].SpawnParameters.Add(parameters);
    //
    //             bool isOpen = false;
    //             _openedEnemies.Add(isOpen);
    //
    //             _addedElementsCount++;
    //         }
    //     }
    //
    //     private void DrawRemoveEnemyButton(int element)
    //     {
    //         if (GUILayout.Button("Remove Enemy", GUILayout.Width(120)))
    //         {
    //             if (_spawnerData.EnemySpawnConfig.Count == 0)
    //                 return;
    //             
    //             _spawnerData.EnemySpawnConfig.Remove(_spawnerData.EnemySpawnConfig[element]);
    //             
    //             
    //             if (_openedEnemies.Count == 0)
    //                 return;
    //             
    //             _openedEnemies.Remove(_openedEnemies[element]);
    //
    //             Debug.Log($"После удаления = {_openedEnemies.Count}");
    //
    //             _addedElementsCount--;
    //         }
    //     }
    //
    //     private bool DrawRemoveParameterButton(int element, int parameter)
    //     {
    //         if (GUILayout.Button("Remove Param", GUILayout.Width(120), GUILayout.ExpandWidth(false)))
    //         {
    //             var spawnParameters = _spawnerData.EnemySpawnConfig[element].SpawnParameters;
    //
    //             if (spawnParameters.Count == 0)
    //                 return false;
    //
    //             _spawnerData.EnemySpawnConfig[element].SpawnParameters.Remove(spawnParameters[parameter]);
    //         }
    //
    //         return true;
    //     }
    //
    //     private void DrawAddParameterButton(int element)
    //     {
    //         if (GUILayout.Button("Add Param", GUILayout.Width(120)))
    //         {
    //             var parameters = new EnemySpawnParameters();
    //             _spawnerData.EnemySpawnConfig[element].SpawnParameters.Add(parameters);
    //         }
    //     }
    //
    //     private void DrawEnemyID(int index)
    //     {
    //         GUILayout.BeginHorizontal(GUI.skin.box);
    //         EditorGUIUtility.labelWidth = 70;
    //         var id = EnemySpawnConfig.GetArrayElementAtIndex(index).FindPropertyRelative("EnemyId").stringValue;
    //         
    //         EnemySpawnConfig.GetArrayElementAtIndex(index).FindPropertyRelative("EnemyId").stringValue = EditorGUILayout.TextField( EnemySpawnConfig.GetArrayElementAtIndex(index).FindPropertyRelative("EnemyId").stringValue, GUILayout.Width(280), GUILayout.ExpandWidth(false));
    //         GUILayout.EndHorizontal();
    //     }
    //     
    //     // private List<T> UpdateList<T>(List<T> oldList) TODO если удаление не смещает элементы
    // }
    }
}