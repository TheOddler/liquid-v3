// GENERATED AUTOMATICALLY FROM 'Assets/Input/InputMaster.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class InputMaster : IInputActionCollection
{
    private InputActionAsset asset;
    public InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7c95da99-1788-459c-9689-f8723f896a5e"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""e3c7c28d-ea72-494d-a440-1b9d25cfc21c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Button"",
                    ""id"": ""05cfb346-e0bf-41bb-82aa-650a3038b5dd"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Add Water"",
                    ""type"": ""Button"",
                    ""id"": ""adc15a07-5697-4e9e-a28c-2aeba3ad475f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""955cf260-9c35-4951-96d1-37b28ac20a62"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""99f3ccab-798a-4fc0-ac1f-dcb74cc2f711"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""98103f83-0edb-414e-853c-e83c1a36ee7c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""921dd091-683f-4d8e-ad82-6b9054990aec"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""94f8e2a8-a0e3-4adf-bed2-6ac0192a7f5e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""fcfde8eb-e3af-4bbb-b361-2aebf118e8aa"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""df10ca5f-b556-48e2-a03d-0445370300d4"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3c193b7c-d0f1-4bfd-ab19-cbbf480c44da"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fe785d1d-ed59-41c4-b4a6-d277b5a3a092"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8102a0ce-d980-4eb4-bf9d-aef0d04b1666"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""11694f47-bc2d-46ac-87c8-3fbaf09036b7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e52cd89b-2061-466b-8f4a-ad1e909f11c4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Add Water"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""9245e5e5-c127-4e6a-9a71-d541a2817a85"",
            ""actions"": [
                {
                    ""name"": ""X Delta"",
                    ""type"": ""Value"",
                    ""id"": ""7465714c-a225-4ccb-afff-abcf123edc20"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Y Delta"",
                    ""type"": ""Button"",
                    ""id"": ""91f72307-c4d8-4e43-9177-423c605874d3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X Position"",
                    ""type"": ""Button"",
                    ""id"": ""a19b00f5-2a43-4fdb-9ddf-b6dead14069c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Y Position"",
                    ""type"": ""Button"",
                    ""id"": ""a44e9a47-d5e8-4ba3-a29e-8e47a324de43"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""095d6cd9-4f59-45e8-bf83-6d13a2321005"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3291d233-9a37-4851-bbb6-c7f3b02d5199"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y Delta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""697d51c9-965b-4318-95a6-4dc3e09761e3"",
                    ""path"": ""<Mouse>/position/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""X Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bab2aa4-f4eb-403a-821c-ede423e2ca1a"",
                    ""path"": ""<Mouse>/position/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Y Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.GetActionMap("Player");
        m_Player_Movement = m_Player.GetAction("Movement");
        m_Player_Look = m_Player.GetAction("Look");
        m_Player_AddWater = m_Player.GetAction("Add Water");
        // Mouse
        m_Mouse = asset.GetActionMap("Mouse");
        m_Mouse_XDelta = m_Mouse.GetAction("X Delta");
        m_Mouse_YDelta = m_Mouse.GetAction("Y Delta");
        m_Mouse_XPosition = m_Mouse.GetAction("X Position");
        m_Mouse_YPosition = m_Mouse.GetAction("Y Position");
    }

    ~InputMaster()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_AddWater;
    public struct PlayerActions
    {
        private InputMaster m_Wrapper;
        public PlayerActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @AddWater => m_Wrapper.m_Player_AddWater;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                Look.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                Look.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                Look.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnLook;
                AddWater.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAddWater;
                AddWater.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAddWater;
                AddWater.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnAddWater;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                Movement.started += instance.OnMovement;
                Movement.performed += instance.OnMovement;
                Movement.canceled += instance.OnMovement;
                Look.started += instance.OnLook;
                Look.performed += instance.OnLook;
                Look.canceled += instance.OnLook;
                AddWater.started += instance.OnAddWater;
                AddWater.performed += instance.OnAddWater;
                AddWater.canceled += instance.OnAddWater;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_XDelta;
    private readonly InputAction m_Mouse_YDelta;
    private readonly InputAction m_Mouse_XPosition;
    private readonly InputAction m_Mouse_YPosition;
    public struct MouseActions
    {
        private InputMaster m_Wrapper;
        public MouseActions(InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @XDelta => m_Wrapper.m_Mouse_XDelta;
        public InputAction @YDelta => m_Wrapper.m_Mouse_YDelta;
        public InputAction @XPosition => m_Wrapper.m_Mouse_XPosition;
        public InputAction @YPosition => m_Wrapper.m_Mouse_YPosition;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                XDelta.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnXDelta;
                XDelta.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnXDelta;
                XDelta.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnXDelta;
                YDelta.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnYDelta;
                YDelta.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnYDelta;
                YDelta.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnYDelta;
                XPosition.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnXPosition;
                XPosition.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnXPosition;
                XPosition.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnXPosition;
                YPosition.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnYPosition;
                YPosition.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnYPosition;
                YPosition.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnYPosition;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                XDelta.started += instance.OnXDelta;
                XDelta.performed += instance.OnXDelta;
                XDelta.canceled += instance.OnXDelta;
                YDelta.started += instance.OnYDelta;
                YDelta.performed += instance.OnYDelta;
                YDelta.canceled += instance.OnYDelta;
                XPosition.started += instance.OnXPosition;
                XPosition.performed += instance.OnXPosition;
                XPosition.canceled += instance.OnXPosition;
                YPosition.started += instance.OnYPosition;
                YPosition.performed += instance.OnYPosition;
                YPosition.canceled += instance.OnYPosition;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAddWater(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnXDelta(InputAction.CallbackContext context);
        void OnYDelta(InputAction.CallbackContext context);
        void OnXPosition(InputAction.CallbackContext context);
        void OnYPosition(InputAction.CallbackContext context);
    }
}
