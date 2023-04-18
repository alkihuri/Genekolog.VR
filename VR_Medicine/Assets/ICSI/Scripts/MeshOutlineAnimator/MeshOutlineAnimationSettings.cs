using UnityEngine;

namespace ICSI.Scripts
{
    [CreateAssetMenu(fileName = "mesh_outline_animation_settings", menuName = "Create outline settings", order = 0)]
    public class MeshOutlineAnimationSettings : ScriptableObject
    {
        public AnimationCurve ValueCurve;
        public float BotValue;
        public float TopValue;
        public float TimeToFade;
    }
}