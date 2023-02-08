#nullable enable
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.MRTKExtras.Editor
{
    /// <summary>
    /// UnityEditor extensions to add XR related menu items.
    /// </summary>
    public static class CreateMenuXR
    {
        [MenuItem("GameObject/AutSoft/XR/Datepicker", false, 0)]
        public static void CreateDatePicker()
        {
            //Parent
            var findAssets = AssetDatabase
                    .FindAssets("UpmAsset_DatePickerXR", new string[] { "Packages/com.autsoft.unitysupplements.mrtkextras/UiComponents/DatePicker" });

            if (findAssets.Length == 0)
            {
                findAssets =
                    AssetDatabase
                    .FindAssets("UpmAsset_DatePickerXR", new string[] { "Assets/AutSoft.UnitySupplements.MRTKExtras/UiComponents/DatePicker" });
            }

            var datePickerGuid = findAssets[0];
            var guidToAssetPath = AssetDatabase.GUIDToAssetPath(datePickerGuid);
            var prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(guidToAssetPath));
            PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            prefab.name = "DatePickerXR";

            if (Selection.activeTransform != null)
            {
                prefab.transform.SetParent(Selection.activeTransform, false);
            }

            Selection.activeGameObject = prefab;
        }
    }
}
