using UnityEngine;

namespace Scriptable_objects
{
    /// <summary>
    /// Character presentation data used by UI or gameplay systems.
    /// </summary>
    [CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
    public class Character : ScriptableObject
    {
        /// <summary>
        /// Display name of the character.
        /// </summary>
        public string characterName;

        /// <summary>
        /// Description text shown in UI.
        /// </summary>
        public string description;

        /// <summary>
        /// Portrait or representative sprite for the character.
        /// </summary>
        public Sprite sprite;
    }
}
