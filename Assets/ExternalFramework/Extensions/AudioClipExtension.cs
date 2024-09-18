using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Games.UnnamedArcade3d.Entities.LittleRed
{
    public static class AudioClipExtension
    {
        public static float GetTotalTime(this AudioClip[] audioClips)
        {
            float totalTime = 0f;
            foreach (var clip in audioClips)
            {
                totalTime += clip.length;
            }

            return totalTime;
        }
        public static float GetTotalTime(this List<AudioClip> audioClips)
        {
            float totalTime = 0f;
            foreach (var clip in audioClips)
            {
                totalTime += clip.length;
            }

            return totalTime;
        }
    }
}
