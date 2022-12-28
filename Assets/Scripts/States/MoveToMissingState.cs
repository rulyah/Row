namespace States
{
    public class MoveToMissingState : State<Core>
    {
        public MoveToMissingState(Core core) : base(core) { }

        public override void OnEnter()
        {
            while (true)
            {
                var slot = _core.slots.Find(n => n.itemInSlot == null);
                if(slot != null) MoveDownItems(slot);
                if(slot == null) break;
            }
            ChangeState(new MoveItemsState(_core));
        }
        
        private void MoveDownItems(Slot slot)
        {
            if (slot == null) return;
            if (slot.posY == 13)
            {
                slot.itemInSlot = Factory.SetItem();
                return;
            }
            var upperSlot = _core.slots.Find(n => n.posX == slot.posX && n.posY == slot.posY + 1);
            if(upperSlot.itemInSlot != null) upperSlot.itemInSlot.SetParent(slot);
            MoveDownItems(upperSlot);
        }
    }
}