// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage;
using InGame.ForUnit;
using InGame.ForCam;
using InGame.ForNpc.Manage;
using InGame.ForNpc;

namespace InGame
{
    public class Main : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [Header("Manage")]
        [SerializeField] private UnitController _unitController = null;
        [SerializeField] private CamController  _camController  = null;
        [SerializeField] private NpcController  _npcController  = null;

        [Space][Header("Unit")]
        [SerializeField] private Unit           _unitOrigin     = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Unit _targetUnit = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private IEnumerator Start()
        {
            // Test Unit 할당
            _targetUnit = _unitController.CreateTargetUnit(_unitOrigin);

            // Camera 초기화
            _camController.OnInit(_targetUnit);
            _camController.ChangeToCamState(CamController.ECamState.Follow_Unit);

            // Unit & Npc 상호작용 초기화
            _npcController.CreatedToNpc();

            Action<NpcTrigger> onEnterNpc = 
            (npcTrigger) => 
            {
                if (npcTrigger.TargetNpc == null)
                {
                    var npc = _npcController.ActiveToNpc(npcTrigger.NpcType, npcTrigger.transform.position);
                    npcTrigger.ChainTargetNpc(npc);
                }
            };
            
            Action<NpcTrigger> onStayNpc  = 
            (npcTrigger) =>
            {
                if (npcTrigger.TargetNpc == null)
                    npcTrigger.ChainTargetNpc(npcTrigger.TargetNpc);
            };

            Action<NpcTrigger> onExitNpc  = 
            (npcTrigger) => 
            {
                if (npcTrigger.TargetNpc == null)
                    return;

                _npcController.InactiveToNpc(npcTrigger);
                npcTrigger.ChainTargetNpc(null);
            };

            //[등록]
            _unitController.InitToNpcTriggerInteraction(onEnterNpc, onStayNpc, onExitNpc);

            yield return null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------

    }
}