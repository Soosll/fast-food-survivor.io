using System;
using System.Collections.Generic;
using System.Linq;
using Data.Test;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(TestSo))]
    public class TestSoEditor : UnityEditor.Editor
    {
        private SerializedProperty Datas;

        private TestSo _testSo;

        private bool _isOpen = true;

        private int _field;
        
        private void OnEnable()
        {
            _testSo = (TestSo)target;

            Datas = serializedObject.FindProperty("Datas");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            GUILayout.BeginVertical();

            _field = EditorGUILayout.IntField(_field);

            if (GUILayout.Button("Clicked", GUILayout.Width(135)))
            {
                for (int i = 0; i < _field; i++)
                {
                    Datas.InsertArrayElementAtIndex(i);
                }

                Debug.Log(_testSo.Datas.Count);

            }

            for (int i = 0; i < _testSo.Datas.Count(); i++)
            {
                //EditorGUILayout.PropertyField(Datas.GetArrayElementAtIndex(i).FindPropertyRelative("Name"));
                Datas.GetArrayElementAtIndex(i).FindPropertyRelative("Name").stringValue = EditorGUILayout.TextField(Datas.GetArrayElementAtIndex(i).FindPropertyRelative("Name").stringValue);
            }



            GUILayout.EndVertical();
            


            if (GUI.changed)
            {
                EditorUtility.SetDirty(_testSo);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}