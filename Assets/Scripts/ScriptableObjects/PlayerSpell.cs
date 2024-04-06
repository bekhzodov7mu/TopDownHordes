﻿using Sirenix.OdinInspector;
using TopDownHordes.Projectile;
using UnityEngine;

namespace TopDownHordes.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerSpell), menuName = "GamePlay SO/" + nameof(PlayerSpell))]
    public class PlayerSpell : ScriptableObject
    {
        [ValidateInput(nameof(ValidateSpellName))]
        public string SpellName; // Localization Key in future if localization is added to project
        
        [ValidateInput(nameof(ValidatePrefabs))]
        public SpellProjectile ProjectilePrefab;

        [ValidateInput(nameof(ValidateProjectileSpeed))]
        public float ProjectileSpeed;
        
        [ValidateInput(nameof(ValidateReloadSpeed))]
        public float ReloadSpeed;

        [ValidateInput(nameof(ValidateDamageValue))]
        public float Damage;

        private bool ValidateSpellName(ref string errorMessage)
        {
            if (SpellName == string.Empty)
            {
                errorMessage = $"{nameof(SpellName)} is empty!";
                return false;
            }

            return true;
        }
        
        private bool ValidatePrefabs(ref string errorMessage)
        {
            if (ProjectilePrefab == null)
            {
                errorMessage = $"{nameof(ProjectilePrefab)} is Null!";
                return false;
            }

            return true;
        }
        
        private bool ValidateProjectileSpeed(ref string errorMessage)
        {
            if (ProjectileSpeed > 0)
            {
                return true;
            }
            
            errorMessage = $"{ProjectileSpeed} value should be more than 0!";
            return false;
        }

        private bool ValidateReloadSpeed(ref string errorMessage)
        {
            if (ReloadSpeed > 0)
            {
                return true;
            }
            
            errorMessage = $"{nameof(ReloadSpeed)} value should be more than 0!";
            return false;
        }
        
        private bool ValidateDamageValue(ref string errorMessage)
        {
            if (Damage > 0)
            {
                return true;
            }
            
            errorMessage = $"{nameof(Damage)} value should be more than 0!";
            return false;
        }
    }
}