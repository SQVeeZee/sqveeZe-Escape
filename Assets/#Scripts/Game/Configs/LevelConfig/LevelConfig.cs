using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[CreateAssetMenu(fileName = "LevelConfig", menuName = MenuPath, order = MenuOrder)]

public class LevelConfig : ScriptableObject, ILevelConfig
{
        private const string MenuPath = "Configs/LevelConfig";
        private const int MenuOrder = int.MinValue + 221;

        [Header("ITEM")]
        [SerializeField] protected string _name = null;

        [Header("PREFABS")]
        [SerializeField] private GameObject _levelPrefab = null;

        protected int _id = -1;

        protected int _levelIndex = 0;

        protected int _levelCompletedCount = 0;
        protected int _iterationIndex = 0;

        protected int _locationLevelIndex = 0;


        public int GetId => _id;
        public string GetName => _name;

        public int GetLevelIndex => _levelIndex;

        public int GetLevelCompletedCount => _levelCompletedCount;
        public int GetIterationIndex => _iterationIndex;
        public int GetLocationLevelIndex => _locationLevelIndex;

        public GameObject GetLevelPrefab => _levelPrefab;


        public virtual void SetId(int id)
        {
            if (_id >= 0)
            {
                return;
            }

            _id = id;
        }

        public virtual void SetLocationLevelIndex(int locationLevelIndex)
        {
            _locationLevelIndex = locationLevelIndex;
        }

        public virtual void SetCompletedCount(int completedCount)
        {
            _levelCompletedCount = completedCount;
        }

        public virtual void SetIterationIndex(int iterationIndex)
        {
            _iterationIndex = iterationIndex;
        }

        public virtual void SetLevelIndex(int levelIndex)
        {
            _levelIndex = levelIndex;
        }
}
