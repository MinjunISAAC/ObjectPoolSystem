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
                    Debug.LogError($"[NpcTrigger.NpcType] Npc Type�� �������� �ʾҽ��ϴ�. Code : {nameof(_npcType)}");

                return _npcType;
            }
        }
    }
}