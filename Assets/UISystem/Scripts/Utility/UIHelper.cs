using System.Linq;

namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public static class UIHelper
    {
        public static SlicePanel[] GetSlicePanels(List<Slice> slices)
        {
            List<SlicePanel> slicePanels = new List<SlicePanel>();
            foreach (var slice in slices)
            {
                slicePanels.AddRange(slice.slicePanels);
            }

            return slicePanels.ToArray();
        }

        public static SlicePanel[] GetSliceDefaultPanels(List<Slice> slices)
        {
            List<SlicePanel> slicePanels = new List<SlicePanel>();
            foreach (var slice in slices)
            {
                slicePanels.AddRange(slice.animationTargets.GetDefaultSlicePanels());
            }

            return slicePanels.ToArray();
        }

        public static SlicePanel[] GetSlicePanels(Slice slice)
        {
            List<SlicePanel> slicePanels = new List<SlicePanel>();
            slicePanels.AddRange(slice.slicePanels);
            return slicePanels.ToArray();
        }

        public static INavigatorCallback[] GetNavigatorCallback(List<Slice> slices)
        {
            List<INavigatorCallback> navigatorCallbacks = new List<INavigatorCallback>();

            for (int indexOfSlice = 0; indexOfSlice < slices.Count; indexOfSlice++)
            {
                INavigatorCallback navigatorCallback = slices[indexOfSlice].GetComponent<INavigatorCallback>();
                if (navigatorCallback != null)
                {
                    navigatorCallbacks.Add(navigatorCallback);
                }
            }

            return navigatorCallbacks.ToArray();
        }

        public static INavigatorCallback[] GetNavigatorCallback(Slice slice)
        {
            List<INavigatorCallback> navigatorCallbacks = new List<INavigatorCallback>();

            INavigatorCallback navigatorCallback = slice.GetComponent<INavigatorCallback>();
            if (navigatorCallback != null)
            {
                navigatorCallbacks.Add(navigatorCallback);
            }

            return navigatorCallbacks.ToArray();
        }
    }
}