using UnityEngine;

namespace Services.InputHandler
{
    public class PlayerInput
    {
        public bool OnLeftMouseUp() => Input.GetMouseButtonUp(0);
        public bool OnLeftMouseDown() => Input.GetMouseButtonDown(0);
        public bool OnLeftMouseGet() => Input.GetMouseButton(0);
        //public bool OnRightMouseDown() => Input.GetMouseButtonDown(1);
        public Vector3 MousePosition() => Input.mousePosition;
        public bool OnKeyDown(KeyCode keyCode) => Input.GetKeyDown(keyCode);
    }
}
