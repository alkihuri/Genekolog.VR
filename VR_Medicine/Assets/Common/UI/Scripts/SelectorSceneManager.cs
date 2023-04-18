using Common.DataManager;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine;


namespace Common.UI
{
    public class SelectorSceneManager : MonoBehaviour
    {
        [SerializeField] GameObject _icsiMenu;
        [SerializeField] GameObject _tvpMenu;

        private void Start()
        {
            _icsiMenu.SetActive(false);
            _tvpMenu.SetActive(false);
        }

        public void ICSIBtnClicked()
        {
            _tvpMenu.SetActive(false);
            var nextState = !_icsiMenu.activeInHierarchy && !_tvpMenu.activeInHierarchy;
            _icsiMenu.SetActive(nextState);
        }
        public void TVPBtnClicked()
        {
            _icsiMenu.SetActive(false);
            var nextState = !_icsiMenu.activeInHierarchy && !_tvpMenu.activeInHierarchy;
            _tvpMenu.SetActive(nextState);
        }


        public void TVPSceneSelected(bool isTest)
        {
            SimulationManagerData.IsTest = isTest;
            SimSceneManager.CurrentSimulation.LoadTVP(); 
        }
        public void ICSIceneSelected(bool isTest)
        {
            SimulationManagerData.IsTest = isTest;
            SimSceneManager.CurrentSimulation.LoadICSI();
        }
    }
}