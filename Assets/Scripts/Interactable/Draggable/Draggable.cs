using UnityEngine;

namespace Interactable.Draggable
{
    public class Draggable : MonoBehaviour
    {
        public GameObject spawnedPrefab;
        public bool spawnedObjectRequiresExternalSnapPoint;
        public Sprite spawnedObjectSprite;
        
        public delegate void DragEndedDelegate(PlacedObject spawnedGameObject, bool spawnedObjectRequiresExternalSnapPoint);

        public DragEndedDelegate DragEndedCallback;

        protected bool IsDragged;
        protected Vector3 MouseDragStartPosition;
        protected Vector3 SpriteDragStartPosition;

        protected GameObject DraggableObject;
        protected Camera MainCamera;

        private void Start()
        {
            MainCamera = Camera.main;
        }

        public virtual void OnMouseDown()
        {
            IsDragged = true;

            Vector3 position = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            DraggableObject = Instantiate(spawnedPrefab, position, Quaternion.identity);
            DraggableObject.GetComponent<SpriteRenderer>().sprite = spawnedObjectSprite;
            DraggableObject.name = gameObject.name;
            MouseDragStartPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            SpriteDragStartPosition = DraggableObject.transform.localPosition;
        }

        private void OnMouseDrag()
        {
            if (IsDragged)
            {
                DraggableObject.transform.localPosition = SpriteDragStartPosition +
                                          (MainCamera.ScreenToWorldPoint(Input.mousePosition) -
                                           MouseDragStartPosition);
            }
        }

        public virtual void OnMouseUp()
        {
            IsDragged = false;
            DragEndedCallback(DraggableObject.GetComponent<PlacedObject>(), spawnedObjectRequiresExternalSnapPoint);
        }
    }
}