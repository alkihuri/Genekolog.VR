using System;
using System.Collections;
using System.Collections.Generic;
using TVP.UI;
using UnityEngine;

namespace TVP
{

    public class SimDomenStateMachine : MonoSinglethon<SimDomenStateMachine>
    {
        private const float TOTAL_STEPS = 9.0f;
        private SimDomenState _currentState;

        [SerializeField] private List<StateTypeEnum> _correctSequence = new List<StateTypeEnum>();


        [SerializeField] TimeMachine _timeMachine;
        [Header("Debug:")]
        public StateTypeEnum _stateTypeForDebug;
        [SerializeField] List<StateTypeEnum> _passedItems = new List<StateTypeEnum>();
        [SerializeField, Range(1, 100)] float _totalPassedStates = 0;
        [SerializeField, Range(0, 100)] private float _completePercent;

        public int CurrentState;

        public float CompletePercent
        {
            get => _completePercent;
            set
            {
                _completePercent = value;
            }
        }

        public float TotalPassedStates { get => _totalPassedStates; set => _totalPassedStates = value; }
        public TimeMachine TimeMachine { get => _timeMachine; set => _timeMachine = value; }
        public bool IsOnTransition { get; private set; }
        public List<StateTypeEnum> CorrectSequence { get => _correctSequence; set => _correctSequence = value; }

        private void Awake()
        {

            TimeMachine = _timeMachine == null ? GetComponent<TimeMachine>() : _timeMachine;

            TimeMachine.OnStateNotFinishedAtTime.AddListener(TimeIsGone);

            CorrectSequenceInnit();
        }

        private void TimeIsGone()
        {
            SimStateCanvas
                .CurrentSimulation
                    .QuestionBoxController
                        .Innit("Вы не успели " + _currentState.LevelTittle, ResetCurrentScene, null, "Еще раз", "Продолжить");
        }

        private void CorrectSequenceInnit()
        {
            CorrectSequence.Add(StateTypeEnum.SimIsStart);

            #region scene_06_01

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_06_01);
            CorrectSequence.Add(StateTypeEnum.UziOsOn_06_01_01);
            CorrectSequence.Add(StateTypeEnum.UziDispalySettedUp_06_01_02);
            CorrectSequence.Add(StateTypeEnum.PompIsOn_06_01_03);
            CorrectSequence.Add(StateTypeEnum.PressuarePompLevelCheck_06_01_04);
            CorrectSequence.Add(StateTypeEnum.PompPedalIsWorking_06_01_05);
            CorrectSequence.Add(StateTypeEnum.PompIsWorking_06_01_06);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_06_01);

            #endregion scene_06_01

