using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace ETModel
{
	[ObjectSystem]
	public class DBDeleteBatchTaskSystem : AwakeSystem<DBDeleteBatchTask, List<long>, string, ETTaskCompletionSource<long>>
	{
		public override void Awake(DBDeleteBatchTask self, List<long> idList, string collectionName, ETTaskCompletionSource<long> tcs)
		{
			self.IdList = idList;
			self.CollectionName = collectionName;
			self.Tcs = tcs;
		}
	}

	public sealed class DBDeleteBatchTask : DBTask
	{
		public string CollectionName { get; set; }

		public List<long> IdList { get; set; }

		public ETTaskCompletionSource<long> Tcs { get; set; }
		
		public override async ETTask Run()
		{
			DBComponent dbComponent = Game.Scene.GetComponent<DBComponent>();
			long deleteCount = 0;

			try
			{
				// 执行删除数据库任务
				foreach (long id in IdList)
				{
					DeleteResult deleteResult = await dbComponent.GetCollection(this.CollectionName).DeleteManyAsync((s) => s.Id == id);
					deleteCount += deleteResult.DeletedCount;
				}
				
				this.Tcs.SetResult(deleteCount);
			}
			catch (Exception e)
			{
				this.Tcs.SetException(new Exception($"删除数据库异常! {this.CollectionName} {IdList.ListToString()}", e));
			}
		}
	}
}