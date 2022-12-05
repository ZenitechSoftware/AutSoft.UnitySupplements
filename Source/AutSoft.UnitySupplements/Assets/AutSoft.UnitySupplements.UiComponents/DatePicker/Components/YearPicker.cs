using AutSoft.UnitySupplements.Vitamins;
using UnityEngine;

namespace AutSoft.UnitySupplements.UiComponents.DatePicker.Components
{
    public class YearPicker : MonoBehaviour
    {
        public void SpawnYears(int startYear)
        {
            transform.DestroyChildren();
            startYear -= startYear % 10;
            for (var i = 0; i < 20; i++)
            {
                var currentYear = Instantiate(Resources.Load<GameObject>("YearButton"), transform);
                currentYear.GetComponent<YearButton>().SetupYearButton(startYear + i);
            }
        }
    }
}
