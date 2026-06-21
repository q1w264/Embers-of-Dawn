using UnityEngine;

namespace Scriptable_objects
{
    [CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
    public class Character : ScriptableObject
    {
        public string characterName;
        public string description;
        public Sprite sprite;  // 角色图片
    }
}
