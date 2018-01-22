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

using System;
using System.Windows.Forms;
// Added for RestSharp. 
using RestSharp;

namespace HelloForgeWorld
{
    /// <summary>
    /// Minimum UI to call Forge.Authenticate().  
    /// Display an access token, request and response.
    /// 
    /// Disclaimer: in real world application, you don't expose 
    /// an access token. This is purely for learning purpose.   
    /// </summary>
    public partial class ForgeUI : Form
    {
        // Save access token. 
        private static string m_accessToken = "";

        public ForgeUI()
        {
            InitializeComponent();
        }

        //=========================================================
        //  Authentication
        //=========================================================
        private void buttonToken_Click(object sender, EventArgs e)
        {
            InitRequestResponse();
            this.Update();

            string scope = "bucket:create bucket:read data:write"; 

            // Here is the main part that we call Forge authenticate 
            m_accessToken = Forge.Authenticate(scope);

            // Show it in the form 
            textBoxToken.Text = m_accessToken;
            
            // For our learning, 
            // show the request and response in the form. 
            ShowRequestResponse(); 
        }

        //==========================================================
        // Helper functions 
        //==========================================================
        // Displays request and response in the text boxes.
        // This is for learning purpose. 
        private void InitRequestResponse()
        {
            // initialize the request and response text in the form.
            textBoxRequest.Text = "Request comes here";
            labelStatus.Text = "";
            textBoxResponse.Text = "Response comes here. This may take seconds. Please wait...";
        }
        private void ShowRequestResponse()
        {
            // show the request and response in the form. 
            IRestResponse response = Forge.m_lastResponse;
            textBoxRequest.Text = response.ResponseUri.AbsoluteUri;
            labelStatus.Text = "Status: " + response.StatusCode.ToString();
            textBoxResponse.Text = response.Content;
        }

    }
}
