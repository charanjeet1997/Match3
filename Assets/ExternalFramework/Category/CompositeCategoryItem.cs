// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// #if UNITY_EDITOR
// using UnityEditor;
// #endif
// namespace CompositeModel
// {
//     [System.Serializable]
//     public class CompositeCategoryItem : CategoryItem
//     {
//         public List<CategoryItem> Items;
//         public bool isDisplaying;
//         public override void AddItem(CategoryItem categoryItem)
//         {
//             Items.Add(categoryItem);
//         }
//         public override void RemoveItem(CategoryItem categoryItem)
//         {
//             Items.Remove(categoryItem);
//         }
//         public override bool Display(int depth)
//         {
//             foreach (CategoryItem item in Items)
//             {
//                 item.Display(depth + 2);
//             }
//
//             return isDisplaying;
//         }
//         
// #if UNITY_EDITOR
//         [ContextMenu("Add New CategoryItem")]
//         public void AddNewCategoryItem()
//         {
//             CategoryItem newItem = ScriptableObject.CreateInstance<CategoryItem>();
//             newItem.name = "New CategoryItem";
//             Items.Add(newItem);
//
//             AssetDatabase.AddObjectToAsset(newItem, this);
//             AssetDatabase.SaveAssets();
//
//             EditorUtility.SetDirty(this);
//             EditorUtility.SetDirty(newItem);
//         }
//         
//         [ContextMenu("Add New CompositeCategoryItem")]
//         public void AddNewNestedCompositeCategoryItem()
//         {
//             CompositeCategoryItem newItem = ScriptableObject.CreateInstance<CompositeCategoryItem>();
//             newItem.name = "New Nested CompositeCategoryItem";
//             Items.Add(newItem);
//
//             AssetDatabase.AddObjectToAsset(newItem, this);
//             AssetDatabase.SaveAssets();
//
//             EditorUtility.SetDirty(this);
//             EditorUtility.SetDirty(newItem);
//         }
//
//         [ContextMenu("Delete All CategoryItems")]
//         public void DeleteAllCategoryItems()
//         {
//             for (int i = Items.Count; i-- > 0;)
//             {
//                 CategoryItem tmp = Items[i];
//                 Items.Remove(tmp);
//                 Undo.DestroyObjectImmediate(tmp);
//             }
//             AssetDatabase.SaveAssets();
//         }
// #endif
//     }
// }
