using AgFx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;

namespace XStream.Phone.Model
{
    public class User : ModelItemBase<UserLoadContext>
    {
        public User()
        {
        }

        public User(IDictionary<string, string> info) : base(new UserLoadContext(info))
        {
        }

        [JsonProperty("token")]
        public int Token { get; set; }

        public class UserDataLoader : IDataLoader<UserLoadContext>
        {
            private const string LoginUriFormat = "http://xstream.cloudapp.net:9001/user={0}/pass={1}";
            private const string LogoutUriFormat = "http://xstream.cloudapp.net:9001/token={0}/logout";
            public LoadRequest GetLoadRequest(UserLoadContext loadContext, Type objectType)
            {
                string uri;
                if (loadContext.IsLoggingIn)
                {
                    uri = String.Format(LoginUriFormat, loadContext.Name, loadContext.Password);
                }
                else
                {
                    int? token = IsolatedStorageSettings.ApplicationSettings["token"] as int?;
                    uri = String.Format(LogoutUriFormat, token);
                }
                return new WebLoadRequest(loadContext, new Uri(uri));
            }

            public object Deserialize(UserLoadContext loadContext, Type objectType, Stream stream)
            {
                string json;
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
                return loadContext.IsLoggingIn ? JsonConvert.DeserializeObject<User>(json) : new User();
            }
        }

    }

    public class UserLoadContext : LoadContext
    {
        public string Name
        {
            get
            {
                return ((IDictionary<string, string>)Identity)["name"];
            }
        }

        public string Password
        {
            get
            {
                return ((IDictionary<string, string>)Identity)["password"];
            }
        }

        public bool IsLoggingIn
        {
            get
            {
                return !((IDictionary<string, string>)Identity).ContainsKey("logout");
            }
        }

        public UserLoadContext(IDictionary<string, string> info) : base(info)
        {
        }
    }
}
