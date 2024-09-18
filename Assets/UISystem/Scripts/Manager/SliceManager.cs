namespace ModulerUISystem
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;
    public class SliceManager : MonoBehaviour
    {
        #region PUBLIC_VARS
        //Managers
        public AbstractCameraStackManager _cameraStackManager;
        public AbstractCanvasSortingOrderManager _canvasSortingOrderManager;
        //Dictionary
        public Dictionary<SliceConfig, Slice> slices;
        public Dictionary<SliceConfig, Slice> overlaySlices;
        #endregion

        #region PRIVATE_VARS
        #endregion

        #region UNITY_CALLBACKS
        private void Awake()
        {
            slices = new Dictionary<SliceConfig, Slice>();
            overlaySlices = new Dictionary<SliceConfig, Slice>();
        }

        #endregion

        #region PRIVATE_METHODS

        private bool CheckSliceAvailableInHierarchy(SliceConfig sliceConfig, ref Slice slice)
        {
            //iterate through contained slices
            foreach (var key in slices.Keys)
            {
                //Compare prefab instances
                if (key.slice.GetInstanceID() == sliceConfig.slice.GetInstanceID())
                {
                    slice = slices[key];
                    // Debug.Log("Slice Config found");
                    return true;
                }
            }
            
            // Debug.Log("Slice Config not found");
            return false;
        }
        private bool CheckSliceAvailableInHierarchy(SliceConfig sliceConfig, ref Slice slice,ref SliceConfig availableSliceConfig)
        {
            //iterate through contained slices
            foreach (var key in slices.Keys)
            {
                //Compare prefab instances
                if (key.slice.GetInstanceID() == sliceConfig.slice.GetInstanceID())
                {
                    availableSliceConfig = key;
                    slice = slices[key];
                    // Debug.Log("Slice Config found "+sliceConfig.slice.name);
                    return true;
                }
            }
            
            // Debug.Log("Slice Config not found"+sliceConfig.slice.name);
            return false;
        }
        private bool CheckOverlaySliceAvailableInHierarchy(SliceConfig sliceConfig, ref Slice slice)
        {
            //iterate through contained slices
            foreach (var key in overlaySlices.Keys)
            {
                //Compare prefab instances
                if (key.slice.GetInstanceID() == sliceConfig.slice.GetInstanceID())
                {
                    slice = overlaySlices[key];
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region PUBLIC_METHODS

        public List<Slice> AddSlices(List<SliceConfig> sliceConfigs)
        {
            //Catch slices items
            SliceManagerDataConfig sliceManagerDataConfig = new SliceManagerDataConfig(_cameraStackManager,_canvasSortingOrderManager);
            List<Slice> tempSlices = new List<Slice>();
            Slice tempSlice = null;
            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                //Check if slice available in the heirarch
                if (!CheckSliceAvailableInHierarchy(sliceConfig, ref tempSlice))
                {
                    //Create slice
                    Slice slice = Instantiate(sliceConfig.slice, transform);

                    //Add canvas and slices to list.
                    sliceManagerDataConfig.AddData(slice.cameraList,slice.canvasList);

                    //add slice item to dictionary
                    slices.Add(sliceConfig, slice);

                    //Slice
                    tempSlices.Add(slice);
                }
                else
                {
                    //Add canvas and slices.
                    sliceManagerDataConfig.AddData(tempSlice.cameraList,tempSlice.canvasList);
                }
            }
            // Debug.Log("Overlay slices : "+overlaySlices.Count);
            //Add overlay cameras
            foreach (var val in overlaySlices)
            {
                sliceManagerDataConfig.AddData(val.Value.cameraList,val.Value.canvasList);
            }
            
            //Ask manager to add canvases and cameras
            sliceManagerDataConfig.ExecuteAdd();
            return tempSlices;
        }
        public List<Slice> AddOverlaySlices(List<SliceConfig> sliceConfigs)
        {
            //Catch slices items
            SliceManagerDataConfig sliceManagerDataConfig = new SliceManagerDataConfig(_cameraStackManager,_canvasSortingOrderManager);
            List<Slice> tempSlices = new List<Slice>();
            Slice tempSlice = null;
            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                //Check if slice available in the heirarch
                if (!CheckOverlaySliceAvailableInHierarchy(sliceConfig, ref tempSlice))
                {
                    //Create slice
                    Slice slice = Instantiate(sliceConfig.slice, transform);

                    //Add canvas and slices to list.
                    sliceManagerDataConfig.AddData(slice.cameraList,slice.canvasList);

                    //add slice item to dictionary
                    overlaySlices.Add(sliceConfig, slice);

                    //Slice
                    tempSlices.Add(slice);
                }
                else
                {
                    //Add canvas and slices.
                    sliceManagerDataConfig.AddData(tempSlice.cameraList,tempSlice.canvasList);
                }
            }
            
            
            //Ask manager to add canvases and cameras
            sliceManagerDataConfig.ExecuteAdd();
            return tempSlices;
        }

        public void RemoveSlices(List<SliceConfig> sliceConfigs)
        {
            //Catch slices items
            SliceManagerDataConfig sliceManagerDataConfig = new SliceManagerDataConfig(_cameraStackManager,_canvasSortingOrderManager);
            List<Slice> sliceToDelete = new List<Slice>();

            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                if (slices.ContainsKey(sliceConfig))
                {
                    //GetSlice
                    Slice slice;
                    slices.TryGetValue(sliceConfig, out slice);

                    //Get cameras and canvases
                    sliceManagerDataConfig.AddData(slice.cameraList,slice.canvasList);
                    
                    //Add slice to delete list
                    sliceToDelete.Add(slice);

                    //Remove slice from dictionary
                    slices.Remove(sliceConfig);
                }
            }

            //Ask manager to remove canvases and cameras
            sliceManagerDataConfig.ExecuteRemove();

            //Delete the slices
            DestroySlices(sliceToDelete);
        }
        public void RemoveOverlaySlices(List<SliceConfig> sliceConfigs)
        {
            //Catch slices items
            SliceManagerDataConfig sliceManagerDataConfig = new SliceManagerDataConfig(_cameraStackManager,_canvasSortingOrderManager);
            List<Slice> sliceToDelete = new List<Slice>();

            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                if (overlaySlices.ContainsKey(sliceConfig))
                {
                    //GetSlice
                    Slice slice;
                    overlaySlices.TryGetValue(sliceConfig, out slice);

                    //Get cameras and canvases
                    sliceManagerDataConfig.AddData(slice.cameraList,slice.canvasList);
                    
                    //Add slice to delete list
                    sliceToDelete.Add(slice);

                    //Remove slice from dictionary
                    overlaySlices.Remove(sliceConfig);
                }
            }

            //Ask manager to remove canvases and cameras
            sliceManagerDataConfig.ExecuteRemove();

            //Delete the slices
            DestroySlices(sliceToDelete);
        }

        public List<SliceConfig> GetSliceConfigsWhichAreThereInList(List<SliceConfig> viewSliceConfigList)
        {
            List<SliceConfig> sliceConfigs = new List<SliceConfig>();
            foreach (var sliceConfig in viewSliceConfigList)
            {
                foreach (var tempSliceConfig in slices.Keys)
                {
                    //Compare prefab instances
                    if (tempSliceConfig.slice.GetInstanceID() == sliceConfig.slice.GetInstanceID())
                    {
                        sliceConfigs.Add(sliceConfig);
                    }
                }
            }
            return sliceConfigs;
        }
        
        public List<Slice> GetSlices(List<SliceConfig> sliceConfigs)
        {
            List<Slice> tempSlices = new List<Slice>();
            
            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                if (slices.ContainsKey(sliceConfig))
                {
                    //GetSlice
                    Slice tempSlice;
                    slices.TryGetValue(sliceConfig, out tempSlice);
                
                    //Add slice to delete list
                    tempSlices.Add(tempSlice);
                }
                
            }
            return tempSlices;
        }

        public List<Slice> GetSlices(ref List<SliceConfig> sliceConfigs)
        {
            List<Slice> tempSlices = new List<Slice>();
            Slice slice = null;
            SliceConfig tempSliceConfig=null;
            List<SliceConfig> tempSliceConfigs = new List<SliceConfig>();
            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                if (CheckSliceAvailableInHierarchy(sliceConfig, ref slice,ref tempSliceConfig))
                {
                    if (!sliceConfigs.Contains(tempSliceConfig))
                    {
                        tempSliceConfigs.Add(tempSliceConfig);
                    }
                    tempSlices.Add(slice);
                }
            }
            sliceConfigs.AddRange(tempSliceConfigs);
            return tempSlices;
        }
        public List<Slice> GetOverlaySlices(List<SliceConfig> sliceConfigs)
        {
            List<Slice> tempSlices = new List<Slice>();

            //iterating through the slice configs
            foreach (var sliceConfig in sliceConfigs)
            {
                if (overlaySlices.ContainsKey(sliceConfig))
                {
                    //GetSlice
                    Slice tempSlice;
                    overlaySlices.TryGetValue(sliceConfig, out tempSlice);

                    //Add slice to delete list
                    tempSlices.Add(tempSlice);
                }
            }
            return tempSlices;
        }

        public void DestroySlices(List<Slice> sliceToDelete)
        {
            for (int indexOfSlice = 0; indexOfSlice < sliceToDelete.Count; indexOfSlice++)
            {
                Destroy(sliceToDelete[indexOfSlice].gameObject);
            }
        }
        #endregion

        #region Class
        public class SliceManagerDataConfig
        {
            private AbstractCameraStackManager cameraStackManager;
            private AbstractCanvasSortingOrderManager canvasSortingOrderManager;
            private List<Camera> _cameras;
            private List<Canvas> _canvases;
            public SliceManagerDataConfig(AbstractCameraStackManager cameraStackManager, AbstractCanvasSortingOrderManager canvasSortingOrderManager)
            {
                this.cameraStackManager = cameraStackManager;
                this.canvasSortingOrderManager = canvasSortingOrderManager;
                _cameras = new List<Camera>();
                _canvases = new List<Canvas>();
            }

            public void ExecuteAdd()
            {
                cameraStackManager.AddCameras(_cameras);
                canvasSortingOrderManager.AddCanvases(_canvases);
            }

            public void ExecuteRemove()
            {
                cameraStackManager.RemoveCameras(_cameras);      
                canvasSortingOrderManager.RemoveCanvases(_canvases);
            }

            public void AddData(List<Camera> cameras, List<Canvas> canvases)
            {
                this._cameras.AddRange(cameras);
                this._canvases.AddRange(canvases);
            }
        }
        #endregion
    }
}