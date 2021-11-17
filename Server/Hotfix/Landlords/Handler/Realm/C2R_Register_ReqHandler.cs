using System;
using ETModel;
using System.Collections.Generic;

namespace ETHotfix
{
    [MessageHandler(AppType.Realm)]
    public class C2R_Register_ReqHandler : AMRpcHandler<C2R_Register_Req, R2C_Register_Ack>
    {
        protected override async ETTask Run(Session session, C2R_Register_Req request, R2C_Register_Ack response, Action reply)
        {
            //数据库操作对象
            DBProxyComponent dbProxy = Game.Scene.GetComponent<DBProxyComponent>();

            //查询账号是否存在
            List<ComponentWithId> result = await dbProxy.Query<AccountInfo>(_account => _account.Account == request.Account);
            if (result.Count > 0)
            {
                response.Error = ErrorCode.ERR_AccountAlreadyRegister;
                reply();
                return;
            }

            //新建账号
            AccountInfo newAccount = ComponentFactory.CreateWithId<AccountInfo>(IdGenerater.GenerateId());
            newAccount.Account = request.Account;
            newAccount.Password = request.Password;

            Log.Info($"注册新账号：{MongoHelper.ToJson(newAccount)}");

            //新建用户信息
            UserInfo newUser = ComponentFactory.CreateWithId<UserInfo>(newAccount.Id);
            newUser.NickName = $"用户{request.Account}";
            newUser.Money = 10000;

            //保存到数据库
            await dbProxy.Save(newAccount);
            await dbProxy.Save(newUser);

            reply();
        }
    }
}
