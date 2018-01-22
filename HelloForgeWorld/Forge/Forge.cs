#region Copyright
//
// Copyright (C) 2015-2018 by Autodesk, Inc.
//
// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.
//
// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK, INC.
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//
// Use, duplication, or disclosure by the U.S. Government is subject to
// restrictions set forth in FAR 52.227-19 (Commercial Computer
// Software - Restricted Rights) and DFAR 252.227-7013(c)(1)(ii)
// (Rights in Technical Data and Computer Software), as applicable.
//
// Written by M.Harada.  
// 
#endregion // Copyright

using System.Net;           // for HttpStatusCode 
using System.Configuration; // for Configuration. Add reference to System.Configuration.
using System.Diagnostics;   // for Debug writing 
// Added for RestSharp 
using RestSharp;
using RestSharp.Deserializers;

///=============================================================
/// Welcome to Forge API. 
/// 
/// Forge class defines/constructs REST API calls to Forge web services.
/// 
/// You can find the API documentation at the following site:
/// https://developer.autodesk.com/
///
/// We are using a library called RestSharp in this lab for 
/// simplicity and to focus on Forge specific API. 
///=============================================================
namespace HelloForgeWorld
{
    class Forge
    {
        // Set values specific to your environments.

        // To Do: set your own configuration in App.config file.
        private static string baseApiUrl = ConfigurationManager.AppSettings["baseApiUrl"];
        private static string clientID = ConfigurationManager.AppSettings["clientID"];
        private static string clientSecret = ConfigurationManager.AppSettings["clientSecret"];

        // Member variables 
        // Save the last response. This is for learning purposes.
        // Not required to make actual REST call. 
        public static IRestResponse m_lastResponse = null;

        ///=============================================================
        /// Authentication/v1/authenticate 
        /// Obtain an access token. 
        /// URL
        /// https://developer.api.autodesk.com/authentication/v1/authenticate
        /// Method: POST
        /// Doc: 
        /// https://developer.autodesk.com/en/docs/oauth/v2/reference/http/authenticate-POST/
        /// 
        /// Sample Response (JSON)
        /// {
        ///   "token_type":"Bearer",
        ///   "expires_in":3599,
        ///   "access_token":"aPJ8Ibj34KgDj8tkuWQFjo4hzs5sN"
        /// }
        ///=============================================================
        public static string Authenticate(string scope)
        {
            // (1) Build request 
            var client = new RestClient();
            client.BaseUrl = new System.Uri(baseApiUrl);

            // Set resource/end point
            var request = new RestRequest();
            request.Resource = "authentication/v1/authenticate";
            request.Method = Method.POST;

            // Set required parameters 
            request.AddParameter("client_id", clientID);   
            request.AddParameter("client_secret", clientSecret);  
            request.AddParameter("grant_type", "client_credentials");
            request.AddParameter("scope", scope);

            Debug.WriteLine("Calling POST authentication/v1/authenticate ...");

            // (2) Execute request and get response
            IRestResponse response = client.Execute(request);

            // Save response. This is to see the response for our learning.
            m_lastResponse = response;

            Debug.WriteLine("StatusCode = " + response.StatusCode);

            // (3) Parse the response and get the access token. 
            string accessToken = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                JsonDeserializer deserial = new JsonDeserializer();
                AuthenticateResponse loginResponse = 
                    deserial.Deserialize<AuthenticateResponse>(response);
                accessToken = loginResponse.access_token;
            }

            return accessToken; 
        }

    }
}
