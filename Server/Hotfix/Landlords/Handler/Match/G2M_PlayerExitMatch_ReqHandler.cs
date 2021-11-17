using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Match)]
    public class G2M_PlayerExitMatch_ReqHandler : AMRpcHandler<G2M_PlayerExitMatch_Req, M2G_PlayerExitMatch_Ack>
    {
        protected override async ETTask Run(Session session, G2M_PlayerExitMatch_Req request, M2G_PlayerExitMatch_Ack response, Action reply)
        {
            Matcher matcher = Game.Scene.GetComponent<MatcherComponent>().Remove(request.UserID);
            matcher?.Dispose();
            Log.Info($"玩家{request.UserID}退出匹配队列");

            reply();
            await ETTask.CompletedTask;
        }
    }
}
