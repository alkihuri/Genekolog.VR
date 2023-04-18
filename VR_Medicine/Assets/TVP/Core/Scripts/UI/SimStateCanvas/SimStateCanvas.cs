using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common.UI;
using Common.DataManager;

namespace TVP.UI
{


    public class SimStateCanvas : MonoSinglethon<SimStateCanvas>
    {

        [SerializeField] QuestionBoxController _questionBoxController;
        [SerializeField] GameObject _msgBox;
        [SerializeField] GameObject _progresBar;
        [SerializeField] GameObject _startButton;
        [SerializeField] GameObject _passedAtTime;



        [SerializeField] TMPro.TextMeshProUGUI _fullDes;
        [SerializeField] TMPro.TextMeshProUGUI _leveltitle;
        [SerializeField] TMPro.TextMeshProUGUI _des;
        [SerializeField] TMPro.TextMeshProUGUI _dataManagerDebug;
        [SerializeField] Image _percentImage;

        [SerializeField] private List<GameObject> circles = new List<GameObject>();
        private SimDomenState _thisState;

        public TextMeshProUGUI FullDes { get => _fullDes; set => _fullDes = value; }
        public TextMeshProUGUI Leveltitle { get => _leveltitle; set => _leveltitle = value; }
        public TextMeshProUGUI Des { get => _des; set => _des = value; }
        public QuestionBoxController QuestionBoxController { get => _questionBoxController; set => _questionBoxController = value; }

        public void NewSceneConfig()
        {
            _progresBar.SetActive(false);
            _msgBox.SetActive(true);
            _startButton.SetActive(true);
        }
        public void RegularSceneConfig()
        {
            _progresBar.SetActive(true);
            _msgBox.SetActive(false);
            _startButton.SetActive(false);
        }


        public void GoHome()
        {
            SimSceneManager.CurrentSimulation.LoadSelectorScene();
        }

        public void UpdateUI(SimDomenState state)
        {
            if(SimulationDataManager.GetLast()!=null)
                _dataManagerDebug.text = SimulationDataManager.GetLast().GetText();

            _passedAtTime.SetActive(state.IsDoneAtTime);

            var completePercent = (1.0f / state.TotalSteps) * (state.CurrentStep);
            _thisState = state;

            _percentImage.fillAmount = completePercent;
            CirclesInnit(state.TotalSteps, state.CurrentStep);


            if (state.FullDesription != "")
                _fullDes.text = state.FullDesription;

            Des.text = (state.CurrentStep+1) + ". " + state.Description;
            _leveltitle.text = state.LevelTittle;

        }

        public void CirclesInnit(int amount, int completed)
        {


            for (int x = 0; x < circles.Count; x++)
            {
                circles[x].gameObject.SetActive(false);
            }

            for (int x = 0; x < circles.Count; x++)
            {
                if (x < amount)
                {
                    circles[x].gameObject.SetActive(true);
                    circles[x].GetComponent<CircleProgressController>().Innit(x);
                }
            }

            for (int x = 0; x < circles.Count; x++)
            {
                if (x < completed)
                {
                    circles[x].GetComponent<CircleProgressController>().Done();
                }
                if (x == completed)
                {
                    circles[x].GetComponent<CircleProgressController>().Light();
                }
            }
        }

        [ContextMenu("Test 1")]
        public void Test1()
        {
            _percentImage.fillAmount = 6f / 6f;
            CirclesInnit(6, 6);
        }
        [ContextMenu("Test 2")]
        public void Test2()
        {
            _percentImage.fillAmount = 2f / 0f;
            CirclesInnit(2, 0);
        }
    }
}