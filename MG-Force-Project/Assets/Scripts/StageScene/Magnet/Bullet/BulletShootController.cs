using Game.GameSystem;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Game.StageScene.Magnet
{
    public class BulletShootController : MonoBehaviour 
    {
        private const float ADD_POWER = 0.2f;

        private enum PowerMeter
        {
            INIT = 0,
            WEAK = 33,
            MEDIUM = 66,
            MAX = 100,
        }

        private InputHandler _inputHandler;

        private MagnetManager _magnet;

        private float _currentPower;

        //[SerializeField] private GameObject bulletPrefab;

        [SerializeField] private GameObject _chargeGageObj;

        [SerializeField] private Image _chargeGage;

        [SerializeField] private GameObject _powerEffectObj;

        private ParticleSystem _particleSystem;

        private bool _canShooting;

        private bool _isCharging;

        public Vector3 targetPos { get; private set; }

        private void Start()
        {
            _inputHandler = InputHandler.Instance;

            _magnet = GameObject.Find(GameConstants.MAGNET_MANAGER_OBJ).GetComponent<MagnetManager>();

            _chargeGageObj.SetActive(false);

            _chargeGage = _chargeGageObj.GetComponent<Image>();

            _powerEffectObj.SetActive(false);

            _particleSystem = _powerEffectObj.GetComponent<ParticleSystem>();
        }

        private void Update()
        {
            if (_magnet.IsMagnetBoot) return;

            if (_canShooting)
            {
                //GameObject gb = Instantiate(bulletPrefab);
                //Vector3 init_position = gameObject.transform.position;
                //init_position.y += 1;

                //gb.transform.position = init_position;

                //_canShooting = false;

                DebugManager.LogMessage("発射ー！！！");

                _powerEffectObj.SetActive(false);

                _canShooting = false;
            }
            else
            {
                if (_inputHandler.IsActionPressing(InputConstants.Action.SHOOT))
                {
                    if (!_isCharging)
                    {
                        _isCharging = true;

                        _currentPower = (int)PowerMeter.INIT;

                        _chargeGageObj.SetActive(true);
                        _powerEffectObj.SetActive(true);
                    }

                    ChageUpdate();
                }

                if (!_inputHandler.IsActionPressing(InputConstants.Action.SHOOT) && _isCharging)
                {
                    _isCharging = false;

                    _chargeGageObj.SetActive(false);

                    _canShooting = true;
                }
            }
        }

        private void ChageUpdate()
        {
            if (_currentPower < (int)PowerMeter.MAX) { _currentPower += ADD_POWER; }

            _chargeGage.fillAmount = Mathf.Clamp01(_currentPower / (int)PowerMeter.MAX);
            
            var main_module = _particleSystem.main;

            if (_currentPower <= (int)PowerMeter.WEAK)
            {
                main_module.startColor = Color.green;
            }
            else if (_currentPower <= (int)PowerMeter.MEDIUM)
            {
                main_module.startColor = Color.yellow;
            }
            else
            {
                main_module.startColor = Color.red;
            }
        }
    }
}