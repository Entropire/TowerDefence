using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "ScriptableObjects", menuName = "ScriptableObjects/EnemyPath")]
    public class EnemyPath : ScriptableObject
    {
        public List<Vector2Int> path;
    }
}