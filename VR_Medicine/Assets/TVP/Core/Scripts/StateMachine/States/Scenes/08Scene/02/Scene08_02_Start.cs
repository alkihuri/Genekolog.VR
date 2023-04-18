using TVP.UI;

namespace TVP.Scene08
{
    public partial class Scene08_02_Start : SimDomenState
    {
        public Scene08_02_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsStart_08_02;  
            CurrentStep = 0;
            TotalSteps = 2;
            LevelTittle = "08.02 УЗИ";
            FullDesription = "Инструкция:\r\n1. Провести контрольное трансвагинальное УЗИ исследование: определить объем правого и левого яичников, матки, толщины эндометрия и количество свободно жидкости в полости малого таза.\r\n2. Зафиксировать все измерения на фотопринтере.";
            Description = "Провести контрольное трансвагинальное УЗИ исследование";

        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            SimStateCanvas.CurrentSimulation.NewSceneConfig();
            stateMachine.TimeMachine.StartTimer(60);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {

        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene08_02();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}