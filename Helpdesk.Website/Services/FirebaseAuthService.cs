﻿using System;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace Helpdesk.Website.Services
{
    public static class FirebaseAuthService
    {
        private static FirebaseAuth UserAuth { get; set; }
        private static FirebaseAuth AdminAuth { get; set; }
        
        public static void Configure()
        {
            // Setup User Authentication
            var userApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("FirebaseServiceAccounts/helpdesk-users-firebase-adminsdk-7j3w5-df4c9ec95c.json")
            });
            UserAuth = FirebaseAuth.GetAuth(userApp);
            
            // Setup Admin Authentication
            var adminApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("FirebaseServiceAccounts/helpdesk-admins-firebase-adminsdk-scgf7-a966a25648.json")
            }, "admin");
            AdminAuth = FirebaseAuth.GetAuth(adminApp);
        }

        private static FirebaseAuth SetupAuth(string tokenFile)
        {
            var firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(tokenFile),
            });

            return FirebaseAuth.GetAuth(firebaseApp);
        }
        
        public static async Task<bool> CheckUserToken(string jwt)
        {
            try { var decodedToken = await UserAuth.VerifyIdTokenAsync(jwt); }
            catch (FirebaseAdmin.Auth.FirebaseAuthException) { return false; }
            
            return true;
        }
        
        public static async Task<bool> CheckAdminToken(string jwt)
        {
            try { var decodedToken = await AdminAuth.VerifyIdTokenAsync(jwt); }
            catch (FirebaseAdmin.Auth.FirebaseAuthException) { return false; }
            
            return true;
        }
    }
}