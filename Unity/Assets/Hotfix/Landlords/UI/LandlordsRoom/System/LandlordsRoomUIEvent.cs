using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETHotfix
{
    [Event(EventIdType.LandlordsEnterRoom)]
    public class LandlordsRoomUIEvent_CreateRoomUI : AEvent
    {
        public override void Run()
        {
            UI ui = LandlordsRoomFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }

    [Event(EventIdType.LandlordsExitRoom)]
    public class LandlordsRoomUIEvent_DeleteRoomUI : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsRoom);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.LandlordsRoom.StringToAB());
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(HandCardsComponent.HANDCARD_NAME.StringToAB());
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(HandCardsComponent.PLAYCARD_NAME.StringToAB());
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(CardHelper.ATLAS_NAME.StringToAB());
        }
    }
}
