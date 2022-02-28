using TMPro;
using UnityEngine;

namespace AutSoft.UnitySupplements.Vitamins
{
    /// <summary>
    /// Sets the text rotation to match the camera's rotation
    /// </summary>
    public class BillboardIcon : MonoBehaviour
    {
        [SerializeField] private TMP_Text _iconText = default!;
        [SerializeField] private Camera _camera = default!;

        private void Awake()
        {
            this.CheckSerializedField(_iconText, nameof(_iconText));
            this.CheckSerializedField(_camera, nameof(_camera));
        }

        //billboard, always look at camera
        private void Update() => transform.rotation = _camera.transform.rotation;

        public void SetIcon(char hexCode) => _iconText.text = hexCode.ToString();
    }
}
