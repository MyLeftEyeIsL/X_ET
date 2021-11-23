using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    public class LandlordsEndFactory
    {
        public static UI Create(UI parent, bool isWin)
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.LandlordsEnd.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.LandlordsEnd.StringToAB(), UIType.LandlordsEnd);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.LandlordsEnd, gameObject, false);
                parent.Add(ui);
                ui.GameObject.transform.SetParent(parent.GameObject.transform, false);

                ui.AddComponent<LandlordsEndComponent, bool>(isWin);
                return ui;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return null;
            }
        }
    }
}
