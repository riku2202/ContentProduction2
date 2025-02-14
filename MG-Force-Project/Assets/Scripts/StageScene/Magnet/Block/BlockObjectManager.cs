using UnityEngine;

namespace Game.StageScene
{
    public class BlockObjectManager : MonoBehaviour
    {
        private enum ObjectType
        {
            NOT_FIXED_BLOCK,
            N_FIXED_BLOCK,
            S_FIXED_BLOCK,
            CAN_FIXED_BLOCK,
            NOT_MOVING_1_BLOCK,
            NOT_MOVING_2_BLOCK,
            NOT_MOVING_3_BLOCK,
            CAN_MOVING_BLOCK,
            N_MOVING_BLOCK,
            S_MOVING_BLOCK,

            GIMMICK_BLOCK,
        }

        private ObjectType _myType;

        [NamedSerializeField(
            new string[]
            {
                "NotFixed_Block",
                "NFixed_Block",
                "SFixed_Block",
                "CanFixed_Block",
                "NotMoving1_Block",
                "NotMoving2_Block",
                "NotMoving3_Block",
                "CanMoving_Block",
                "NMoving_Block",
                "SMoving_Block",
                "Gimmick_Block",
            }
        )]
        [SerializeField]
        private GameObject[] _originObjects;

        public GameObject currentOriginObject { get; private set; }

        public GameObject currentObject { get; private set; }

        private void SetObject()
        {
            currentOriginObject = _originObjects[(int)_myType];

            SetObjectLayer(gameObject.layer);
        }

        private void SetChildConfig()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.layer != (int)GameConstants.Layer.MAGNET_RANGE)
                {
                    child.gameObject.layer = gameObject.layer;
                }

                child.gameObject.tag = gameObject.tag;
            }
        }

        public void SetObjectType(int type, int new_layer, string new_tag)
        {
            if (type < 0) type = 10;
            else type--;

            _myType = (ObjectType)type;

            gameObject.layer = new_layer;
            gameObject.tag = new_tag;

            SetObject();
        }

        public void SetObjectLayer(int new_layer)
        {
            gameObject.layer = new_layer;

            switch (gameObject.layer)
            {
                case (int)GameConstants.Layer.DEFAULT:
                    if (gameObject.CompareTag(GameConstants.Tag.FIXED))
                    {
                        currentOriginObject = _originObjects[(int)ObjectType.CAN_FIXED_BLOCK];
                    }
                    else
                    {
                        currentOriginObject = _originObjects[(int)ObjectType.CAN_MOVING_BLOCK];
                    }

                    break;
                
                case (int)GameConstants.Layer.N_MAGNET:
                    if (gameObject.CompareTag(GameConstants.Tag.FIXED))
                    {
                        currentOriginObject = _originObjects[(int)ObjectType.N_FIXED_BLOCK];
                    }
                    else
                    {
                        currentOriginObject = _originObjects[(int)ObjectType.N_MOVING_BLOCK];
                    }

                    break;

                case (int)GameConstants.Layer.S_MAGNET:
                    if (gameObject.CompareTag(GameConstants.Tag.FIXED))
                    {
                        currentOriginObject = _originObjects[(int)ObjectType.S_FIXED_BLOCK];
                    }
                    else
                    {
                        currentOriginObject = _originObjects[(int)ObjectType.S_MOVING_BLOCK];
                    }

                    break;
            }

            SetChildConfig();
        }
    }
}