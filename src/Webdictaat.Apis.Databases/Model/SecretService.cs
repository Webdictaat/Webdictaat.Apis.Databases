using MySql.Data.MySqlClient.Framework.NetCore10;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Webdictaat.Apis.Databases.Model
{
    public interface ISecretService
    {
        string GetAssignmentToken(string userId, int assignmentId);
    }

    public class SecretService : ISecretService
    {
        private string _secret;
        private SHA1 _sha1;

        public SecretService()
        {
            _sha1 = System.Security.Cryptography.SHA1.Create();
        }

        public SecretService(string secret)
        {
            _sha1 = System.Security.Cryptography.SHA1.Create();
            this._secret = secret;
        }

        public string GetAssignmentToken(string userId, int assignmentId)
        {
            string toBeHashed = userId + assignmentId + this._secret;
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(toBeHashed);
            byte[] hash = _sha1.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
