// GENERATED AUTOMATICALLY FROM 'Assets/InputSettings/PlayerControls.inputactions'

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
            ""name"": ""PlayerKeyboard"",
            ""id"": ""b73ad74f-3068-40cc-9f54-292317033894"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""3819fc1a-4eaf-4aca-89cf-e88ece8c48a4"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""532731d0-3426-4181-afe7-ce88f170c793"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SpellHotkey1"",
                    ""type"": ""Button"",
                    ""id"": ""934aeb1b-c1fa-4f1b-81ed-207760211fae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""652bdb5e-43bf-4b36-b2bc-3c87408232eb"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""7eea7673-9c0f-4e44-ba3e-facec411df6d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""1ecc6fcd-7881-4361-904a-f522d5335ebe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""6a7a2bad-d2c6-46d3-8e7a-dcba324cd806"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7ce7e2f6-91f9-4693-a1f2-54126e502899"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bdafaa6d-2a9e-4ec1-be14-0d1e1e5c6d49"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b497e4ec-ee8d-44c9-a392-669549b76f79"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SpellHotkey1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerKeyboard
        m_PlayerKeyboard = asset.FindActionMap("PlayerKeyboard", throwIfNotFound: true);
        m_PlayerKeyboard_Move = m_PlayerKeyboard.FindAction("Move", throwIfNotFound: true);
        m_PlayerKeyboard_LeftClick = m_PlayerKeyboard.FindAction("LeftClick", throwIfNotFound: true);
        m_PlayerKeyboard_SpellHotkey1 = m_PlayerKeyboard.FindAction("SpellHotkey1", throwIfNotFound: true);
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

    // PlayerKeyboard
    private readonly InputActionMap m_PlayerKeyboard;
    private IPlayerKeyboardActions m_PlayerKeyboardActionsCallbackInterface;
    private readonly InputAction m_PlayerKeyboard_Move;
    private readonly InputAction m_PlayerKeyboard_LeftClick;
    private readonly InputAction m_PlayerKeyboard_SpellHotkey1;
    public struct PlayerKeyboardActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerKeyboardActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_PlayerKeyboard_Move;
        public InputAction @LeftClick => m_Wrapper.m_PlayerKeyboard_LeftClick;
        public InputAction @SpellHotkey1 => m_Wrapper.m_PlayerKeyboard_SpellHotkey1;
        public InputActionMap Get() { return m_Wrapper.m_PlayerKeyboard; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerKeyboardActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerKeyboardActions instance)
        {
            if (m_Wrapper.m_PlayerKeyboardActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnMove;
                @LeftClick.started -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnLeftClick;
                @LeftClick.performed -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnLeftClick;
                @LeftClick.canceled -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnLeftClick;
                @SpellHotkey1.started -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnSpellHotkey1;
                @SpellHotkey1.performed -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnSpellHotkey1;
                @SpellHotkey1.canceled -= m_Wrapper.m_PlayerKeyboardActionsCallbackInterface.OnSpellHotkey1;
            }
            m_Wrapper.m_PlayerKeyboardActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @LeftClick.started += instance.OnLeftClick;
                @LeftClick.performed += instance.OnLeftClick;
                @LeftClick.canceled += instance.OnLeftClick;
                @SpellHotkey1.started += instance.OnSpellHotkey1;
                @SpellHotkey1.performed += instance.OnSpellHotkey1;
                @SpellHotkey1.canceled += instance.OnSpellHotkey1;
            }
        }
    }
    public PlayerKeyboardActions @PlayerKeyboard => new PlayerKeyboardActions(this);
    public interface IPlayerKeyboardActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLeftClick(InputAction.CallbackContext context);
        void OnSpellHotkey1(InputAction.CallbackContext context);
    }
}
