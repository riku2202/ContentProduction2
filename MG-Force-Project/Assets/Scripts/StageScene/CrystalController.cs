using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.StageScene
{
    public class CrystalController : MonoBehaviour
    {
        public bool IsGoalEvent { get; private set; }

        private float _speed = 18.5f;

        private Vector3 _rotate;

        private void Start()
        {
            IsGoalEvent = false;
        }

        private void Update()
        {
            _rotate = gameObject.transform.eulerAngles;

            _rotate.y += Time.deltaTime * _speed;

            gameObject.transform.eulerAngles = _rotate;
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(GameConstants.Tag.PLAYER))
            {
                IsGoalEvent = true;
                //SceneManager.LoadScene("Clear");//ClearScene‚É‘JˆÚ
            }
        }
    }
}