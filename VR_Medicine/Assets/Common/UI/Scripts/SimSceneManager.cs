using System.Collections;
using System.Collections.Generic;
using TVP;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Common.UI
{
    public class SimSceneManager : MonoSinglethon<SimSceneManager>
    {
        private const string TVP_SCENE_NAME = "TVP.Sim";
        private const string ICSI_SCENE_NAME = "ICSI-Scene";
        private const string SELECTOR_SCENE_NAME = "ModeSelectScene";
        private const string SHOW_RESULT_SCENE_NAME = "ShowResult";
        private const string REGISTRATION_SCENE_NAME = "RegistrationScene";
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }

        public void LoadTVP()
        {
            LoadScene(TVP_SCENE_NAME);
        }

        public void LoadICSI()
        {
            LoadScene(ICSI_SCENE_NAME);
        }
        public void LoadRegistration()
        {
            LoadScene(REGISTRATION_SCENE_NAME);
        }
        public void LoadShowResult()
        {
            LoadScene(SHOW_RESULT_SCENE_NAME);
        }
        public void LoadSelectorScene()
        {
            LoadScene(SELECTOR_SCENE_NAME);
        }
    }
}