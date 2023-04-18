using TVP.UI;

namespace TVP.Scene06
{
    public class Scene06_01_Start : SimDomenState
    {


        public Scene06_01_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsStart_06_01;
            FullDesription = "Инструкция:\r\nВключить УЗИ аппарат с монитором\r\nПодготовить аппарат УЗИ\r\nПодготовить помпу УЗИ\r\nПроверить уровень разрежения.\r\nПроверить работу ножной педали\r\nПроверить работу помпы";

            Description = "Настроить изображение на узи мониторе";
            LevelTittle = "06.01 Подготовка УЗИ аппарата";
            TotalSteps = 6;
            CurrentStep = 0;

        }

        public override void Enter(SimDomenStateMachine stateMachine)
        {
            PrintInfo();
            SimStateCanvas.CurrentSimulation.NewSceneConfig(); 

            stateMachine.TimeMachine.StartTimer(180);
        }

        public override void Exit(SimDomenStateMachine stateMachine)
        {  
        }

        public override void Reset(SimDomenStateMachine stateMachine)
        {
            stateMachine.ResetToScene06_01();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}