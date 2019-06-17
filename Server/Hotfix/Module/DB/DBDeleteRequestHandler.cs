using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.DB)]
	public class DBDeleteRequestHandler : AMRpcHandler<DBDeleteRequest, DBDeleteResponse>
	{
		protected override void Run(Session session, DBDeleteRequest message, Action<DBDeleteResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}
		
		protected async ETVoid RunAsync(Session session, DBDeleteRequest message, Action<DBDeleteResponse> reply)
		{
			DBDeleteResponse response = new DBDeleteResponse();
			try
			{
				response.deleteCount = await Game.Scene.GetComponent<DBComponent>().Delete(message.CollectionName, message.Id);

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}