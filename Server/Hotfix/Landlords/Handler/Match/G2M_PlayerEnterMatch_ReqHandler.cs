using System;
using ETModel;
using System.Threading.Tasks;

namespace ETHotfix
{
    [MessageHandler(AppType.Match)]
    public class G2M_PlayerEnterMatch_ReqHandler : AMRpcHandler<G2M_PlayerEnterMatch_Req, M2G_PlayerEnterMatch_Ack>
    {
        protected override async ETTask Run(Session session, G2M_PlayerEnterMatch_Req request, M2G_PlayerEnterMatch_Ack response, Action reply)
        {
            MatchComponent matchComponent = Game.Scene.GetComponent<MatchComponent>();
            ActorMessageSenderComponent actorProxyComponent = Game.Scene.GetComponent<ActorMessageSenderComponent>();

            if (matchComponent.Playing.ContainsKey(request.UserID))
            {
                MatchRoomComponent matchRoomComponent = Game.Scene.GetComponent<MatchRoomComponent>();
                long roomId = matchComponent.Playing[request.UserID];
                Room room = matchRoomComponent.Get(roomId);
                Gamer gamer = room.Get(request.UserID);

                //重置GateActorID
                gamer.PlayerID = request.PlayerID;

                //重连房间
                ActorMessageSender actorProxy = actorProxyComponent.Get(roomId);
                await actorProxy.Call(new Actor_PlayerEnterRoom_Req()
                {
                    PlayerID = request.PlayerID,
                    UserID = request.UserID,
                    SessionID = request.SessionID
                });

                //向玩家发送匹配成功消息
                ActorMessageSender gamerActorProxy = actorProxyComponent.Get(gamer.PlayerID);
                gamerActorProxy.Send(new Actor_MatchSucess_Ntt() { GamerID = gamer.Id });
            }
            else
            {
                //创建匹配玩家
                Matcher matcher = MatcherFactory.Create(request.PlayerID, request.UserID, request.SessionID);
            }

            reply();
        }
    }
}
