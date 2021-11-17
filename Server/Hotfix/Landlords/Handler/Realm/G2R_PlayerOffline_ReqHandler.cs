using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class G2R_PlayerOffline_ReqHandler : AMRpcHandler<G2R_PlayerOffline_Req, R2G_PlayerOffline_Ack>
    {
        protected override async ETTask Run(Session session, G2R_PlayerOffline_Req request, R2G_PlayerOffline_Ack response, Action reply)
        {
            //玩家下线
            Game.Scene.GetComponent<OnlineComponent>().Remove(request.UserID);
            Log.Info($"玩家{request.UserID}下线");

            reply();
            await ETTask.CompletedTask;
        }
    }
}
