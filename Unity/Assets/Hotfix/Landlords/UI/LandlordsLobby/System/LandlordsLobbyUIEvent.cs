using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.LandlordsEnterLobby)]
    public class LandlordsLobbyUIEvent_CreateLobbyUI : AEvent
    {
        public override void Run()
        {
            UI ui = LandlordsLobbyFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }

    [Event(EventIdType.LandlordsExitLobby)]
    public class LandlordsLobbyUIEvent_DeleteLobbyUI : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsLobby);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.LandlordsLobby.StringToAB());
        }
    }
}