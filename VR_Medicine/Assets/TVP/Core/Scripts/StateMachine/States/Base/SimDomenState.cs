using Common.DataManager;
using Common.UI;
using UnityEngine;

namespace TVP
{
    public abstract class SimDomenState
    {
        protected SimDomenStateMachine _stateMachine;

        public StateTypeEnum StateType;
        public string Description;
        public string FullDesription;
        public string LevelTittle;
        public int TotalSteps;
        public int CurrentStep;
        public int Time; 
        public bool IsDone;
         


        private bool _isDoneAtTime;

        public bool IsDoneAtTime 
        { 
            get => _isDoneAtTime; 
            set
            {
                _isDoneAtTime = value; 
                if (_isDoneAtTime)
                {
                    SimulationDataManager.IncreaseCompleteSteps();
                }
                else
                { 
                    SimulationDataManager.IncreaseError();
                }
            }
        }

        public SimDomenState(SimDomenStateMachine stateMachine)
        {
            this._stateMachine = stateMachine;
        }
        public void PrintInfo()
        {
            Debug.Log(StateType + " is statred!");
        }

        public abstract void Update(SimDomenStateMachine stateMachine);
        public abstract void Enter(SimDomenStateMachine stateMachine);
        public abstract void Exit(SimDomenStateMachine stateMachine); 
        public abstract void Reset(SimDomenStateMachine stateMachine); 
    }
}