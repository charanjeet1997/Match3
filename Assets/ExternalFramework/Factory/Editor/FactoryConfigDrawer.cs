using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Game.Factories
{
    [CustomPropertyDrawer(typeof(FactoryConfig<>))]
    public class FactoryConfigDrawer : PropertyDrawer
    {
        private List<string> names = new List<string>()
        {
            "StartPoint", "EndPoint", 
            "BombTrap", "SpikeTrap", "SentryTrap", // Traps
            "ArmourBoost", "HealthBoost", "SneakBoost", "StaminaBoost", "ShieldBoost", "DamageBoost", "AmmoBoost", "Melee", "Shooter", "Zombie", // Boosts
            "SelectionGizmo", // Gizmos
            "ArmourBoostParticle", "HealthBoostParticle", "SneakBoostParticle", "StaminaBoostParticle", "ShieldBoostParticle", "AmmoBoostParticle,", // Boosts effects
            "HitParticle", "SentryBullet"
        };

        private const float Padding = 5f; // Padding space
        private bool isFoldout = true; // Foldout state

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Apply padding to the top
            position.y += Padding;
            position.height -= 2 * Padding;

            // Draw the foldout
            isFoldout = EditorGUI.Foldout(new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight), isFoldout, label);
            if (isFoldout)
            {
                // Adjust position for inner properties
                position.y += EditorGUIUtility.singleLineHeight + 2;

                // Calculate rects with adjusted positions
                var prefabRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                var nameRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2, position.width, EditorGUIUtility.singleLineHeight);
                var startImmediatelyRect = new Rect(position.x, position.y + 2 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);
                var numberOfObjectsToCreateRect = new Rect(position.x, position.y + 3 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);
                var delayBetweenInstancesRect = new Rect(position.x, position.y + 4 * (EditorGUIUtility.singleLineHeight + 2), position.width, EditorGUIUtility.singleLineHeight);

                // Draw fields - pass GUIContent.none to each so they are drawn without labels
                EditorGUI.PropertyField(prefabRect, property.FindPropertyRelative("prefab"), new GUIContent("Prefab"));

                string nameValue = property.FindPropertyRelative("name").stringValue;
                int selectedIndex = names.Contains(nameValue) ? names.IndexOf(nameValue) : 0;
                property.FindPropertyRelative("name").stringValue = names[EditorGUI.Popup(nameRect, "Name", selectedIndex, names.ToArray())];

                EditorGUI.PropertyField(startImmediatelyRect, property.FindPropertyRelative("startImmediately"), new GUIContent("Start Immediately"));
                EditorGUI.PropertyField(numberOfObjectsToCreateRect, property.FindPropertyRelative("numberOfObjectsToCreate"), new GUIContent("Number of Objects"));
                EditorGUI.PropertyField(delayBetweenInstancesRect, property.FindPropertyRelative("delayBetweenInstances"), new GUIContent("Delay Between Instances"));
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (isFoldout)
            {
                // 5 fields, each with a single line height, plus padding space at top and bottom
                return (6 * (EditorGUIUtility.singleLineHeight + 2)) + (2 * Padding);
            }
            else
            {
                // Only the foldout height plus padding space
                return EditorGUIUtility.singleLineHeight + 2 * Padding;
            }
        }
    }
}
