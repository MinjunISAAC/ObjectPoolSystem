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
        // Variables
        // --------------------------------------------------
        private bool _isActive = false;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public ENpcType NpcType 
        {
            get
            {
                if (_npcType == ENpcType.Unknown)
                    Debug.LogError($"[Npc.NpcType] Npc Type�� �������� �ʾҽ��ϴ�. Code : {nameof(_npcType)}");

                return _npcType;
            }
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------

    }
}