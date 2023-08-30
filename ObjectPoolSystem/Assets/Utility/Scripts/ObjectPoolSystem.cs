// ----- C#
using System;
using System.Collections;
using System.Collections.Generic;

// ----- Unity
using UnityEngine;

namespace Utility.ForObjectPool
{
    public class ObjectPoolSystem : MonoBehaviour
    {
        // --------------------------------------------------
        // Singleton
        // --------------------------------------------------
        // ----- Constructor
        private ObjectPoolSystem() { }

        // ----- Static Variables
        private static ObjectPoolSystem _instance = null;

        // ----- Variables
        private const string FILE_PATH = "ObjectPoolSystem.prefab";
        private bool _isSingleton = false;

        // ----- Property
        public static ObjectPoolSystem Instance
        {
            get
            {
                if (null == _instance)
                {
                    var existingInstance = FindObjectOfType<ObjectPoolSystem>();

                    if (existingInstance != null)
                    {
                        _instance = existingInstance;
                    }
                    else
                    {
                        var origin = Resources.Load<ObjectPoolSystem>(FILE_PATH);
                        _instance = Instantiate<ObjectPoolSystem>(origin);
                        _instance._isSingleton = true;
                        DontDestroyOnLoad(_instance.gameObject);
                    }
                }

                return _instance;
            }
        }

        // --------------------------------------------------
        // Variables
        // --------------------------------------------------
        private Dictionary<Type, object> pools = new Dictionary<Type, object>();

        public void CreatePool<TKey>(TKey obj, int capacity) where TKey : Component
        {
            var pool = new ObjectPool<TKey>(capacity);
            pools.Add(typeof(TKey), pool);
        }

        public TKey GetObject<TKey>() where TKey : Component
        {
            if (!pools.TryGetValue(typeof(TKey), out var pool))
            {
                Debug.LogError($"<color = red>[ObjectPoolSystem.GetObject] 해당 Object가 담긴 Pool이 존재하지 않습니다.</color>");
                return null;
            }

            if (pool is ObjectPool<TKey> typedPool)
            {
                return typedPool.GetObject();
            }
            else
            {
                Debug.LogError($"<color = red>[ObjectPoolSystem.GetObject] 해당 Object가 담긴 Pool을 변환하지 못했습니다.</color>");
                return null;
            }
        }

        public void ReturnObject<TKey>(TKey obj) where TKey : Component
        {
            if (!pools.TryGetValue(typeof(TKey), out var pool))
            {
                Debug.LogError($"<color = red>[ObjectPoolSystem.ReturnObject] 해당 Object가 담긴 Pool이 존재하지 않습니다.</color>");
                return;
            }

            if (pool is ObjectPool<TKey> typedPool)
            {
                typedPool.ReturnObject(obj);
                return;
            }
            else
            {
                Debug.LogError($"<color = red>[ObjectPoolSystem.ReturnObject] 해당 Object가 담긴 Pool을 변환하지 못했습니다.</color>");
                return;
            }
        }

        // --------------------------------------------------
        // Functions - Event
        // --------------------------------------------------
        private void Awake()
        {
            if (null == _instance)
            {
                var existingInstance = FindObjectOfType<ObjectPoolSystem>();

                if (existingInstance != null)
                {
                    _instance = existingInstance;
                    DontDestroyOnLoad(_instance.gameObject);
                }
            }
        }

        private void OnDestroy()
        {
            if (_isSingleton)
                _instance = null;
        }
    }
}