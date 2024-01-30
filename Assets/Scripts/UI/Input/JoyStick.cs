using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Input
{
    public class JoyStick : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _joyStickImage;

        [SerializeField] private float _moveRadius;

        private UnityEngine.Camera _camera;
        
        private void Awake()
        {
            _camera = UnityEngine.Camera.main;
        }

        public Vector2 Direction { get; private set; }
        
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pointerPosition = _camera.ScreenToWorldPoint(eventData.position);

            var inputDirection = pointerPosition - transform.position;

            inputDirection = new Vector3(inputDirection.x, inputDirection.y, 0);

            Vector3 clampedDirection = Vector3.ClampMagnitude(inputDirection, _moveRadius);
            
            _joyStickImage.transform.position = transform.position + clampedDirection;
            
            Vector3 joystickDirection = new Vector3(clampedDirection.x / _moveRadius, clampedDirection.y / _moveRadius, 0);

            Direction = new Vector2(joystickDirection.x, joystickDirection.y);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _joyStickImage.transform.localPosition = Vector2.zero;
            Direction = Vector2.zero;
        }
    }
}