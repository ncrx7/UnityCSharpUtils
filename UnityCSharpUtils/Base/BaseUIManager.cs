using System;
using System.Collections.Generic;
using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityUtils.BaseClasses
{
    public class BaseUIManager<TType, TData> : MonoBehaviour where TType : Enum
    {
        [Header("Panels")]
        [SerializeField] private List<BasePanel<TType, TData>> _mainPanels;
        protected Dictionary<TType, BasePanel<TType, TData>> _mainPanelMap = new();

        protected Dictionary<UIActionType, object> _uiActionMap = new();

        protected virtual void Awake()
        {
            InitializeBaseUIAction();
            InitializeMainPanelMap();
        }

        private void InitializeBaseUIAction()
        {
            AddUIActionToMap<bool, GameObject>(UIActionType.SetPanelVisibility, (active, panel) => panel.SetActive(active));
            AddUIActionToMap<string, TextMeshProUGUI>(UIActionType.SetText, (textString, textObject) => textObject.text = textString);
            AddUIActionToMap<Slider, float>(UIActionType.SetSlider, (slider, value) => slider.value = value);
            AddUIActionToMap<Image, Sprite>(UIActionType.SetImage, (image, sprite) => image.sprite = sprite);
        }

        private void InitializeMainPanelMap()
        {
            if(_mainPanels == null || _mainPanels.Count == 0)
                return;
            
            foreach (var mainPanel in _mainPanels)
            {
                _mainPanelMap[mainPanel.PanelType] = mainPanel;
                Debug.Log("main panel name: " + _mainPanelMap[mainPanel.PanelType].gameObject.name);
            }
        }

        protected void AddUIActionToMap<T>(UIActionType actionType, Action<T> action)
        {
            _uiActionMap[actionType] = action;
        }

        protected void AddUIActionToMap<T1, T2>(UIActionType actionType, Action<T1, T2> action)
        {
            _uiActionMap[actionType] = action;
        }

        protected void ExecuteUIAction<T>(UIActionType actionType, T value)
        {
            if (_uiActionMap.TryGetValue(actionType, out var action) && action is Action<T> typedAction)
            {
                typedAction(value);
            }
            else
            {
                Debug.LogWarning("Undefined action Type!!");
            }
        }

        protected void ExecuteUIAction<T1, T2>(UIActionType actionType, T1 value1, T2 value2)
        {
            if (_uiActionMap.TryGetValue(actionType, out var action) && action is Action<T1, T2> typedAction)
            {
                typedAction?.Invoke(value1, value2);
            }
            else
            {
                Debug.LogWarning("Undefined action Type!!");
            }
        }

        protected bool TryGetPanel<TPanel>(TType type, out TPanel result) where TPanel : BasePanel<TType, TData>
        {
            if (_mainPanelMap.TryGetValue(type, out var basePanel))
            {
                result = basePanel as TPanel;
                return result != null;
            }

            result = null;
            return false;
        }

        protected TPanel GetPanel<TPanel>(TType type) where TPanel : BasePanel<TType,TData>
        {
            return _mainPanelMap[type] as TPanel;
        }
    }
}

public enum UIActionType
{
    SetPanelVisibility,
    SetText,
    SetImage,
    SetSlider
}
