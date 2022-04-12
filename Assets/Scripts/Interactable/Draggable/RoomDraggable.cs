using RoomCustomization;
using UnityEngine;

namespace Interactable.Draggable
{
    public class RoomDraggable : Draggable
    {
        public PositionSaveController saveController;
        
        public override void OnMouseDown()
        {
            IsDragged = true;

            Vector3 position = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;

            DraggableObject = Instantiate(spawnedPrefab, position, Quaternion.identity);
            
            // set attributes of placed object
            DraggableObject.GetComponent<SpriteRenderer>().sprite =
                gameObject.GetComponent<SpriteRenderer>().sprite;
            DraggableObject.name = gameObject.name;
            DraggableObject.GetComponent<PlacedRoomObject>().saveController = saveController;
            
            MouseDragStartPosition = MainCamera.ScreenToWorldPoint(Input.mousePosition);
            SpriteDragStartPosition = DraggableObject.transform.localPosition;

            DragStartedCallback();
        }
        
        public override void OnMouseUp()
        {
            IsDragged = false;
            DragEndedCallback(DraggableObject.GetComponent<PlacedObject>(), 
                spawnedObjectRequiresExternalSnapPoint);
            
            saveController.placedRooms.Add(DraggableObject);
        }
    }
}