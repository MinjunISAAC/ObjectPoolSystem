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
        [SerializeField] private List<Npc> _testNpcGroup = null;
        [SerializeField] private Transform _npcParents   = null;

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<ENpcType, ObjectPool<Npc>> _pools   = null;

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        //private void Awake()
        //{
        //    CreatedToNpc();
        //}

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------
        // ----- Public
        public Npc ActiveToNpc(ENpcType npcType, Vector3 pos)
        {
            if (!_pools.TryGetValue(npcType, out ObjectPool<Npc> pool))
            {
                Debug.LogError($"<color=red>[NpcController.ActiveToNpc] {npcType}의 Object Pool이 존재하지 않습니다.</color>");
                return null;
            }

            var npc = pool.GetObject(_npcParents.transform);
            npc.transform.position = pos;

            return npc;
        }

        public void InactiveToNpc(NpcTrigger npcTrigger)
        {
            var npcType = npcTrigger.NpcType;

            if (!_pools.TryGetValue(npcType, out ObjectPool<Npc> pool))
            {
                Debug.LogError($"<color=red>[NpcController.InactiveToNpc] {nameof(npcType)}의 Object Pool이 존재하지 않습니다.</color>");
                return;
            }

            pool.ReturnObject(npcTrigger.TargetNpc);
        }

        public void CreatedToNpc() 
        {
            _pools = new Dictionary<ENpcType, ObjectPool<Npc>>();

            for (int i = 0; i < _testNpcGroup.Count; i++)
            {
                var npc     = _testNpcGroup[i];
                var npcPool = new ObjectPool<Npc>(30);

                npcPool.OnInit(npc, _npcParents.transform);

                _pools.Add(npc.NpcType, npcPool);
            }
        }

        // ----- Private
    }
}