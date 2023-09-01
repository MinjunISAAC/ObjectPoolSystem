// ----- C#
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

// ----- User Defined
using InGame.ForUnit.Manage;
using InGame.ForUnit;
using InGame.ForCam;

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

            yield return null;
        }

        // --------------------------------------------------
        // Functions - Nomal
        // --------------------------------------------------

    }
}