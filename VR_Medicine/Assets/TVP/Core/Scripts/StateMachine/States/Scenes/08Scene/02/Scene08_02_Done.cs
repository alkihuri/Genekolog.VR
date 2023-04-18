namespace TVP.Scene08
{
    public partial class Scene08_02_Done : SimDomenState
    {
        public Scene08_02_Done(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsDone_08_02;
            CurrentStep = 2;
            TotalSteps = 2;
            LevelTittle = "08.02 УЗИ";
            FullDesription = "Инструкция:\r\n1. Провести контрольное трансвагинальное УЗИ исследование: определить объем правого и левого яичников, матки, толщины эндометрия и количество свободно жидкости в полости малого таза.\r\n2. Зафиксировать все измерения на фотопринтере.";
            Description = "Подготовка след. сцены";

        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            IsDoneAtTime = stateMachine.TimeMachine.IsDoneAtTime;
            stateMachine.TimeMachine.OnStateFinihsh?.Invoke();
            PrintInfo();
            NextScenLevel();
        }
        public void NextScenLevel()
        {
            SimDomenStateMachine.CurrentSimulation.StateAdapter(StateTypeEnum.SceneIsStart_08_03);
        } 

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene08_02();
        }
    }
}