using UnityEngine;

namespace Interactable.Draggable
{
    public class PlacedObject : MonoBehaviour
    {
        [System.NonSerialized]
        public SnapPoint SnapPoint;
        
        public virtual void OnMouseDown()
        {
            SnapPoint.IsOccupied = false;
            Destroy(gameObject);
        }
    }
}
