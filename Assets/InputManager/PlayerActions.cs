//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/InputManager/PlayerActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerActions"",
    ""maps"": [
        {
            ""name"": ""Player_Map"",
            ""id"": ""32d2141e-9288-4451-ad3b-ecf4e60b800b"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""2adba034-396d-4bee-ad87-e1027feaca75"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Laser"",
                    ""type"": ""Button"",
                    ""id"": ""57364b6c-eea3-4ebe-b0cf-e5756eade62d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restart Game"",
                    ""type"": ""Button"",
                    ""id"": ""46024fbc-147c-4ba5-82be-8b63c4e522a5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause Game"",
                    ""type"": ""Button"",
                    ""id"": ""5c867739-a92a-42da-bcc2-93c868b2bae7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""4c7ad5b2-c56b-47a1-b754-9754b31092f3"",
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
                    ""id"": ""dc1edb8d-4d2a-4b7d-9e49-975f8edfd4cf"",
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
                    ""id"": ""72d3feec-c7f1-4092-91fb-25954ee84e1d"",
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
                    ""id"": ""f8c41d4b-c378-469f-9442-7b1869e7824d"",
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
                    ""id"": ""83b10768-b233-4d65-ab2b-86fe38296763"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a1582512-31dd-4562-8d14-ab40f122ff80"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Laser"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d71e8337-ef23-48f9-b579-5fc1fd2cd7e4"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9faadb5a-1313-4322-bdc8-aef334fb306b"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause Game"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player_Map
        m_Player_Map = asset.FindActionMap("Player_Map", throwIfNotFound: true);
        m_Player_Map_Movement = m_Player_Map.FindAction("Movement", throwIfNotFound: true);
        m_Player_Map_Laser = m_Player_Map.FindAction("Laser", throwIfNotFound: true);
        m_Player_Map_RestartGame = m_Player_Map.FindAction("Restart Game", throwIfNotFound: true);
        m_Player_Map_PauseGame = m_Player_Map.FindAction("Pause Game", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player_Map
    private readonly InputActionMap m_Player_Map;
    private List<IPlayer_MapActions> m_Player_MapActionsCallbackInterfaces = new List<IPlayer_MapActions>();
    private readonly InputAction m_Player_Map_Movement;
    private readonly InputAction m_Player_Map_Laser;
    private readonly InputAction m_Player_Map_RestartGame;
    private readonly InputAction m_Player_Map_PauseGame;
    public struct Player_MapActions
    {
        private @PlayerActions m_Wrapper;
        public Player_MapActions(@PlayerActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Map_Movement;
        public InputAction @Laser => m_Wrapper.m_Player_Map_Laser;
        public InputAction @RestartGame => m_Wrapper.m_Player_Map_RestartGame;
        public InputAction @PauseGame => m_Wrapper.m_Player_Map_PauseGame;
        public InputActionMap Get() { return m_Wrapper.m_Player_Map; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(Player_MapActions set) { return set.Get(); }
        public void AddCallbacks(IPlayer_MapActions instance)
        {
            if (instance == null || m_Wrapper.m_Player_MapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_Player_MapActionsCallbackInterfaces.Add(instance);
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @Laser.started += instance.OnLaser;
            @Laser.performed += instance.OnLaser;
            @Laser.canceled += instance.OnLaser;
            @RestartGame.started += instance.OnRestartGame;
            @RestartGame.performed += instance.OnRestartGame;
            @RestartGame.canceled += instance.OnRestartGame;
            @PauseGame.started += instance.OnPauseGame;
            @PauseGame.performed += instance.OnPauseGame;
            @PauseGame.canceled += instance.OnPauseGame;
        }

        private void UnregisterCallbacks(IPlayer_MapActions instance)
        {
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @Laser.started -= instance.OnLaser;
            @Laser.performed -= instance.OnLaser;
            @Laser.canceled -= instance.OnLaser;
            @RestartGame.started -= instance.OnRestartGame;
            @RestartGame.performed -= instance.OnRestartGame;
            @RestartGame.canceled -= instance.OnRestartGame;
            @PauseGame.started -= instance.OnPauseGame;
            @PauseGame.performed -= instance.OnPauseGame;
            @PauseGame.canceled -= instance.OnPauseGame;
        }

        public void RemoveCallbacks(IPlayer_MapActions instance)
        {
            if (m_Wrapper.m_Player_MapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayer_MapActions instance)
        {
            foreach (var item in m_Wrapper.m_Player_MapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_Player_MapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public Player_MapActions @Player_Map => new Player_MapActions(this);
    public interface IPlayer_MapActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnLaser(InputAction.CallbackContext context);
        void OnRestartGame(InputAction.CallbackContext context);
        void OnPauseGame(InputAction.CallbackContext context);
    }
}
