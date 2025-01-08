using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

namespace Game.StageScene.Player
{
    public static class AnimationManager
    {
        public static AnimationCurve[] GetAnimationCurve(AnimationClip animation)
        {
#if UNITY_EDITOR

            if (animation == null)
            {
                DebugManager.LogMessage($"{animation.name} Clip�����݂��܂���", DebugManager.MessageType.Error);

                return null;
            }

            var bindings = AnimationUtility.GetCurveBindings(animation);

            AnimationCurve[] curve = new AnimationCurve[bindings.Length];

            for (int i = 0; i < bindings.Length; i++)
            {
                // �e�J�[�u���擾
                curve[i] = AnimationUtility.GetEditorCurve(animation, bindings[i]);
            }

            return curve;

#else
            return null;

#endif
        }
    }
}