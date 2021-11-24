using ETModel;
using System;
using UnityEngine;

namespace ETHotfix
{
    public class LandlordsRoomFactory
    {
        public static UI Create()
        {
			try
			{
				ResourcesComponent resourcesComponent = ETModel.Game.Scene.GetComponent<ResourcesComponent>();
				resourcesComponent.LoadBundle(UIType.LandlordsRoom.StringToAB());
				resourcesComponent.LoadBundle(HandCardsComponent.HANDCARD_NAME.StringToAB());
				resourcesComponent.LoadBundle(HandCardsComponent.PLAYCARD_NAME.StringToAB());
				resourcesComponent.LoadBundle(CardHelper.ATLAS_NAME.StringToAB());
				GameObject bundleGameObject = (GameObject)resourcesComponent.GetAsset(UIType.LandlordsRoom.StringToAB(), UIType.LandlordsRoom);
				GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject);

				UI ui = ComponentFactory.Create<UI, string, GameObject>(UIType.LandlordsRoom, gameObject, false);

				ui.AddComponent<GamerComponent>();
				ui.AddComponent<LandlordsRoomComponent>();
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