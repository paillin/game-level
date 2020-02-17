
using UnityEngine;

public static class PhysicsHelper
{
    public const float GravityStrength = 9.80665f;
}

public static class AnimationHelper
{
    public static float GetClipLength(this Animator animator, string animationName)
    {
        float time = float.NaN;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name.Equals(animationName))
                time = ac.animationClips[i].length;

        return time;
    }
}
