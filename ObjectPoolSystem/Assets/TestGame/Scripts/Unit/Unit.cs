// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForNpc;

namespace InGame.ForUnit
{
    public class Unit : MonoBehaviour
    {
        // --------------------------------------------------
        // Components
        // --------------------------------------------------
        [SerializeField] private Rigidbody _rb       = null;
        [SerializeField] private LayerMask _npcLayer = -1;

        // --------------------------------------------------
        // Properties
        // --------------------------------------------------
        public Rigidbody UnitRigidBody => _rb;

        // --------------------------------------------------
        // Npc In Event
        // --------------------------------------------------
        public event Action<NpcTrigger> EventOnNpcTriggerEncounter = null;
        public void OnNpcEncounter(NpcTrigger npcTrigger)
        {
            if (EventOnNpcTriggerEncounter != null)
                EventOnNpcTriggerEncounter(npcTrigger);
        }

        public event Action<ENpcType, Vector3> EventOnNpcTriggerStay = null;
        public void OnNpcStay(ENpcType npcType, Vector3 pos)
        {
            if (EventOnNpcTriggerStay != null)
                EventOnNpcTriggerStay(npcType, pos);
        }

        public event Action<NpcTrigger> EventOnNpcTriggerBreakUp = null;
        public void OnNpcBreakUp(NpcTrigger npcTrigger)
        {
            if (EventOnNpcTriggerBreakUp != null)
                EventOnNpcTriggerBreakUp(npcTrigger);
        }

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer != _npcLayer.value)
                return;

            if (other.TryGetComponent<NpcTrigger>(out NpcTrigger npcTrigger))
                OnNpcEncounter(npcTrigger);
            else // [TODO] Test 후 삭제 필요한 부분
                Debug.LogError($"<color=red>[Unit.OnTriggerEnter] Layer 설정이 잘못된 Npc가 존재합니다.</color>");
        }

        private void OnTriggerStay(Collider other)
        {
            if (1 << other.gameObject.layer != _npcLayer.value)
                return;

            if (other.TryGetComponent<NpcTrigger>(out NpcTrigger npcTrigger))
                OnNpcStay(npcTrigger.NpcType, npcTrigger.transform.position);
            else // [TODO] Test 후 삭제 필요한 부분
                Debug.LogError($"<color=red>[Unit.OnTriggerStay] Layer 설정이 잘못된 Npc가 존재합니다.</color>");
        }

        private void OnTriggerExit(Collider other)
        {
            if (1 << other.gameObject.layer != _npcLayer.value)
                return;

            if (other.TryGetComponent<NpcTrigger>(out NpcTrigger npcTrigger))
                OnNpcBreakUp(npcTrigger);
            else // [TODO] Test 후 삭제 필요한 부분
                Debug.LogError($"<color=red>[Unit.OnTriggerExit] Layer 설정이 잘못된 Npc가 존재합니다.</color>");
        }
    }
}