using TVP.UI;
using UnityEngine;

namespace TVP.Scene06
{
    public class Scene06_04_Start : SimDomenState
    {
        public Scene06_04_Start(SimDomenStateMachine stateMachine) : base(stateMachine)
        {
            StateType = StateTypeEnum.SceneIsStart_06_04;
            FullDesription = "Инструкция:\r\n1. Выбрать иглу для пункции\r\n2. Проверить иглы на проходимость: аспирировать жидкость из тестовой пробирки\r\n3. Ввести иглу для пункции в адаптер";
            CurrentStep = 0;
            TotalSteps = 3;
            LevelTittle = "06.04 Исследование";
            Description = "Выбрать иглу для пункции!";

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
            stateMachine.ResetToScene06_04();
        }

        public override void Update(SimDomenStateMachine stateMachine)
        {

        }
    }

}