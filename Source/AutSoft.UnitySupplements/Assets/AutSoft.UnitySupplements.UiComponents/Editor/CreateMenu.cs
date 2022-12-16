#nullable enable
using UnityEditor;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.Editor
{
    public static class CreateMenu
    {
        [MenuItem("GameObject/AutSoft/TimeLine/BasicTimelinePlayer", false, 0)]
        public static void CreateTimeLine()
        {
            //Parent
            var findAssets = AssetDatabase.FindAssets("UpmAsset_TimelinePlayer", new string[] { "Packages/UiComponents/Timeline" });

            if (findAssets.Length == 0)
            {
                findAssets = AssetDatabase.FindAssets("UpmAsset_TimelinePlayer", new string[] { "AutSoft.UnitySupplements.UiComponents/Timeline" });
            }
            var timelineGuid = findAssets[0];
            var guidToAssetPath = AssetDatabase.GUIDToAssetPath(timelineGuid);
            var prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(guidToAssetPath));
            PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            prefab.name = "TimeLinePlayer";

            if (Selection.activeTransform != null)
            {
                prefab.transform.SetParent(Selection.activeTransform, false);
            }

            Selection.activeGameObject = prefab;
        }

        [MenuItem("GameObject/AutSoft/Datepicker", false, 0)]
        public static void CreateDatePicker()
        {
            //Parent
            var findAssets = 
                AssetDatabase.FindAssets("UpmAsset_DatePicker", new string[] { "Packages/com.autsoft.unitysupplements.uicomponents/Datepicker" });
            if (findAssets.Length == 0)
            {
                findAssets =
                    AssetDatabase.FindAssets("UpmAsset_DatePicker", new string[] { "Assets/AutSoft.UnitySupplements.UiComponents/Datepicker" });
            }
            var datePickerGuid = findAssets[0];
            var guidToAssetPath = AssetDatabase.GUIDToAssetPath(datePickerGuid);
            var prefab = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(guidToAssetPath));
            PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
            prefab.name = "DatePicker";

            if (Selection.activeTransform != null)
            {
                prefab.transform.SetParent(Selection.activeTransform, false);
            }

            Selection.activeGameObject = prefab;
        }
    }
}
