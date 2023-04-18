using Autohand;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TVP
{
    public class TVPTutorialAdapter : MonoSinglethon<TVPTutorialAdapter>
    {
        [SerializeField] TutorialController _tutorialControlelr;

        [SerializeField] HandControllerTutorialData _right;
        [SerializeField] HandControllerTutorialData _left;


        [SerializeField] Grabbable _niddle;



        public TutorialController TutorialControlelr { get => _tutorialControlelr; set => _tutorialControlelr = value; }

        private void Awake()
        {
            TutorialControlelr = GameObject.FindAnyObjectByType<TutorialController>();
        }
        public void ShowNiddleSelectTutorial()
        {
            _right.PrimaryButton.StartTutorial();
        }

        public void DispenceAspirateTutorial()
        {

            _right.PrimaryButton.StartTutorial();
            _right.SecondButton.StartTutorial();
        }
    }
}