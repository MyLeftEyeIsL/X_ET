using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_PlayerOnline_ReqHandler : AMRpcHandler<G2R_PlayerOnline_Req, R2G_PlayerOnline_Ack>
    {
        protected override async ETTask Run(Session session, G2R_PlayerOnline_Req request, R2G_PlayerOnline_Ack response, Action reply)
        {
            OnlineComponent onlineComponent = Game.Scene.GetComponent<OnlineComponent>();

            //将已在线玩家踢下线
            await RealmHelper.KickOutPlayer(request.UserID);

            //玩家上线
            onlineComponent.Add(request.UserID, request.GateAppID);
            Log.Info($"玩家{request.UserID}上线");

            reply();
        }
    }
}
