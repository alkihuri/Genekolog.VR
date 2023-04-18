using Common;
using Common.DataManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.UI
{
    public class UIElementsVisibilityController : MonoBehaviour
    {
        [SerializeField] List<GameObject> _uiElements = new List<GameObject>();

        public void UpdateVisibility(GameObject element)
        {
            element.SetActive(!SimulationManagerData.IsTest); 
        }


        private void Start() => UpdateCanvas();

        [ContextMenu("Update visibility")]
        public  void UpdateCanvas()
        {
            foreach (GameObject element in _uiElements) { UpdateVisibility(element); }
        }
    }
}