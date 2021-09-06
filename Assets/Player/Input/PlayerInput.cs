// GENERATED AUTOMATICALLY FROM 'Assets/Player/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""8a9b5012-2b44-43f3-8713-911f1bc414bf"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""33c1fbe5-29f3-40a5-bef6-608464dafefe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5c807685-28fc-4adc-9f64-90bd6645b139"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dive"",
                    ""type"": ""Button"",
                    ""id"": ""164e1b71-eb45-46ab-bda1-a183b07487da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Slash"",
                    ""type"": ""Button"",
                    ""id"": ""afbd3870-040a-4175-8827-6f8b03548fd9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Strike"",
                    ""type"": ""Button"",
                    ""id"": ""78b502d4-ca10-4c19-92aa-a6d7474eeade"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e41bcaa7-3e2f-4733-a39a-4101c0f3343c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Shield"",
                    ""type"": ""Button"",
                    ""id"": ""1015339d-7fda-4a05-8427-79aaf7c8f5a3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Shift"",
                    ""type"": ""Button"",
                    ""id"": ""da4cbb9e-1b93-4a59-b840-527f173bf917"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Heal"",
                    ""type"": ""Button"",
                    ""id"": ""12a336bf-c4c3-4000-9793-f366f822b1bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Keyboard Primary"",
                    ""id"": ""f2d3d97d-ae08-4095-91cf-2f5a2a3e1548"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Secondary"",
                    ""id"": ""929770a8-b178-4f87-adb7-1d976fe8860e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""a751acab-e4f4-4616-962f-500eb77d0ab2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9acb99ef-8cb9-43a0-a3f8-77a2be3716d4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8c17cdeb-6dd1-4d04-8c09-251151c89d48"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""77d34bbf-6599-4c8e-86fc-3537e9c8fc12"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""5655ce6e-b7b7-47b5-a467-9ee71d903e33"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""e6d38359-8681-4e15-aeda-7bc273963d02"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""f6a22847-3253-49dc-b45c-8f4754f940a7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""f5f7fd04-c455-47d7-8312-ec7754a713c5"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7d4a1410-9bdd-49e0-aa29-7f7de336d968"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2b5ed9f7-5487-46fd-a700-73302871f8b8"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8cfe3b37-2c9b-4602-a6e5-174dc6cb9086"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""99b5a4da-65fc-40fd-b820-13225c4ec9ff"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8f43e648-7f12-4256-9523-86a581e63d8f"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e44cf596-975a-4139-b5d5-b2db7fe86298"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Dive"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c19ae53-a383-43d8-aaa8-723a060415c2"",
                    ""path"": ""<Keyboard>/u"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Slash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7bcc37e8-3aff-40c5-8521-ae3ac720d80f"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Slash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f51199c7-5037-4ee8-9ef1-10a6c50b182f"",
                    ""path"": ""<Keyboard>/j"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c9925f4-88a5-493e-a860-d7fecba31f84"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Strike"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a3d9e35-ee21-45d0-aaef-18e0b3e3f637"",
                    ""path"": ""<Keyboard>/k"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8058d09c-e5b1-48c0-a9ad-570eaeeae816"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""daacdf58-7cfc-48dc-b60c-9c5738859535"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e269916-8d73-4fa8-92bf-79c4b60c6ee4"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Shield"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d04a0501-3797-4961-aa30-9327fe5e5230"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4865a97c-751c-4e3a-95ec-598bb5bb2dc6"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Shift"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""59772fd3-bcfd-43d3-a62a-3a0b7445bad5"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31a610ab-e10a-42c8-8f0b-451eb7882f11"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Heal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""General"",
            ""id"": ""ba22839b-064d-41fb-9a12-3cab134591dd"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""9fbb1b88-48ef-4d1c-aa35-970e372f1414"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f868cf7f-ce81-4255-a4e7-aa0b3f393e73"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e88c21a-5635-4257-8b54-c4cd70d42bdb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        },
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": []
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Move = m_Player.FindAction("Move", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_Dive = m_Player.FindAction("Dive", throwIfNotFound: true);
        m_Player_Slash = m_Player.FindAction("Slash", throwIfNotFound: true);
        m_Player_Strike = m_Player.FindAction("Strike", throwIfNotFound: true);
        m_Player_Dash = m_Player.FindAction("Dash", throwIfNotFound: true);
        m_Player_Shield = m_Player.FindAction("Shield", throwIfNotFound: true);
        m_Player_Shift = m_Player.FindAction("Shift", throwIfNotFound: true);
        m_Player_Heal = m_Player.FindAction("Heal", throwIfNotFound: true);
        // General
        m_General = asset.FindActionMap("General", throwIfNotFound: true);
        m_General_Interact = m_General.FindAction("Interact", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Move;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_Dive;
    private readonly InputAction m_Player_Slash;
    private readonly InputAction m_Player_Strike;
    private readonly InputAction m_Player_Dash;
    private readonly InputAction m_Player_Shield;
    private readonly InputAction m_Player_Shift;
    private readonly InputAction m_Player_Heal;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Player_Move;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @Dive => m_Wrapper.m_Player_Dive;
        public InputAction @Slash => m_Wrapper.m_Player_Slash;
        public InputAction @Strike => m_Wrapper.m_Player_Strike;
        public InputAction @Dash => m_Wrapper.m_Player_Dash;
        public InputAction @Shield => m_Wrapper.m_Player_Shield;
        public InputAction @Shift => m_Wrapper.m_Player_Shift;
        public InputAction @Heal => m_Wrapper.m_Player_Heal;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMove;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Dive.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDive;
                @Dive.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDive;
                @Dive.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDive;
                @Slash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlash;
                @Slash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlash;
                @Slash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSlash;
                @Strike.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStrike;
                @Strike.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStrike;
                @Strike.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnStrike;
                @Dash.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnDash;
                @Shield.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShield;
                @Shield.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShield;
                @Shield.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShield;
                @Shift.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShift;
                @Shift.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShift;
                @Shift.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShift;
                @Heal.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @Heal.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
                @Heal.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHeal;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Dive.started += instance.OnDive;
                @Dive.performed += instance.OnDive;
                @Dive.canceled += instance.OnDive;
                @Slash.started += instance.OnSlash;
                @Slash.performed += instance.OnSlash;
                @Slash.canceled += instance.OnSlash;
                @Strike.started += instance.OnStrike;
                @Strike.performed += instance.OnStrike;
                @Strike.canceled += instance.OnStrike;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Shield.started += instance.OnShield;
                @Shield.performed += instance.OnShield;
                @Shield.canceled += instance.OnShield;
                @Shift.started += instance.OnShift;
                @Shift.performed += instance.OnShift;
                @Shift.canceled += instance.OnShift;
                @Heal.started += instance.OnHeal;
                @Heal.performed += instance.OnHeal;
                @Heal.canceled += instance.OnHeal;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // General
    private readonly InputActionMap m_General;
    private IGeneralActions m_GeneralActionsCallbackInterface;
    private readonly InputAction m_General_Interact;
    public struct GeneralActions
    {
        private @PlayerInput m_Wrapper;
        public GeneralActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_General_Interact;
        public InputActionMap Get() { return m_Wrapper.m_General; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GeneralActions set) { return set.Get(); }
        public void SetCallbacks(IGeneralActions instance)
        {
            if (m_Wrapper.m_GeneralActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_GeneralActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_GeneralActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public GeneralActions @General => new GeneralActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnDive(InputAction.CallbackContext context);
        void OnSlash(InputAction.CallbackContext context);
        void OnStrike(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnShield(InputAction.CallbackContext context);
        void OnShift(InputAction.CallbackContext context);
        void OnHeal(InputAction.CallbackContext context);
    }
    public interface IGeneralActions
    {
        void OnInteract(InputAction.CallbackContext context);
    }
}
