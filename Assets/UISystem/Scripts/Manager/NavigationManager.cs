namespace ModulerUISystem
{
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    using System.Linq;

    public class NavigationManager : MonoBehaviour
    {
        #region PUBLIC_VARS

        #endregion

        #region PRIVATE_VARS

        private Dictionary<Navigator, Action[]> navigators = new Dictionary<Navigator, Action[]>();
        private List<Navigator> navigatorsToRemove = new List<Navigator>();

        #endregion

        #region UNITY_CALLBACKS

        private void FixedUpdate()
        {
            if (navigatorsToRemove.Count == 0 && navigators.Count == 0)
            {
                UIManager.instance.ToggleInteractionBlocker(false);
                return;
            }

            UIManager.instance.ToggleInteractionBlocker(ShouldBlockInteraction());
            foreach (var navigator in navigators.Keys)
            {
                if (navigator.currentStage == TransitionState.Completed)
                {
                    navigatorsToRemove.Add(navigator);
                    continue;
                }

                navigator.Navigate();
            }

            foreach (var navigator in navigatorsToRemove)
            {
                Utility.ExecuteActions(navigators[navigator]);
                navigators.Remove(navigator);
            }

            navigatorsToRemove.Clear();
        }

        #endregion

        #region PRIVATE_METHODS

        private bool ShouldBlockInteraction()
        {
            for (int indexOfNavigator = 0; indexOfNavigator < navigators.Keys.Count; indexOfNavigator++)
            {
                if (navigators.ElementAt(indexOfNavigator).Key.ShouldBlockInteraction)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region PUBLIC_METHODS

        public void AddNavigation(TransitionConfig[] transitionConfigs,
            TransitionType transitionType, List<Slice> slices, bool shouldBlockInteraction, params Action[] actions)
        {
            List<Transition> transitions = new List<Transition>();
            int indexOfMaxDurationTransition = 0;
            float duration = Mathf.NegativeInfinity;

            for (int indexOfTransitionConfig = 0;
                indexOfTransitionConfig < transitionConfigs.Length;
                indexOfTransitionConfig++)
            {
                Transition transition = transitionConfigs[indexOfTransitionConfig].GetTransition(transitionType,
                    UIHelper.GetSlicePanels(slices[indexOfTransitionConfig]), slices.ToArray());
                if (duration < transition.duration)
                {
                    indexOfMaxDurationTransition = indexOfTransitionConfig;
                }

                transitions.Add(transition);
            }

            float numberOfFramesToAdvance = (TransitionType.Show == transitionType) ? 12f : 0f;
            //Add them to navigator
            for (int indexOfTransitionConfig = 0;
                indexOfTransitionConfig < transitionConfigs.Length;
                indexOfTransitionConfig++)
            {
                Navigator navigator = new Navigator(transitions[indexOfTransitionConfig], transitionType,
                    shouldBlockInteraction, numberOfFramesToAdvance,UIHelper.GetNavigatorCallback(slices[indexOfTransitionConfig]));
                if (indexOfMaxDurationTransition == indexOfTransitionConfig)
                {
                    navigators.Add(navigator, actions);
                    continue;
                }

                navigators.Add(navigator, null);
            }
        }

        #endregion
    }
}