using System.Collections.Generic;
using UnityEngine;

namespace ToolSystem
{
    public class ToolRunner
    {
        private List<Tool> tools;
        private Tool selectedTool;

        public ToolRunner(params Tool[] tools)
        {
            this.tools = new List<Tool>(tools);
            ToggleAllTools(this.tools, false);
        }

        public void AddTool(Tool tool)
        {
            if (!tools.Contains(tool))
            {
                tools.Add(tool);
            }
        }

        public void RemoveTool(Tool tool)
        {
            if (tools.Contains(tool))
            {
                tools.Remove(tool);
            }
        }

        public void ExecuteTools()
        {
            if (selectedTool.Active && selectedTool.IsReadyToRun())
            {
                // Debug.Log("selected tool is running : "+selectedTool.Name);
                selectedTool.OnToolRunning();
            }
        }

        public void ToggleAllTools(List<Tool> tools, bool status)
        {
            foreach (var tool in tools)
            {
                tool.Active = status;
            }
        }

        public void ToggleCurrentSelectedTool(bool status)
        {
            selectedTool.Active = status;
        }

        public void UpdateSelectedTool(string toolName)
        {
            Tool targetTool = tools.Find(x => x.Name.Equals(toolName));

            if (targetTool == null)
                return;

            if (selectedTool == targetTool)
                return;

            if (selectedTool == null)
            {
                selectedTool = targetTool;
                selectedTool.Active = true;
                return;
            }

            selectedTool.Active = false;
            selectedTool = targetTool;
            selectedTool.Active = true;
        }
        public void DestroyTools()
        {
            foreach (var tool in tools)
            {
                tool.OnDestroy();
            }
            tools.Clear();
        }
    }
}