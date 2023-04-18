using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.DataManager
{
    public static class SimulationManagerData  
    {
        private static bool _isTest;
        public static bool IsTest 
        { 
            get => _isTest; set => _isTest = value; }
         
    }
}