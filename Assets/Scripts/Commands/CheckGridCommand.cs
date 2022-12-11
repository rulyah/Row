using System.Collections.Generic;
using Configs;
using UnityEngine;

namespace Commands
{
    public static class CheckGridCommand
    {
        public static void CheckGrid(Core core, List<Slot> checkedSlots)
        {
            Debug.Log("CheckGridCommand");
            CheckLine(core, checkedSlots);
            CheckColumn(core, checkedSlots);
        }
        private static void CheckLine(Core core, List<Slot> checkedSlots)
        {
            for (var y = 1; y <= GameConfig.gridSize; y++)
            {
                checkedSlots.Clear();
                var slots = core.slots.FindAll(n => n.posY == y);
                for (var x = 0; x < slots.Count - 1; x++)
                {
                    var next = x + 1;

                    if (slots[x].itemInSlot.spriteId == slots[next].itemInSlot.spriteId)
                    {
                        if(!checkedSlots.Contains(slots[x])) checkedSlots.Add(slots[x]);
                        if(!checkedSlots.Contains(slots[next])) checkedSlots.Add(slots[next]);
                    }
                    else
                    {
                        if(checkedSlots.Count < 3) checkedSlots.Clear();
                        else
                        {
                            for (var i = 0; i < checkedSlots.Count; i++)
                            {
                                Model.levelModel.matchList.Add(checkedSlots[i]);
                            }
                            checkedSlots.Clear();
                        }
                    }
                }
            }
        }

        private static void CheckColumn(Core core, List<Slot> checkedSlots)
        {
            for (var x = 1; x <= GameConfig.gridSize; x++)
            {
                checkedSlots.Clear();
                var slots = core.slots.FindAll(n => n.posX == x && n.posY <= GameConfig.gridSize);
                for (var y = 0; y < slots.Count - 1; y++)
                {
                    var next = y + 1;
                    if (slots[y].itemInSlot.spriteId == slots[next].itemInSlot.spriteId)
                    {
                        if(!checkedSlots.Contains(slots[y])) checkedSlots.Add(slots[y]);
                        if(!checkedSlots.Contains(slots[next])) checkedSlots.Add(slots[next]);
                    }
                    else
                    {
                        if(checkedSlots.Count < 3) checkedSlots.Clear();
                        else
                        {
                            for (var i = 0; i < checkedSlots.Count; i++)
                            {
                                Model.levelModel.matchList.Add(checkedSlots[i]); 
                            }
                            checkedSlots.Clear();
                        }
                    }
                }
            }
        }
    }
}