using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_GetUserInfo_ReqHandler : AMRpcHandler<C2G_GetUserInfo_Req, G2C_GetUserInfo_Ack>
    {
        protected override async ETTask Run(Session session, C2G_GetUserInfo_Req request, G2C_GetUserInfo_Ack response, Action reply)
        {
            //验证Session
            if (!GateHelper.SignSession(session))
            {
                response.Error = ErrorCode.ERR_SignError;
                reply();
                return;
            }

            //查询用户信息
            DBProxyComponent dbProxyComponent = Game.Scene.GetComponent<DBProxyComponent>();
            UserInfo userInfo = await dbProxyComponent.Query<UserInfo>(request.UserID, false);

            response.NickName = userInfo.NickName;
            response.Wins = userInfo.Wins;
            response.Loses = userInfo.Loses;
            response.Money = userInfo.Money;

            reply();
        }
    }
}
