using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class MH2MP_CreateRoom_ReqHandler : AMRpcHandler<MH2MP_CreateRoom_Req, MP2MH_CreateRoom_Ack>
    {
        protected override async ETTask Run(Session session, MH2MP_CreateRoom_Req request, MP2MH_CreateRoom_Ack response, Action reply)
        {
            //创建房间
            Room room = ComponentFactory.Create<Room>();
            room.AddComponent<DeckComponent>();
            room.AddComponent<DeskCardsCacheComponent>();
            room.AddComponent<OrderControllerComponent>();
            room.AddComponent<GameControllerComponent, RoomConfig>(RoomHelper.GetConfig(RoomLevel.Lv100));
            await room.AddComponent<MailBoxComponent>().AddLocation();
            Game.Scene.GetComponent<RoomComponent>().Add(room);

            Log.Info($"创建房间{room.InstanceId}");

            response.RoomID = room.InstanceId;

            reply();
        }
    }
}
