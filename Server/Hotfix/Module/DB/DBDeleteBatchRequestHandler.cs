using System;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.DB)]
	public class DBDeleteBatchRequestHandler : AMRpcHandler<DBDeleteBatchRequest, DBDeleteBatchResponse>
	{
		protected override void Run(Session session, DBDeleteBatchRequest message, Action<DBDeleteBatchResponse> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}
		
		protected async ETVoid RunAsync(Session session, DBDeleteBatchRequest message, Action<DBDeleteBatchResponse> reply)
		{
			DBDeleteBatchResponse response = new DBDeleteBatchResponse();
			try
			{
				DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
				response.deleteCount = await dbComponent.DeleteBatch(message.CollectionName, message.IdList);

				reply(response);
			}
			catch (Exception e)
			{
				ReplyError(response, e, reply);
			}
		}
	}
}