            #region scene_06_02

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_06_02);
            CorrectSequence.Add(StateTypeEnum.HandsIsProccesed_06_02_01);
            CorrectSequence.Add(StateTypeEnum.VaginaIsProccesed_06_02_02);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_06_02);

            #endregion scene_06_02


            #region scene_06_03

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_06_03);
            CorrectSequence.Add(StateTypeEnum.GelIsApplied_06_03_01);
            CorrectSequence.Add(StateTypeEnum.PrezervativIsApplied_06_03_02);
            CorrectSequence.Add(StateTypeEnum.BiopsyAdapterIsMounted_06_03_03);
            CorrectSequence.Add(StateTypeEnum.VaginaPenetreted_06_03_04);
            CorrectSequence.Add(StateTypeEnum.UziEmulation_06_03_05);
            CorrectSequence.Add(StateTypeEnum.SimpleUziInvestigation_06_03_06);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_06_03);

            #endregion scene_06_03 

            #region scene_06_04

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_06_04);
            CorrectSequence.Add(StateTypeEnum.NiddleISSelected_06_04_01);
            CorrectSequence.Add(StateTypeEnum.AspiratingTestIsDone_06_04_02);
            CorrectSequence.Add(StateTypeEnum.NiddleIsMountedToAdapter_06_04_03);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_06_04);

            #endregion scene_06_04


            #region scene_07_01

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_07_01);
            CorrectSequence.Add(StateTypeEnum.SurgeryIsStart_07_01_01);
            CorrectSequence.Add(StateTypeEnum.AspiratingIsDone_07_01_02);
            CorrectSequence.Add(StateTypeEnum.SurgeryIsDone_07_01_03);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_07_01);

            #endregion scene_07_01

            #region scene_08_01

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_08_01);
            CorrectSequence.Add(StateTypeEnum.DontReleasePedal_08_01_01);
            CorrectSequence.Add(StateTypeEnum.DeattachNiddleFromAdapter_08_01_02);
            CorrectSequence.Add(StateTypeEnum.PutNiddleAtAntisepticFluid_08_01_03);
            CorrectSequence.Add(StateTypeEnum.WashNiddle_08_01_04);
            CorrectSequence.Add(StateTypeEnum.PassNiddleToHelper_08_01_05);
            CorrectSequence.Add(StateTypeEnum.DeattachAdapterFromDevice_08_01_06);
            CorrectSequence.Add(StateTypeEnum.PassAdapterToHelper_08_01_07);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_08_01);

            #endregion

            #region scene_08_02

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_08_02);
            CorrectSequence.Add(StateTypeEnum.LastUziInvestigation_08_02_01);
            CorrectSequence.Add(StateTypeEnum.PrintResultOfSurgery_08_02_02);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_08_02);

            #endregion

            #region scene_08_03

            CorrectSequence.Add(StateTypeEnum.SceneIsStart_08_03);
            CorrectSequence.Add(StateTypeEnum.lastProccesVagina_08_03_01);
            CorrectSequence.Add(StateTypeEnum.GenMirrorProccess_08_03_02);
            CorrectSequence.Add(StateTypeEnum.GenZerkaloOff_08_03_03);
            CorrectSequence.Add(StateTypeEnum.SceneIsDone_08_03);


            #endregion

            CorrectSequence.Add(StateTypeEnum.SimIsEnd);

            CurrentState = 0;
        }

        private void Start()
        {
            CompletePercent = 0;
            TotalPassedStates = -1;
            IsOnTransition = false;

            ChangeState(new StartSimState(this));
            ChangeState(new Scene06.Scene06_01_Start(this));


            //Test();

        }

        private void Update()
        {
            if (_currentState != null)
                _currentState.Update(this);
        }

        public void ChangeState(SimDomenState newState)
        {

            if (CurrentState >= CorrectSequence.Count)
                return;


            if (CorrectSequence[CurrentState] != newState.StateType)
            {

                print($"STATE CAHNGE OCCURED [<color=red> {newState.StateType} </color>] BUT SHOULD BE [<color=green> {CorrectSequence[CurrentState].ToString()} </color>]");
                return;
            }
            else
                CurrentState++;


            print($"PREV. STATE[<color=orange> {newState.StateType} </color>] -> NEW STATE[<color=green> {CorrectSequence[CurrentState].ToString()} </color>]");

            if (_currentState != null) _currentState.Exit(this);

            TotalPassedStates++;

            _currentState = newState;

            if (_currentState != null)
            {
                _stateTypeForDebug = newState.StateType;
                _currentState.Enter(this);
                SimStateCanvas.CurrentSimulation.UpdateUI(_currentState);
            }
        }

        public void ResetCurrentScene()
        {
            _currentState.Reset(this);
        }
        public void StateAdapter(string scene)
        {
            switch (scene)
            {
                case "ApadterNiddlePass":
                    StateAdapter(StateTypeEnum.PassNiddleToHelper_08_01_05);
                    StateAdapter(StateTypeEnum.PassAdapterToHelper_08_01_07);
                    StateAdapter(StateTypeEnum.SceneIsDone_08_01);
                    break;
                case "08_03":
                    StateAdapter(StateTypeEnum.SceneIsDone_08_03);
                    break;


                case "GemMirrorOn":
                    StateAdapter(StateTypeEnum.GenMirrorProccess_08_03_02);
                    break;

                case "GemMirrorOff":
                    StateAdapter(StateTypeEnum.GenZerkaloOff_08_03_03);
                    break;

                case "06_04_01":
                    StateAdapter(StateTypeEnum.NiddleISSelected_06_04_01);
                    break;
            }
        }

        IEnumerator DelayedStateAdapter(StateTypeEnum state)
        {
            //yield return new WaitWhile(() => IsOnTransition);

            IsOnTransition = true;


            yield return new WaitForSeconds(1f);

            switch (state)
            {
                case StateTypeEnum.SimIsStart:
                    ChangeState(new TVP.StartSimState(this));
                    break;

                #region scene_06_01
                case StateTypeEnum.SceneIsStart_06_01:
                    ChangeState(new TVP.Scene06.Scene06_01_Start(this));
                    break;
                case StateTypeEnum.UziOsOn_06_01_01:
                    ChangeState(new TVP.Scene06.Scene06_01_01_UziDeviceIsOn(this));
                    break;
                case StateTypeEnum.UziDispalySettedUp_06_01_02:
                    ChangeState(new TVP.Scene06.Scene06_01_02_UziDisplaySettedUp(this));
                    break;
                case StateTypeEnum.PompIsOn_06_01_03:
                    ChangeState(new TVP.Scene06.Scene06_01_03_PompIsOn(this));
                    break;
                case StateTypeEnum.PressuarePompLevelCheck_06_01_04:
                    ChangeState(new TVP.Scene06.Scene06_01_04_PressuarePompLevelCheck(this));
                    break;
                case StateTypeEnum.PompPedalIsWorking_06_01_05:
                    ChangeState(new TVP.Scene06.Scene06_01_05_PompPedalCheck(this));
                    break;
                case StateTypeEnum.PompIsWorking_06_01_06:
                    ChangeState(new TVP.Scene06.Scene06_01_06_PompIsWorking(this));
                    break;

                case StateTypeEnum.SceneIsDone_06_01:
                    ChangeState(new TVP.Scene06.Scene06_01_Done(this));
                    break;

                #endregion scene_06_01

                #region scene_06_02
                case StateTypeEnum.SceneIsStart_06_02:
                    ChangeState(new TVP.Scene06.Scene06_02_Start(this));
                    break;

                case StateTypeEnum.HandsIsProccesed_06_02_01:
                    ChangeState(new TVP.Scene06.Scene06_02_01_HandsIsProccesed(this));
                    break;

                case StateTypeEnum.VaginaIsProccesed_06_02_02:
                    ChangeState(new TVP.Scene06.Scene06_02_02_VaginaIsProccesed(this));
                    break;


                case StateTypeEnum.SceneIsDone_06_02:
                    ChangeState(new TVP.Scene06.Scene06_02_Done(this));
                    break;


                #endregion scene_06_02

                #region scene_06_03
                case StateTypeEnum.SceneIsStart_06_03:
                    ChangeState(new TVP.Scene06.Scene06_03_Start(this));
                    break;
                case StateTypeEnum.GelIsApplied_06_03_01:
                    ChangeState(new TVP.Scene06.Scene06_03_01_Gel(this));
                    break;
                case StateTypeEnum.PrezervativIsApplied_06_03_02:
                    ChangeState(new TVP.Scene06.Scene06_03_02_Prezervativ(this));
                    break;
                case StateTypeEnum.BiopsyAdapterIsMounted_06_03_03:
                    ChangeState(new TVP.Scene06.Scene06_03_03_Adapter(this));
                    break;
                case StateTypeEnum.VaginaPenetreted_06_03_04:
                    ChangeState(new TVP.Scene06.Scene06_03_04_VaginaPenetrated(this));
                    break;
                case StateTypeEnum.UziEmulation_06_03_05:
                    ChangeState(new TVP.Scene06.Scene06_03_05_UziEmulation(this));
                    break;
                case StateTypeEnum.SimpleUziInvestigation_06_03_06:
                    ChangeState(new TVP.Scene06.Scene06_03_06_UziSimpleInvestigation(this));
                    break;

                case StateTypeEnum.SceneIsDone_06_03:
                    ChangeState(new TVP.Scene06.Scene06_03_Done(this));
                    break;
                #endregion scene_06_03

                #region scene_06_04
                case StateTypeEnum.SceneIsStart_06_04:
                    ChangeState(new TVP.Scene06.Scene06_04_Start(this));
                    break;
                case StateTypeEnum.NiddleISSelected_06_04_01:
                    ChangeState(new TVP.Scene06.Scene06_04_01_NiddleSelect(this));
                    break;
                case StateTypeEnum.AspiratingTestIsDone_06_04_02:
                    ChangeState(new TVP.Scene06.Scene06_04_02_AspiratingCheck(this));
                    break;
                case StateTypeEnum.NiddleIsMountedToAdapter_06_04_03:
                    ChangeState(new TVP.Scene06.Scene06_04_03_NiddleAttachToAdapter(this));
                    break;

                case StateTypeEnum.SceneIsDone_06_04:
                    ChangeState(new TVP.Scene06.Scene06_04_Done(this));
                    break;

                #endregion scene_06_04



                #region scene_07_01
                case StateTypeEnum.SceneIsStart_07_01:
                    ChangeState(new TVP.Scene07.Scene07_01_Start(this));
                    break;
                case StateTypeEnum.SurgeryIsStart_07_01_01:
                    ChangeState(new TVP.Scene07.Scene07_01_01_SurgeryIsStart(this));
                    break;
                case StateTypeEnum.AspiratingIsDone_07_01_02:
                    ChangeState(new TVP.Scene07.Scene07_01_02_AspiratingIsDone(this));
                    break;
                case StateTypeEnum.SurgeryIsDone_07_01_03:
                    ChangeState(new TVP.Scene07.Scene07_01_03_SurgeryIsDone(this));
                    break;

                case StateTypeEnum.SceneIsDone_07_01:
                    ChangeState(new TVP.Scene07.Scene07_01_Done(this));
                    break;

                #endregion scene_07_01



                #region scene_08_01
                case StateTypeEnum.SceneIsStart_08_01:
                    ChangeState(new TVP.Scene08.Scene08_01_Start(this));
                    break;
                case StateTypeEnum.DontReleasePedal_08_01_01:
                    ChangeState(new TVP.Scene08.Scene08_01_01_DontReleasePedal(this));
                    break;
                case StateTypeEnum.DeattachNiddleFromAdapter_08_01_02:
                    ChangeState(new TVP.Scene08.Scene08_01_02_DeattachNiddleFromAdapter(this));
                    break;
                case StateTypeEnum.PutNiddleAtAntisepticFluid_08_01_03:
                    ChangeState(new TVP.Scene08.Scene08_01_03_PutNiddleAtAntisepticFluid_08_01_03(this));
                    break;
                case StateTypeEnum.WashNiddle_08_01_04:
                    ChangeState(new TVP.Scene08.Scene08_01_04_WashNiddle_08_01_04(this));
                    break;
                case StateTypeEnum.PassNiddleToHelper_08_01_05:
                    ChangeState(new TVP.Scene08.Scene08_01_05_PassNiddleToHelper(this));
                    break;
                case StateTypeEnum.DeattachAdapterFromDevice_08_01_06:
                    ChangeState(new TVP.Scene08.Scene08_01_06_DeattachAdapterFromDevice(this));
                    break;
                case StateTypeEnum.PassAdapterToHelper_08_01_07:
                    ChangeState(new TVP.Scene08.Scene08_01_07_PassAdapterToHelper(this));
                    break;

                case StateTypeEnum.SceneIsDone_08_01:
                    ChangeState(new TVP.Scene08.Scene08_01_Done(this));
                    break;

                #endregion scene_08_01

                #region scene_08_02
                case StateTypeEnum.SceneIsStart_08_02:
                    ChangeState(new TVP.Scene08.Scene08_02_Start(this));
                    break;
                case StateTypeEnum.LastUziInvestigation_08_02_01:
                    ChangeState(new TVP.Scene08.Scene08_02_01_LastUziInvestigation(this));
                    break;
                case StateTypeEnum.PrintResultOfSurgery_08_02_02:
                    ChangeState(new TVP.Scene08.Scene08_02_02_PrintResultOfSurgery(this));
                    break;
                case StateTypeEnum.SceneIsDone_08_02:
                    ChangeState(new TVP.Scene08.Scene08_02_Done(this));
                    break;

                #endregion scene_08_02

                #region scene_08_03
                case StateTypeEnum.SceneIsStart_08_03:
                    ChangeState(new TVP.Scene08.Scene08_03_Start(this));
                    break;
                case StateTypeEnum.lastProccesVagina_08_03_01:
                    ChangeState(new TVP.Scene08.Scene08_03_01_lastProccesVagina(this));
                    break;
                case StateTypeEnum.GenMirrorProccess_08_03_02:
                    ChangeState(new TVP.Scene08.Scene08_03_02_GenMirrorProccess(this));
                    break;
                case StateTypeEnum.GenZerkaloOff_08_03_03:
                    ChangeState(new TVP.Scene08.Scene08_03_03_GenZerkaloOff(this));
                    break;
                case StateTypeEnum.SceneIsDone_08_03:
                    ChangeState(new TVP.Scene08.Scene08_03_Done(this));
                    break;

                #endregion scene_08_03




                default:
                    Debug.Log("Error!" + state.ToString() + " not definied!");
                    break;
            }

            yield return new WaitForSeconds(1);
            IsOnTransition = false;

        }

        public void AVC(StateTypeEnum state)
        {

        }

        internal void StateAdapter(StateTypeEnum state)
        {
            StartCoroutine(DelayedStateAdapter(state));
        }


        public void ResetToState(StateTypeEnum state)
        {
            CurrentState = CorrectSequence.IndexOf(state);
            StateAdapter(state);
        }


        #region ReseterFunctions

        public void ResetToScene06_01()
        {
            ResetToState(StateTypeEnum.SceneIsStart_06_01);
            PompController.CurrentSimulation.TurnOff();
            UziDevice.CurrentSimulation.TurnOff();
        }
        public void ResetToScene06_02()
        {
            ResetToState(StateTypeEnum.SceneIsStart_06_02);
        }
        public void ResetToScene06_03()
        {
            ResetToState(StateTypeEnum.SceneIsStart_06_03);
        }
        public void ResetToScene06_04()
        {
            ResetToState(StateTypeEnum.SceneIsStart_06_04);
        }
        public void ResetToScene07_01()
        {
            ResetToState(StateTypeEnum.SceneIsStart_07_01);
        }
        public void ResetToScene08_01()
        {
            ResetToState(StateTypeEnum.DontReleasePedal_08_01_01);
        }
        public void ResetToScene08_02()
        {
            ResetToState(StateTypeEnum.SceneIsStart_08_02);
        }
        public void ResetToScene08_03()
        {
            ResetToState(StateTypeEnum.SceneIsStart_08_03);
        }
        #endregion ReseterFunctions

        #region TEST

        [ContextMenu("Test")]
        public void Test()
        {
            StartCoroutine(GoThroughEachState());
        }


        IEnumerator GoThroughEachState()
        {
            foreach (StateTypeEnum state in CorrectSequence)
            {
                yield return new WaitForSeconds(.3f);
                SimStateCanvas.CurrentSimulation.RegularSceneConfig();
                ResetToState(state);
            }
        }
        #endregion TEST

    }

}