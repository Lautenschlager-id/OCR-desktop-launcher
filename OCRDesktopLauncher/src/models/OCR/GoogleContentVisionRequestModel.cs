using System.Collections.Generic;
using System.Runtime.Serialization;

class GoogleContentVisionRequestModel
{
	public Model<Request> Model { get; private set; }

	[DataContract]
	public struct Image
	{
		[DataMember]
		public string content { get; set; }
	}

	[DataContract]
	public struct Feature
	{
		[DataMember]
		public int maxResults { get; set; }
		[DataMember]
		public string type { get; set; }
	}

	[DataContract]
	public struct RequestGroup
	{
		[DataMember]
		public List<Feature> features { get; set; }
		[DataMember]
		public Image image { get; set; }
	}

	[DataContract]
	public struct Request
	{
		[DataMember]
		public List<RequestGroup> requests { get; set; }
	}

	public GoogleContentVisionRequestModel()
	{
		Model = new Model<Request>()
		{
			Data = new Request()
		};
	}
}