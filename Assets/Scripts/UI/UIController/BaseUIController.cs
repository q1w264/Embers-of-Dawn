using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;

namespace UI.UIController
{
    /// <summary>
    /// Base runtime UI Toolkit controller with open/close visibility and focus bootstrap.
    /// </summary>
    [RequireComponent(typeof(UIDocument))]
    public abstract class BaseUIController : MonoBehaviour
    {
        private UIDocument _document;
        private Coroutine _initialFocusRoutine;
        private InputSystemUIInputModule _cachedInputModule;
        private GameObject _cachedSelectionTarget;
        protected VisualElement Root { get; private set; }

        /// <summary>
        /// Manager that controls stack-based UI flow for this controller.
        /// </summary>
        public UIManager UIManager { get; set; }

        /// <summary>
        /// Indicates whether this controller is currently visible.
        /// </summary>
        public bool IsOpen { get; private set; }

        private VisualElement FirstFocusedElement { get; set; }

        protected virtual void Awake()
        {
            _document = GetComponent<UIDocument>();
            Root = _document.rootVisualElement;
            
            Close();
        }
        
        /// <summary>
        /// Opens the UI root and initializes input focus.
        /// </summary>
        public void Open()
        {
            IsOpen = true;
            if (Root != null)
            {
                Root.style.display = DisplayStyle.Flex;
            }

            StartInitialFocusRoutine();
        }
        
        /// <summary>
        /// Closes the UI root and stops focus initialization routine.
        /// </summary>
        public void Close()
        {
            IsOpen = false;
            if (_initialFocusRoutine != null)
            {
                StopCoroutine(_initialFocusRoutine);
                _initialFocusRoutine = null;
            }
            if (Root != null)
            {
                Root.style.display = DisplayStyle.None;
            }
        }

        private void FocusFirst()
        {
            if (FirstFocusedElement == null) return;
            if (!FirstFocusedElement.focusable) return;
            FirstFocusedElement.Focus();
        }

        /// <summary>
        /// Sets the first element to focus when this UI opens.
        /// </summary>
        /// <param name="element">Focusable element to prioritize.</param>
        protected void FocusFirst(VisualElement element)
        {
            FirstFocusedElement = element;
            if (IsOpen)
            {
                FocusFirst();
            }
        }

        private void StartInitialFocusRoutine()
        {
            if (_initialFocusRoutine != null)
            {
                StopCoroutine(_initialFocusRoutine);
            }

            _initialFocusRoutine = StartCoroutine(InitialFocusRoutine());
        }

        private IEnumerator InitialFocusRoutine()
        {
            for (var i = 0; i < 6 && IsOpen; i++)
            {
                EnsureUiInputReady();
                FocusFirst();
                yield return null;
            }

            _initialFocusRoutine = null;
        }

        private void EnsureUiInputReady()
        {
            var eventSystem = GetOrCreateEventSystem();
            if (eventSystem == null) return;

            eventSystem.sendNavigationEvents = true;
            if (!eventSystem.enabled)
            {
                eventSystem.enabled = true;
            }

            if (_cachedInputModule == null || _cachedInputModule.gameObject != eventSystem.gameObject)
            {
                if (eventSystem.currentInputModule is InputSystemUIInputModule currentInputModule)
                {
                    _cachedInputModule = currentInputModule;
                }
                else
                {
                    _cachedInputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
                    if (_cachedInputModule == null)
                    {
                        _cachedInputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
                    }
                }
            }

            if (_cachedInputModule == null) return;
            if (!_cachedInputModule.enabled)
            {
                _cachedInputModule.enabled = true;
            }

            if (_cachedInputModule.actionsAsset == null)
            {
                _cachedInputModule.AssignDefaultActions();
            }
            _cachedInputModule.actionsAsset?.Enable();

            var selectedTarget = ResolveSelectionTarget(eventSystem);
            if (selectedTarget == null) return;

            if (eventSystem.currentSelectedGameObject == selectedTarget) return;
            eventSystem.SetSelectedGameObject(null);
            eventSystem.SetSelectedGameObject(selectedTarget);
        }

        private static EventSystem GetOrCreateEventSystem()
        {
            var eventSystem = EventSystem.current;
            if (eventSystem != null) return eventSystem;

            var eventSystemObject = new GameObject("EventSystem");
            eventSystem = eventSystemObject.AddComponent<EventSystem>();
            eventSystemObject.AddComponent<InputSystemUIInputModule>();
            return eventSystem;
        }

        private GameObject ResolveSelectionTarget(EventSystem eventSystem)
        {
            if (_cachedSelectionTarget != null)
            {
                return _cachedSelectionTarget;
            }

            var panelEventHandlers = eventSystem.GetComponentsInChildren<PanelEventHandler>(true);
            if (panelEventHandlers.Length > 0)
            {
                if (_document != null && _document.panelSettings != null)
                {
                    var panelSettingsName = _document.panelSettings.name;
                    foreach (var handler in panelEventHandlers)
                    {
                        if (handler.gameObject.name == panelSettingsName)
                        {
                            _cachedSelectionTarget = handler.gameObject;
                            return _cachedSelectionTarget;
                        }
                    }
                }

                _cachedSelectionTarget = panelEventHandlers[0].gameObject;
                return _cachedSelectionTarget;
            }

            var panelEventHandler = GetComponent<PanelEventHandler>() ?? GetComponentInChildren<PanelEventHandler>(true);
            if (panelEventHandler != null)
            {
                _cachedSelectionTarget = panelEventHandler.gameObject;
                return _cachedSelectionTarget;
            }

            return null;
        }

    }
}