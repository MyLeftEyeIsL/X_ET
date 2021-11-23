using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    public class LandlordsInteractionFactory
    {
        public static UI Create(UI parent)
        {
            try
            {
                ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
                resourcesComponent.LoadBundle(UIType.LandlordsInteraction.StringToAB());
                GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.LandlordsInteraction.StringToAB(), UIType.LandlordsInteraction);
                GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

                UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.LandlordsInteraction, gameObject, false);
                parent.Add(ui);
                ui.GameObject.transform.SetParent(parent.GameObject.transform, false);

                ui.AddComponent<LandlordsInteractionComponent>();
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
