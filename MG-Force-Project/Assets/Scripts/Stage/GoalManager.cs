using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage 
{
    public class GoalManager : MonoBehaviour
    {
        public bool IsGoalEvent { get; private set; }

        private void Start()
        {
            IsGoalEvent = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameConstants.Tag.Player.ToString()))
            {
                IsGoalEvent = true;
            }
        }
    }
}