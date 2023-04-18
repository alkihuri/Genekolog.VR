using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_03_04_VaginaPenetrated: SimDomenState
    {
        public Scene06_03_04_VaginaPenetrated(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.VaginaPenetreted_06_03_04;
            CurrentStep = 4;
            TotalSteps = 6;
            LevelTittle = "06.03 Подготовка";
            Description = "Эмуляция узи";
        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}