// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using Utility.ForObjectPool;

namespace InGame.ForNpc.Manage
{
    public class NpcController : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private List<Npc>                             _testNpc = null;
        [SerializeField] private Dictionary<ENpcType, ObjectPool<Npc>> _pools   = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Start()
        {
            CreatedToNpc();
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        private void CreatedToNpc() 
        {
            _pools = new Dictionary<ENpcType, ObjectPool<Npc>>();
            for (int i = 0; i < _testNpc.Count; i++)
            {
                var npc     = _testNpc[i];
                var npcPool = new ObjectPool<Npc>(10);
                npcPool.OnInit(npc, null);

                _pools.Add(npc.NpcType, npcPool);

                //ObjectPoolSystem.Instance.CreatePool<ENpcType, Npc>(npc, 10);
            }
        }

    }
}