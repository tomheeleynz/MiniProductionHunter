// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""0c8d48ad-c64b-4359-92f6-9f1c29f8f82e"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""ac26353b-c3c3-42a4-9c7f-1cbeee366f2e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""3fdf4172-a271-4089-a3e2-84b37f67e5b8"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DrawBow"",
                    ""type"": ""Value"",
                    ""id"": ""140d0f3e-1d60-4544-aad9-7875584f4cc9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""61315948-6a9b-4eb4-8443-f880913882fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LStick Down"",
                    ""type"": ""Button"",
                    ""id"": ""fd15c1a0-7859-4695-831a-2b37c71e2f57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""45bfa4c7-8a6a-4643-aa72-671802eebd4c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""a96f2b4e-be32-4c51-8747-d81bb0a75673"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interactable"",
                    ""type"": ""Button"",
                    ""id"": ""ebd7206e-ef50-4e04-9c7c-9b720e258d8f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bdf21150-9abd-47e4-bd5c-42ed1b9ae3c7"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f82e745f-cc40-476f-9e8a-43669e0b864a"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c747b2c-5f5c-4316-90f6-5acfecfc1ca0"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DrawBow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""63012453-369b-496f-828a-cff3fe3a1301"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""50dca58c-0b92-454c-a287-4b5b67fda166"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LStick Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb13b0bf-5146-4b45-a32b-d8d4e5ccbabf"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""421b654c-dba2-4d14-91fa-b9e8dbd12c55"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6e813e1-ccdc-4de2-896f-3460b00b88cf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interactable"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""dfe3a57f-5f34-4d83-a058-8c0e04aaa89c"",
            ""actions"": [
                {
                    ""name"": ""MoveDownMenu"",
                    ""type"": ""Button"",
                    ""id"": ""fb2475b0-5da4-4745-b38e-87db6cb6c4c6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveUpMenu"",
                    ""type"": ""Button"",
                    ""id"": ""db86535f-9566-401b-82f6-b87d50f2f34f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""fbe1611d-10e6-43de-aeab-c61b2cbbc81c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""81b8d448-5ff4-484b-8928-a211b2b36ad0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f9ce3e79-1ee6-4120-aaa1-c816976b8bbd"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveDownMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca7effdc-7597-4d18-adb5-4c79297fcdb9"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveUpMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""091d302d-eeb8-4485-b83f-691b00134d99"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff5da198-a63f-4764-975d-c715d3f07356"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_Rotate = m_Gameplay.FindAction("Rotate", throwIfNotFound: true);
        m_Gameplay_DrawBow = m_Gameplay.FindAction("DrawBow", throwIfNotFound: true);
        m_Gameplay_Fire = m_Gameplay.FindAction("Fire", throwIfNotFound: true);
        m_Gameplay_LStickDown = m_Gameplay.FindAction("LStick Down", throwIfNotFound: true);
        m_Gameplay_Crouch = m_Gameplay.FindAction("Crouch", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_Interactable = m_Gameplay.FindAction("Interactable", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_MoveDownMenu = m_UI.FindAction("MoveDownMenu", throwIfNotFound: true);
        m_UI_MoveUpMenu = m_UI.FindAction("MoveUpMenu", throwIfNotFound: true);
        m_UI_Select = m_UI.FindAction("Select", throwIfNotFound: true);
        m_UI_Quit = m_UI.FindAction("Quit", throwIfNotFound: true);
    }

    public void Dispose()
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_Rotate;
    private readonly InputAction m_Gameplay_DrawBow;
    private readonly InputAction m_Gameplay_Fire;
    private readonly InputAction m_Gameplay_LStickDown;
    private readonly InputAction m_Gameplay_Crouch;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_Interactable;
    public struct GameplayActions
    {
        private @PlayerControls m_Wrapper;
        public GameplayActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @Rotate => m_Wrapper.m_Gameplay_Rotate;
        public InputAction @DrawBow => m_Wrapper.m_Gameplay_DrawBow;
        public InputAction @Fire => m_Wrapper.m_Gameplay_Fire;
        public InputAction @LStickDown => m_Wrapper.m_Gameplay_LStickDown;
        public InputAction @Crouch => m_Wrapper.m_Gameplay_Crouch;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @Interactable => m_Wrapper.m_Gameplay_Interactable;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMove;
                @Rotate.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRotate;
                @DrawBow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDrawBow;
                @DrawBow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDrawBow;
                @DrawBow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDrawBow;
                @Fire.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnFire;
                @LStickDown.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLStickDown;
                @LStickDown.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLStickDown;
                @LStickDown.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnLStickDown;
                @Crouch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Interactable.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteractable;
                @Interactable.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteractable;
                @Interactable.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInteractable;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @DrawBow.started += instance.OnDrawBow;
                @DrawBow.performed += instance.OnDrawBow;
                @DrawBow.canceled += instance.OnDrawBow;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @LStickDown.started += instance.OnLStickDown;
                @LStickDown.performed += instance.OnLStickDown;
                @LStickDown.canceled += instance.OnLStickDown;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Interactable.started += instance.OnInteractable;
                @Interactable.performed += instance.OnInteractable;
                @Interactable.canceled += instance.OnInteractable;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_MoveDownMenu;
    private readonly InputAction m_UI_MoveUpMenu;
    private readonly InputAction m_UI_Select;
    private readonly InputAction m_UI_Quit;
    public struct UIActions
    {
        private @PlayerControls m_Wrapper;
        public UIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveDownMenu => m_Wrapper.m_UI_MoveDownMenu;
        public InputAction @MoveUpMenu => m_Wrapper.m_UI_MoveUpMenu;
        public InputAction @Select => m_Wrapper.m_UI_Select;
        public InputAction @Quit => m_Wrapper.m_UI_Quit;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @MoveDownMenu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveDownMenu;
                @MoveDownMenu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveDownMenu;
                @MoveDownMenu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveDownMenu;
                @MoveUpMenu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveUpMenu;
                @MoveUpMenu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveUpMenu;
                @MoveUpMenu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMoveUpMenu;
                @Select.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSelect;
                @Quit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveDownMenu.started += instance.OnMoveDownMenu;
                @MoveDownMenu.performed += instance.OnMoveDownMenu;
                @MoveDownMenu.canceled += instance.OnMoveDownMenu;
                @MoveUpMenu.started += instance.OnMoveUpMenu;
                @MoveUpMenu.performed += instance.OnMoveUpMenu;
                @MoveUpMenu.canceled += instance.OnMoveUpMenu;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnRotate(InputAction.CallbackContext context);
        void OnDrawBow(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnLStickDown(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnInteractable(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnMoveDownMenu(InputAction.CallbackContext context);
        void OnMoveUpMenu(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
    }
}
