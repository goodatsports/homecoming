// GENERATED AUTOMATICALLY FROM 'Assets/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""2b523455-221f-456e-b21e-42d3a2df8477"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""c04941da-b6c7-4593-bbf2-01396dc4f404"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""53edbf2a-e82b-4bf8-8e74-bb48d83af820"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Swing"",
                    ""type"": ""Button"",
                    ""id"": ""b65c1306-d3c9-43e8-b89d-2f038f1fe27e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowPointer"",
                    ""type"": ""Button"",
                    ""id"": ""ed009a34-cbde-43c4-a6e2-ec31b9cf1126"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Toggle Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""7c7bb207-a997-4794-ae5d-264267829419"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ThrowRope"",
                    ""type"": ""Button"",
                    ""id"": ""6a0d3c2a-c124-425e-90e1-f1e91faac750"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""261ddbe8-8f6b-409e-8db7-f27fc7beed8a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e04ae61-b1ab-4233-b408-04198ebf8501"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""75b5085e-ede8-4118-88d0-69ee960d9f68"",
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
                    ""id"": ""5b7a9a48-de90-43c3-ac44-703a06f537f6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5943df1f-8b07-4429-a017-23a63a0d2576"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""93151bbd-348a-472b-a777-785d76daf076"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""4f943289-46f5-4cd3-9819-a489db5b69b7"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""49a60f29-9519-463d-8e28-97c1e885ee1b"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Swing"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05ab83f1-bceb-4f38-8441-8433d9dbb8ea"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ShowPointer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35f8b4cf-9108-424f-8f4a-7d8552400754"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Toggle Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3a2f08cf-93cf-4c1d-a4d9-2e224199cc25"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ThrowRope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Dialog"",
            ""id"": ""8545966f-1b41-4e61-a6ad-eaff86796744"",
            ""actions"": [
                {
                    ""name"": ""MoveCursor"",
                    ""type"": ""Button"",
                    ""id"": ""cfa28880-2bd3-492d-af12-18d1d59cfbbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""1afa2211-2826-4939-a3c8-2b6e1a414491"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""b4d89d6a-8d03-432d-ab4e-07a4b42b42ea"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""f5082dc9-5638-4f17-9e76-cb7d94502cb9"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""511130d7-94fc-4eaa-b425-d56ddd416a2d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8bc9b4df-4fce-4494-b928-2d23368f800a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""a750d97a-d8d0-4b33-bff3-928da7e8fafa"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""0782b277-5a22-4e2e-af50-c40a5b6a023b"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8115c58a-84ef-4e00-9d68-7d7976ab4071"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Shopping"",
            ""id"": ""d8a4c54a-fbeb-48ee-aa10-23811aa3b8e1"",
            ""actions"": [
                {
                    ""name"": ""MoveShopCursor"",
                    ""type"": ""Button"",
                    ""id"": ""5a3cf945-00eb-4cef-8d9c-94db9ded73e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""EndShopping"",
                    ""type"": ""Button"",
                    ""id"": ""5b61c2bc-1979-4909-afed-bd648e50a172"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ItemInteract"",
                    ""type"": ""Button"",
                    ""id"": ""db27e1bf-d96e-4ecb-8e9e-33dbe92bb549"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8f0fee6e-afe5-48e1-8ff2-50b1ff2dcad7"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveShopCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""f90e6248-4f0f-4487-b530-e55304608c20"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveShopCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""908bafb8-3945-4d11-bb74-59eb47fbb883"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveShopCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""74648998-7d9a-431d-af46-ffa1a33f5b56"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""EndShopping"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4c61ad3-697b-4bd4-9c7a-58231ba518fb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""ItemInteract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""c20999a8-8747-4f8f-9b9a-6b4503468096"",
            ""actions"": [
                {
                    ""name"": ""MoveInventoryCursor"",
                    ""type"": ""Button"",
                    ""id"": ""04ba6fae-9c76-444d-9726-d0f287732e20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""a43a5464-cfb2-4620-86e2-18fe82a60280"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveInventoryCursor"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""9fb8b557-2d27-4432-ac49-d6fcf02dea9f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveInventoryCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""37c70926-07a7-4511-90ce-b71307219a47"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""MoveInventoryCursor"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Use = m_Player.FindAction("Use", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Swing = m_Player.FindAction("Swing", throwIfNotFound: true);
        m_Player_ShowPointer = m_Player.FindAction("ShowPointer", throwIfNotFound: true);
        m_Player_ToggleInventory = m_Player.FindAction("Toggle Inventory", throwIfNotFound: true);
        m_Player_ThrowRope = m_Player.FindAction("ThrowRope", throwIfNotFound: true);
        // Dialog
        m_Dialog = asset.FindActionMap("Dialog", throwIfNotFound: true);
        m_Dialog_MoveCursor = m_Dialog.FindAction("MoveCursor", throwIfNotFound: true);
        m_Dialog_Confirm = m_Dialog.FindAction("Confirm", throwIfNotFound: true);
        // Shopping
        m_Shopping = asset.FindActionMap("Shopping", throwIfNotFound: true);
        m_Shopping_MoveShopCursor = m_Shopping.FindAction("MoveShopCursor", throwIfNotFound: true);
        m_Shopping_EndShopping = m_Shopping.FindAction("EndShopping", throwIfNotFound: true);
        m_Shopping_ItemInteract = m_Shopping.FindAction("ItemInteract", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_MoveInventoryCursor = m_Inventory.FindAction("MoveInventoryCursor", throwIfNotFound: true);
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
    private readonly InputAction m_Player_Use;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Swing;
    private readonly InputAction m_Player_ShowPointer;
    private readonly InputAction m_Player_ToggleInventory;
    private readonly InputAction m_Player_ThrowRope;
    public struct PlayerActions
    {
        private @InputMaster m_Wrapper;
        public PlayerActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_Player_Use;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Swing => m_Wrapper.m_Player_Swing;
        public InputAction @ShowPointer => m_Wrapper.m_Player_ShowPointer;
        public InputAction @ToggleInventory => m_Wrapper.m_Player_ToggleInventory;
        public InputAction @ThrowRope => m_Wrapper.m_Player_ThrowRope;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnUse;
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Swing.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwing;
                @Swing.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwing;
                @Swing.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSwing;
                @ShowPointer.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowPointer;
                @ShowPointer.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowPointer;
                @ShowPointer.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnShowPointer;
                @ToggleInventory.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInventory;
                @ToggleInventory.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnToggleInventory;
                @ThrowRope.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrowRope;
                @ThrowRope.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrowRope;
                @ThrowRope.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnThrowRope;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Swing.started += instance.OnSwing;
                @Swing.performed += instance.OnSwing;
                @Swing.canceled += instance.OnSwing;
                @ShowPointer.started += instance.OnShowPointer;
                @ShowPointer.performed += instance.OnShowPointer;
                @ShowPointer.canceled += instance.OnShowPointer;
                @ToggleInventory.started += instance.OnToggleInventory;
                @ToggleInventory.performed += instance.OnToggleInventory;
                @ToggleInventory.canceled += instance.OnToggleInventory;
                @ThrowRope.started += instance.OnThrowRope;
                @ThrowRope.performed += instance.OnThrowRope;
                @ThrowRope.canceled += instance.OnThrowRope;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Dialog
    private readonly InputActionMap m_Dialog;
    private IDialogActions m_DialogActionsCallbackInterface;
    private readonly InputAction m_Dialog_MoveCursor;
    private readonly InputAction m_Dialog_Confirm;
    public struct DialogActions
    {
        private @InputMaster m_Wrapper;
        public DialogActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCursor => m_Wrapper.m_Dialog_MoveCursor;
        public InputAction @Confirm => m_Wrapper.m_Dialog_Confirm;
        public InputActionMap Get() { return m_Wrapper.m_Dialog; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DialogActions set) { return set.Get(); }
        public void SetCallbacks(IDialogActions instance)
        {
            if (m_Wrapper.m_DialogActionsCallbackInterface != null)
            {
                @MoveCursor.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnMoveCursor;
                @MoveCursor.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnMoveCursor;
                @Confirm.started -= m_Wrapper.m_DialogActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_DialogActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_DialogActionsCallbackInterface.OnConfirm;
            }
            m_Wrapper.m_DialogActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCursor.started += instance.OnMoveCursor;
                @MoveCursor.performed += instance.OnMoveCursor;
                @MoveCursor.canceled += instance.OnMoveCursor;
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
            }
        }
    }
    public DialogActions @Dialog => new DialogActions(this);

    // Shopping
    private readonly InputActionMap m_Shopping;
    private IShoppingActions m_ShoppingActionsCallbackInterface;
    private readonly InputAction m_Shopping_MoveShopCursor;
    private readonly InputAction m_Shopping_EndShopping;
    private readonly InputAction m_Shopping_ItemInteract;
    public struct ShoppingActions
    {
        private @InputMaster m_Wrapper;
        public ShoppingActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveShopCursor => m_Wrapper.m_Shopping_MoveShopCursor;
        public InputAction @EndShopping => m_Wrapper.m_Shopping_EndShopping;
        public InputAction @ItemInteract => m_Wrapper.m_Shopping_ItemInteract;
        public InputActionMap Get() { return m_Wrapper.m_Shopping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ShoppingActions set) { return set.Get(); }
        public void SetCallbacks(IShoppingActions instance)
        {
            if (m_Wrapper.m_ShoppingActionsCallbackInterface != null)
            {
                @MoveShopCursor.started -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnMoveShopCursor;
                @MoveShopCursor.performed -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnMoveShopCursor;
                @MoveShopCursor.canceled -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnMoveShopCursor;
                @EndShopping.started -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnEndShopping;
                @EndShopping.performed -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnEndShopping;
                @EndShopping.canceled -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnEndShopping;
                @ItemInteract.started -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnItemInteract;
                @ItemInteract.performed -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnItemInteract;
                @ItemInteract.canceled -= m_Wrapper.m_ShoppingActionsCallbackInterface.OnItemInteract;
            }
            m_Wrapper.m_ShoppingActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveShopCursor.started += instance.OnMoveShopCursor;
                @MoveShopCursor.performed += instance.OnMoveShopCursor;
                @MoveShopCursor.canceled += instance.OnMoveShopCursor;
                @EndShopping.started += instance.OnEndShopping;
                @EndShopping.performed += instance.OnEndShopping;
                @EndShopping.canceled += instance.OnEndShopping;
                @ItemInteract.started += instance.OnItemInteract;
                @ItemInteract.performed += instance.OnItemInteract;
                @ItemInteract.canceled += instance.OnItemInteract;
            }
        }
    }
    public ShoppingActions @Shopping => new ShoppingActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_MoveInventoryCursor;
    public struct InventoryActions
    {
        private @InputMaster m_Wrapper;
        public InventoryActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveInventoryCursor => m_Wrapper.m_Inventory_MoveInventoryCursor;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @MoveInventoryCursor.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnMoveInventoryCursor;
                @MoveInventoryCursor.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnMoveInventoryCursor;
                @MoveInventoryCursor.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnMoveInventoryCursor;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveInventoryCursor.started += instance.OnMoveInventoryCursor;
                @MoveInventoryCursor.performed += instance.OnMoveInventoryCursor;
                @MoveInventoryCursor.canceled += instance.OnMoveInventoryCursor;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnUse(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnSwing(InputAction.CallbackContext context);
        void OnShowPointer(InputAction.CallbackContext context);
        void OnToggleInventory(InputAction.CallbackContext context);
        void OnThrowRope(InputAction.CallbackContext context);
    }
    public interface IDialogActions
    {
        void OnMoveCursor(InputAction.CallbackContext context);
        void OnConfirm(InputAction.CallbackContext context);
    }
    public interface IShoppingActions
    {
        void OnMoveShopCursor(InputAction.CallbackContext context);
        void OnEndShopping(InputAction.CallbackContext context);
        void OnItemInteract(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnMoveInventoryCursor(InputAction.CallbackContext context);
    }
}
