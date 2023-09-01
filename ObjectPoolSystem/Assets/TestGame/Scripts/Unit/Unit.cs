// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

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
        // Functions - Event
        // --------------------------------------------------
        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer != _npcLayer.value)
                return;

            Debug.Log($"Enter Npc : {other.gameObject.name}");
        }

        private void OnTriggerStay(Collider other)
        {
            if (1 << other.gameObject.layer != _npcLayer.value)
                return;

            Debug.Log($"Stay Npc : {other.gameObject.name}");
        }

        private void OnTriggerExit(Collider other)
        {
            if (1 << other.gameObject.layer != _npcLayer.value)
                return;

            Debug.Log($"Exit Npc : {other.gameObject.name}");
        }
    }
}