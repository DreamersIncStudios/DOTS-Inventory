using Dreamers.CADSystem.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dreamers.CADSystem
{
    [CustomPropertyDrawer(typeof(arrayLayout))]
    public class TestArrayDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PrefixLabel(position, label);
            Rect newposition = position;
            newposition.y += 18f;
            SerializedProperty data = property.FindPropertyRelative("rows");
            SerializedProperty size = property.FindPropertyRelative("Size");
            EditorGUI.PropertyField(newposition, size);
            newposition.y += 18f;

            if (data.arraySize != size.vector2IntValue.x)
                data.arraySize = size.vector2IntValue.x;
            for (int i = 0; i < size.vector2IntValue.x; i++)
            {
                SerializedProperty row = data.GetArrayElementAtIndex(i).FindPropertyRelative("row");
                newposition.height = 18f;
                if (row.arraySize != size.vector2IntValue.y)
                    row.arraySize = size.vector2IntValue.y;
                newposition.width = 20;
                for (int j = 0; j < size.vector2IntValue.y; j++)
                {
                    EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(j), GUIContent.none);
                    //  (newposition, row.GetArrayElementAtIndex(j), GUIContent.none);
                    newposition.x += newposition.width;
                }
                newposition.x = position.x;
                newposition.y += 18f;

            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 18f * 8;
        }
    }
}