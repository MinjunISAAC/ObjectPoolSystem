// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage.ForUI;
using InGame.ForNpc;

namespace InGame.ForUnit.Manage
{
    public class UnitController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Joy Pad")]
        [SerializeField] private JoyPad    _joyPad            = null;

        [Space(1.5f)][Header("Unit Collection")]
        [SerializeField] private Transform _unitCreateParents = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        // ----- Const
        private const float ROTATE_VALUE = 0.5f;

        // ----- Private
        private Unit  _targetUnit  = null;
        private float _unitMoveValue = 0.0f;

        // --------------------------------------------------
        // Property
        // --------------------------------------------------
        public Unit   PlayableUnit => _targetUnit;
        public JoyPad JoyPad       => _joyPad;

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Unit CreateTargetUnit(Unit targetUnit, Transform spawnTrans = null)
        {
            if (_targetUnit != null)
            {
                Debug.LogError($"<color = red>[UnitController.CreateTargetUnit] 이미 PlayableUnit이 존재합니다.</color>");
                return null;
            }

            _targetUnit = Instantiate(targetUnit, _unitCreateParents);

            if (spawnTrans != null)
                _targetUnit.transform.position = spawnTrans.position;

            SetJoyPad();

            return _targetUnit;
        }
        
        public void InitToNpcTriggerInteraction(Action<NpcTrigger> onNpcEncounter, Action<NpcTrigger> onNpcStay, Action<NpcTrigger> onNpcBreakUp)
        {
            // [TODO] 테스트 후 주석처리 되어야할 부분
            if (_targetUnit == null)
            {
                Debug.LogError($"<color=red>[UnitController.InitiateNpcInteraction] Target Unit이 지정되지 않았습니다.</color>");
                return;
            }

            _targetUnit.EventOnNpcTriggerEncounter += (npcTrigger) => onNpcEncounter(npcTrigger);
            _targetUnit.EventOnNpcTriggerStay      += (npcTrigger) => onNpcStay     (npcTrigger);
            _targetUnit.EventOnNpcTriggerBreakUp   += (npcTrigger) => onNpcBreakUp  (npcTrigger);
        }

        public void SetJoyPad()
        {
            if (_targetUnit == null)
            {
                Debug.LogError($"<color = red>[UnitController.SetJoyPad] Target Unit이 할당되지 않았습니다.</color>");
                return;
            }

            _joyPad.OnInit(_targetUnit);
            _joyPad.UsedJoyStickEvent(true);
        }

        public void ChangeUnitSpeed(float moveSpeed, float roatateSpeed = ROTATE_VALUE)
        {
            _unitMoveValue = moveSpeed;
            _joyPad.ChangeMoveFactors(moveSpeed, roatateSpeed);
        }

        public void UsedJoyPad(bool isOn)
        {
            _joyPad.UsedJoyStickEvent(isOn);

            if (!isOn) _joyPad.FrameRect.gameObject.SetActive(isOn);
        }
    }
}
