namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class AnimationConstants
    {
        public static int instantIn = Animator.StringToHash("InstantIn");
        public static int instantOut = Animator.StringToHash("InstantOut");
        public static int popInDefault = Animator.StringToHash("PopInDefault");
        public static int popOutDefault = Animator.StringToHash("PopOutDefault");
    }
}