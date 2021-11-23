using ETModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETHotfix
{
    [Event(EventIdType.LandlordsEnterLogin)]
    public class LandlordsLoginUIEvent_CreateLoginUI : AEvent
    {
        public override void Run()
        {
            UI ui = LandlordsLoginFactory.Create();
            Game.Scene.GetComponent<UIComponent>().Add(ui);
        }
    }

    [Event(EventIdType.LandlordsExitLogin)]
    public class LandlordsLoginUIEvent_RemoveLoginUI : AEvent
    {
        public override void Run()
        {
            Game.Scene.GetComponent<UIComponent>().Remove(UIType.LandlordsLogin);
            ETModel.Game.Scene.GetComponent<ResourcesComponent>().UnloadBundle(UIType.LandlordsLogin.StringToAB());
        }
    }
}