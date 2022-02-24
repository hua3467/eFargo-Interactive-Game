using System.Collections.Generic;
using UnityEngine;

namespace Interactable.Draggable
{
    public class SnapController : MonoBehaviour
    {
        public List<SnapPoint> snapPoints;
        public List<Draggable> draggableObjects;
        public float snapRange = 0.5f;

        private void Update()
        {
            foreach (var draggable in draggableObjects)
            {
                draggable.DragEndedCallback = OnDragEnded;
            }
        }

        private void OnDragEnded(PlacedObject spawnedGameObject, bool requiresExternalSnapPoint)
        {
            float closestDistance = -1;
            SnapPoint closestSnapPoint = null;

            foreach (var snapPoint in snapPoints)
            {
                var currentDistance = Vector2.Distance(spawnedGameObject.transform.localPosition, snapPoint.transform.localPosition);
                if (closestSnapPoint == null || currentDistance < closestDistance)
                {
                    closestSnapPoint = snapPoint;
                    closestDistance = currentDistance;
                }
            }

            if (closestSnapPoint != null && closestDistance <= snapRange 
                                         && closestSnapPoint.isExternal == requiresExternalSnapPoint
                                         && !closestSnapPoint.IsOccupied)
            {
                spawnedGameObject.transform.localPosition = closestSnapPoint.transform.localPosition;
                closestSnapPoint.IsOccupied = true;
                closestSnapPoint.AttachedGameObject = spawnedGameObject.gameObject;
                spawnedGameObject.SnapPoint = closestSnapPoint;
            }
            else
            {
                Destroy(spawnedGameObject.gameObject);
            }
        }
    }
}