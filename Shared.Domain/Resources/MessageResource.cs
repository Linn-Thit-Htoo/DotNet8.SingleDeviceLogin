using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Resources
{
    public class MessageResource
    {
        public static string Success { get; } = "Success.";
        public static string SaveSuccess { get; } = "Saving Successful.";
        public static string SaveFail { get; } = "Saving Fail.";
        public static string UpdateSuccess { get; } = "Updating Successful.";
        public static string UpdateFail { get; } = "Updating Fail.";
        public static string DeleteSuccess { get; } = "Deleting Successful.";
        public static string DeleteFail { get; } = "Deleting Fail.";
        public static string NotFound { get; } = "No Data Found.";
        public static string Duplicate { get; } = "Duplicate Data.";
        public static string EmailDuplicate { get; } = "Duplicate Email.";
        public static string InvalidId { get; } = "Id is invalid.";
        public static string LoginFail { get; } = "Login Fail.";
        public static string InvalidEncryptionKey { get; } = "You are not allowed.";
        public static string Unauthorized { get; } = "Unauthorized.";
        public static string InvalidPageNo { get; } = "Page No is invalid.";
        public static string InvalidPageSize { get; } = "Page Size is invalid.";
        public static string CreateDirectoryFail { get; } = "Creating Directory Fail.";
    }
}
