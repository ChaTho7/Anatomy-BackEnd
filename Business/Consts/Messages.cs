using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Consts
{
    public static class Messages
    {
        public static string success = "Process Succeed !!!";
        public static string fail = "Process Failed !!!";
        public static string duplicateName = "Duplicate name has been detected";
        public static string sortLimit = "Sort limit exceed";
        public static string imageCountExceed = "Image count exceed";
        public static string authorizationDenied = "No permission";
        public static string accessTokenCreated = "Token creation success";
        public static string userAlreadyExists = "User already exists";
        public static string successfulLogin = "Login success";
        public static string passwordError = "Password invalid";
        public static string userNotFound = "User not found";
        public static string userRegistered = "User registration success";
        public static string claimsFetched = "Claims fetched successfully";
        public static string userFetchedByMail = "User fetched successfully";
        public static string userNotExists = "User not exists";
        public static string imageNotExists = "Image not exists";
        public static string claimsSetted="Claims setted successfully";
        public static string imageAddSuccess = "Image added successfully";
        public static string userUpdated = "User infos updated successfully";
    }
}
