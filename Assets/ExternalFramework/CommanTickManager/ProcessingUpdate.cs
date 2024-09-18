using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CommanTickManager
{

    public class ProcessingUpdate : MonoBehaviour
    {
        private List<ITick> ticks = new List<ITick>(25);
        private List<IFixedTick> fixedTicks = new List<IFixedTick>();
        private List<ILateTick> lateTicks = new List<ILateTick>();
        private static ProcessingUpdate processingUpdate;
        private int countTicks;
        private int countTicksFixed;
        private int countTicksLate;
        public static ProcessingUpdate Instance
        {
            get
            {
                return processingUpdate;
            }
        }
        void Awake()
        {
            processingUpdate = this;
        }
        public void Add(object updateble)
        {
            var tickable = updateble as ITick;
            if (tickable != null)
            {
                ticks.Add(tickable);
                countTicks++;
            }

            var tickableFixed = updateble as IFixedTick;
            if (tickableFixed != null)
            {
                fixedTicks.Add(tickableFixed);
                countTicksFixed++;
            }

            var tickableLate = updateble as ILateTick;
            if (tickableLate != null)
            {
                lateTicks.Add(tickableLate);
                countTicksLate++;
            }
        }
        public void Remove(object updateble)
        {
            if (ticks.Remove(updateble as ITick))
            {
                countTicks--;
            }
            if (lateTicks.Remove(updateble as ILateTick))
            {
                countTicksLate--;
            }
            if (fixedTicks.Remove(updateble as IFixedTick))
            {
                countTicksFixed--;
            }
        }
        private void Update()
        {
            for (var i = 0; i < countTicks; i++)
            {
                ticks[i].Tick();
            }
        }
        private void FixedUpdate()
        {
            for (var i = 0; i < countTicksFixed; i++)
            {
                fixedTicks[i].FixedTick();
            }
        }
        private void LateUpdate()
        {
            for (var i = 0; i < countTicksLate; i++)
            {
                lateTicks[i].LateTick();
            }
        }
    }
}