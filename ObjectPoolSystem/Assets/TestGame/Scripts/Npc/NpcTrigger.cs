// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace InGame.ForNpc
{
    public class NpcTrigger : MonoBehaviour
    {
        // --------------------------------------------------
        // Npc Trigger State Enum
        // --------------------------------------------------
        public enum ETriggerState
        {
            Unknown = 0,
            Show    = 1,
            Hide    = 2,
            Clear   = 3,
        }

        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private ENpcType      _npcType      = ENpcType.Unknown;
        [SerializeField] private ETriggerState _triggerState = ETriggerState.Hide;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public ENpcType NpcType 
        {
            get
            {
                if (_npcType == ENpcType.Unknown)
                    Debug.LogError($"[NpcTrigger.NpcType] Npc Type이 지정되지 않았습니다. Code : {nameof(_npcType)}");

                return _npcType;
            }
        }
    }
}