﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Dreamers.Global
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager instance;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            if (instance == null)
                instance = this;
            if (instance != this)
                Destroy(this);
        }

        private static GameObject _uICanvas;
       
        public  GameObject UICanvas()
        {
            GameObject Instance;
            if (!_uICanvas)
            {
                Instance = new GameObject();
                Instance.name = "Canvas";
                GameObject EventInstance = new GameObject();
                EventInstance.name = "Event System";
                Instance.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
                EventInstance.AddComponent<StandaloneInputModule>();
                CanvasScaler scaler = Instance.AddComponent<CanvasScaler>();
                scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                scaler.referenceResolution = new Vector2(1920, 1080);
                scaler.matchWidthOrHeight = 0.0f;
                scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
                Instance.AddComponent<GraphicRaycaster>();
                _uICanvas = Instance;
            }
            else
                Instance = _uICanvas;
            return Instance;
        }

        [SerializeField] private GameObject UIPanelPrefab;
        [SerializeField] private GameObject TextBoxPrefab;
        [SerializeField] private GameObject Button;
        // write anchoring system

        public GameObject Panel(Transform Parent, Vector2 Size, Vector2 Position) 
        {
            GameObject temp = Instantiate(UIPanelPrefab);
            temp.transform.SetParent(Parent);
            RectTransform PanelRect = temp.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(0, 1);
            PanelRect.anchorMin = new Vector2(0, 1);
            PanelRect.sizeDelta = Size;
            PanelRect.anchoredPosition = Position;

            return temp;
        }
        public GameObject TextBox(Transform Parent, Vector2 Size) {

            GameObject temp = Instantiate(TextBoxPrefab);
            temp.transform.SetParent(Parent);
            RectTransform PanelRect = temp.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(0, 1);
            PanelRect.anchorMin = new Vector2(0, 1);
            PanelRect.sizeDelta = Size;
            return temp;
        }
        public GameObject UIButton(Transform Parent)
        {
            GameObject temp = Instantiate(Button);
            temp.transform.SetParent(Parent);
            RectTransform PanelRect = temp.GetComponent<RectTransform>();
            PanelRect.pivot = new Vector2(0.5f, .5f);
            PanelRect.anchorMax = new Vector2(0, 1);
            PanelRect.anchorMin = new Vector2(0, 1);

            return temp;
        }

    }
}
