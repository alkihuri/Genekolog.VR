using TVP.UI;
using Unity.VisualScripting;
using UnityEditor;

namespace TVP.Scene06
{
    public class Scene06_03_Start : SimDomenState
    {
        public Scene06_03_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsStart_06_03;
            CurrentStep = 0;
            TotalSteps = 6;
            FullDesription = "Инструкция:\r\nВзять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ\r\nВзять презерватив - надеть на УЗИ датчиĸ, \r\nВзять адаптер - приĸрепить ĸ УЗИ датчиĸу - теперь адаптер в сборе \r\nВведение во влагалище - \r\nЭмуляция УЗИ\r\nИсследования ";
            LevelTittle = "06.03 Подготовка";
            Description = "Взять со столика гель - отĸрыть паĸетиĸ, нанести на УЗИ датчиĸ";
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
            stateMachine.ResetToScene06_03();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }
}