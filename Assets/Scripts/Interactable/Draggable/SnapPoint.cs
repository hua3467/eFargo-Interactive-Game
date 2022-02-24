using UnityEngine;

namespace Interactable.Draggable
{
    public class SnapPoint : MonoBehaviour
    {
        public bool isExternal;
        [System.NonSerialized]
        public bool IsOccupied;
        [System.NonSerialized]
        public GameObject AttachedGameObject;
    }
}