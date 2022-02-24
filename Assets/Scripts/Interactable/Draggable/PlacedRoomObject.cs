using System;
using RoomCustomization;

namespace Interactable.Draggable
{
    public class PlacedRoomObject : PlacedObject
    {
        public PositionSaveController saveController;
        public override void OnMouseDown()
        {
            SnapPoint.IsOccupied = false;
            if (!saveController.placedRooms.Remove(gameObject))
            {
                throw new Exception("object not in list");
            }
            Destroy(gameObject);
        }
    }
}