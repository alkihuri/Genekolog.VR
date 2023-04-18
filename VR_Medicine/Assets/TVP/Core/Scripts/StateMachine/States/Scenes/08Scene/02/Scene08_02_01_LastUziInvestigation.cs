using TVP.UI;

namespace TVP.Scene08
{

    public class Scene08_02_01_LastUziInvestigation : SimDomenState
    {
        public Scene08_02_01_LastUziInvestigation(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.LastUziInvestigation_08_02_01;
            CurrentStep = 1;
            TotalSteps = 2;
            LevelTittle = "08.02 УЗИ";
            FullDesription = "Инструкция:\r\n1. Провести контрольное трансвагинальное УЗИ исследование: определить объем правого и левого яичников, матки, толщины эндометрия и количество свободно жидкости в полости малого таза.\r\n2. Зафиксировать все измерения на фотопринтере.";
            Description = "Зафиксировать все измерения на фотопринтере.";

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
            stateMachine.ResetToScene08_02();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }

}