//using MonsterErecruitSearchAPI.Infrastructure.LogModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TokenManagerOAUTH
{
    public static class TokenGateway
    {
        static TokenGateway()
        {
        }

        public static TokenRecord GetNewOAuthToken(TokenRequestModel tr)
        {
            RestClient client = new RestSharp.RestClient(tr.TokenURL);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", tr.OAuthCreds.ClientId);
            request.AddParameter("client_secret", tr.OAuthCreds.ClientSecret);
            if (!String.IsNullOrEmpty(tr.Scope))
            {
                request.AddParameter("scope", "GatewayAccess");
            }
            request.AddParameter("grant_type", "client_credentials");
            var tResponse = client.Execute(request);
            var responseJson = tResponse.Content;
            TokenModel t = TokenModel.TokenFromJson(responseJson);

            TokenRecord record = new TokenRecord(t, tr);
            return record;

        }
        //public static IRestResponse GetRecord(string endpoint, string accessToken)
        //{
        //    var client = new RestSharp.RestClient(endpoint);
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("Authorization", "Bearer " + accessToken);
        //    IRestResponse response = client.Execute(request);
        //    return response;
        //}


        //public static string GetMonsterSearchResults(string argSearchQuery, string accessToken, int pageId, int perPage)
        //{
        //    var client = new RestClient($"https://api.jobs.com/v2/candidates/queries?page={pageId}&perPage={perPage}");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Postman-Token", "bf42d0b5-44a3-49be-a0ae-d6841810d042");
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("Authorization", $"Bearer {accessToken}");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Accept", "application/json");
        //    request.AddParameter("undefined", $"{argSearchQuery}", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    return response.Content;
        //}

        //public static string PostRecord(string endpoint, Workflows.Models.WorkflowRequestModel model, string message)
        //{
        //    string token = Infrastructure.Datastores.OAuthTokenManager.GetErecruitOAuthToken(model).AccessToken;

        //    var client = new RestClient("https://portaltest.arch-sc.com/WebAPI/Note");
        //    var request = new RestRequest(Method.POST);
        //    request.AddHeader("Postman-Token", "a6ade277-ce57-4ba5-9193-ed1d95b93d7d");
        //    request.AddHeader("cache-control", "no-cache");
        //    request.AddHeader("Authorization", $"Bearer {token}");
        //    request.AddHeader("Content-Type", "application/json");
        //    request.AddHeader("Cache-Control", "no-cache");
        //    request.AddHeader("Accept", "application/json");
        //    request.AddParameter("undefined", message, ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);

        //    return "x";
        //}
        //public static string GetFileAsBase64String(string token, string endPoint)
        //{
        //    //byte[] pdfBytes = File.ReadAllBytes(pdfPath);
        //    //string pdfBase64 = Convert.ToBase64String(pdfBytes);


        //    var client = new RestClient(endPoint);
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");

        //    request.AddHeader("Authorization", "Bearer " + token);
        //    request.AddHeader("Content-Type", "application/pdf");
        //    IRestResponse response = client.Execute(request);
        //    var fileBytes = client.DownloadData(request);


        //    //string p = Path.Combine(System.IO.Path.GetTempPath(), "Response.pdf");
        //    //File.WriteAllBytes(p, fileBytes);

        //    String fileBase64String = Convert.ToBase64String(fileBytes);

        //    return fileBase64String;


        //}

    }
}

