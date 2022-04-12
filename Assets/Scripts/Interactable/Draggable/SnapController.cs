using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Interactable.Draggable
{
    public class SnapController : MonoBehaviour
    {
        public List<SnapPoint> snapPoints;
        public List<Draggable> draggableObjects;
        public float snapRange = 0.5f;

        private void Start()
        {
            foreach (var snapPoint in snapPoints)
            {
                snapPoint.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            foreach (var draggable in draggableObjects)
            {
                draggable.DragStartedCallback = OnDragBegin;
                draggable.DragEndedCallback = OnDragEnded;
            }
        }

        private void OnDragBegin()
        {
            foreach (var snapPoint in snapPoints)
            {
                snapPoint.gameObject.SetActive(true);
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

            StartCoroutine(HideSnapPoints());
        }

        private IEnumerator HideSnapPoints()
        {
            yield return new WaitForSeconds(.5f);
            foreach (var snapPoint in snapPoints)
            {
                snapPoint.gameObject.SetActive(false);
            }
        }
    }
}