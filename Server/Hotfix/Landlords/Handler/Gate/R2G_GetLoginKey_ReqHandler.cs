using ETModel;
using System;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class R2G_GetLoginKey_ReqHandler : AMRpcHandler<R2G_GetLoginKey_Req, G2R_GetLoginKey_Ack>
    {
        protected override async ETTask Run(Session session, R2G_GetLoginKey_Req request, G2R_GetLoginKey_Ack response, Action reply)
        {
            long key = RandomHelper.RandInt64();
            Game.Scene.GetComponent<LandlordsGateSessionKeyComponent>().Add(key, request.UserID);
            response.Key = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
