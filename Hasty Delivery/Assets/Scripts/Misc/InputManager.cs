using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum InputDirection
{
    LeftInput = -1,
    CenterInput = 0,
    RightInput = 1
}

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public event Action<InputDirection> OnInput;

    public const float inputXThreshold = 0.4f;

    private bool canInput = true;
    private int fingerID = -1;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

    #if UNITY_ANDROID || UNITY_IOS
        fingerID = 0; 
    #endif
    }

    private void Update()
    {
        if(EventSystem.current != null)
            if (EventSystem.current.IsPointerOverGameObject(fingerID)) return;
        if (!canInput) return;
    #if UNITY_ANDROID || UNITY_IOS
        ProcessTouch();
    #elif UNITY_EDITOR || UNITY_STANDALONE
        ProcessClick();
    #endif
    }

    private void ProcessClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 clickPosition = Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            InputDirection inputDirection = ProcessInputPosition(clickPosition);
            SendInputDirection(inputDirection);
        }
    }

    private void ProcessTouch()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Vector3 touchedPosition = Camera.main.ScreenToViewportPoint(touch.position);
                InputDirection inputDirection= ProcessInputPosition(touchedPosition);
                SendInputDirection(inputDirection);
            }
        }
    }

    private void SendInputDirection(InputDirection inputDirection)
    {
        print(inputDirection);
        OnInput?.Invoke(inputDirection);
    }

    private InputDirection ProcessInputPosition(Vector3 inputPosition)
    {
        InputDirection inputDirection;
        if (inputPosition.x < inputXThreshold)
            inputDirection = InputDirection.LeftInput;
        else if (inputPosition.x < inputXThreshold + (1 - 2 * inputXThreshold))
            inputDirection = InputDirection.CenterInput;
        else
            inputDirection = InputDirection.RightInput;
        return inputDirection;
    }

    public void SetCanInput(bool canInput)
    {
        this.canInput = canInput;
    }
}
