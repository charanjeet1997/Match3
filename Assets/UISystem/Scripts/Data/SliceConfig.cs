namespace ModulerUISystem
{
    using System;

    [Serializable]
    public class SliceConfig
    {
        public Slice slice;
        public bool overrideViewShowTransition;
        public bool overrideViewHideTransition;
        public TransitionConfig showTransitionConfig;
        public TransitionConfig hideTransitionConfig;

        public string sliceName
        {
            get
            {
                return slice.name;
            }
        }
    }
}