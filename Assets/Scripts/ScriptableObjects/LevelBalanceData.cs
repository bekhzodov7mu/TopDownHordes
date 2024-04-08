using System;
using System.Linq;
using Sirenix.OdinInspector;
using TopDownHordes.Enemies;
using UnityEngine;

namespace TopDownHordes.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(LevelBalanceData), menuName = "GamePlay SO/" + nameof(LevelBalanceData))]
    public class LevelBalanceData : ScriptableObject
    {
        [ValidateInput(nameof(ValidateBalanceInfo))]
        public EnemiesBalanceInfo[] EnemiesBalanceInfo;
        
        private bool ValidateBalanceInfo(ref string errorMessage)
        {
            for (int i = 0; i < EnemiesBalanceInfo.Length; i++)
            {
                if (i + 1 >= EnemiesBalanceInfo.Length) break;

                if (EnemiesBalanceInfo[i].TimeRequired >= EnemiesBalanceInfo[i + 1].TimeRequired)
                {
                    errorMessage = $"{EnemiesBalanceInfo[i].Progression} 'TimeRequired' should be less than {EnemiesBalanceInfo[i + 1].Progression} 'TimeRequired'";
                    return false;
                }
            }
            
            return true;
        }
    }
    
    [Serializable]
    public class EnemiesBalanceInfo
    {
        [FoldoutGroup("$Progression")]
        public string Progression = "Progression 0";
        
        [FoldoutGroup("$Progression")]
        public int TimeRequired;

        [FoldoutGroup("$Progression")]
        [ValidateInput(nameof(ValidateBalance))]
        public Collections.SerializableDictionary<EnemyLinker, int> EnemiesSpawnProbability;

        private bool ValidateBalance(ref string errorMessage)
        {
            int totalProbability = EnemiesSpawnProbability.Sum(x => x.Value);
            if (totalProbability != 100)
            {
                errorMessage = "Total percentage value us NOT 100!";
                return false;
            }

            return true;
        }
    }
}