// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined

namespace InGame.ForNpc
{
    public class Npc : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private ENpcType _npcType = ENpcType.Unknown;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public ENpcType NpcType 
        {
            get
            {
                if (_npcType == ENpcType.Unknown)
                    Debug.LogError($"[Npc.NpcType] Npc Type이 지정되지 않았습니다. Code : {nameof(_npcType)}");

                return _npcType;
            }
        }
    }
}