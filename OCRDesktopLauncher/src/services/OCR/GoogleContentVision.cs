using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

class GoogleContentVisionTransformRequestPayload
{
	public string EncodedImage { get; set; }
}

class GoogleContentVisionTransformRequest : ServiceRequest
{
	public static string ImagePath = @"tmp_capture.jpeg";

	internal override string URL
		{ get; set; } = "https://content-vision.googleapis.com/v1/images:annotate?alt=json&key=";

	internal override StringContent GetRequestPayload(object payloadData)
	{
		GoogleContentVisionRequestModel payload = new GoogleContentVisionRequestModel();
		payload.Model.Data.requests = new List<GoogleContentVisionRequestModel.RequestGroup>
		{
			new GoogleContentVisionRequestModel.RequestGroup
			{
				features = new List<GoogleContentVisionRequestModel.Feature>
				{
					new GoogleContentVisionRequestModel.Feature()
					{
						maxResults = 1,
						type = "TEXT_DETECTION"
					}
				},
				image = new GoogleContentVisionRequestModel.Image()
				{
					content = (payloadData as GoogleContentVisionTransformRequestPayload)
						.EncodedImage
				}
			}
		};

		return new StringContent(payload.Model.ToJSON());
	}

	internal override async Task<object> GetResponsePayload(HttpResponseMessage payloadData)
	{
		string responseContent = await payloadData.Content.ReadAsStringAsync();
		return new GoogleContentVisionResponseModel().Model.FromJSON(responseContent);
	}

	public GoogleContentVisionTransformRequest(string privateKey) : base(privateKey) { }

	public override async Task<object> Request()
	{
		GoogleContentVisionTransformRequestPayload requestPayload =
			new GoogleContentVisionTransformRequestPayload();

		string image = await ImageProcessing.EncodeImageToBase64(ImagePath);
		requestPayload.EncodedImage = image;

		HttpResponseMessage httpResponse =
			await HttpRequest.PostAsync(URL, GetRequestPayload(requestPayload));

		try
		{
			object responsePayload = await GetResponsePayload(httpResponse);
			
			return ((GoogleContentVisionResponseModel.Response)responsePayload)
				.responses[0].textAnnotations[0].description;
		}
		catch
		{
			return null;
		}
	}
}

class GoogleContentVision : Service
{
	public static GoogleContentVisionTransformRequest TransformIntoText { get; private set; }
	
	private static readonly string keyPath = @"google_key";

	static GoogleContentVision()
	{
		PrivateKey = File.ReadAllText(keyPath);
		
		TransformIntoText = new GoogleContentVisionTransformRequest(PrivateKey);
	}
}