using System.Net.Http;
using System.Threading.Tasks;

abstract class ServiceRequest
{
	internal static readonly HttpClient HttpRequest = new HttpClient();

	internal abstract string URL { get; set; }

	internal abstract StringContent GetRequestPayload(object payloadData);

	internal abstract Task<object> GetResponsePayload(HttpResponseMessage payloadData);

	public ServiceRequest(string privateKey)
	{
		URL += privateKey;
	}

	public ServiceRequest() { }

	static ServiceRequest()
	{
		HttpRequest.DefaultRequestHeaders
			.Add("user-agent", "Google-API-Java-Client Google-HTTP-Java-Client/1.21.0 (gzip)");
	}

	public abstract Task<object> Request();
}