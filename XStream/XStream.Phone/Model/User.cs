﻿using AgFx;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace XStream.Phone.Model
{
    public class User : ModelItemBase<UserLoadContext>
    {
        public User()
        {
        }

        public User(IDictionary<string, string> login) : base(new UserLoadContext(login))
        {
        }

        [JsonProperty("token")]
        public int Token { get; set; }

        public class UserDataLoader : IDataLoader<UserLoadContext>
        {
            private const string UriFormat = "http://xstream.cloudapp.net:9001/user={0}/pass={1}";
            public LoadRequest GetLoadRequest(UserLoadContext loadContext, Type objectType)
            {
                string uri = String.Format(UriFormat, loadContext.Name, loadContext.Password);
                return new WebLoadRequest(loadContext, new Uri(uri));
            }

            public object Deserialize(UserLoadContext loadContext, Type objectType, Stream stream)
            {
                string json;
                using (StreamReader reader = new StreamReader(stream))
                {
                    json = reader.ReadToEnd();
                }
                return JsonConvert.DeserializeObject<User>(json);
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

        public UserLoadContext(IDictionary<string, string> login) : base(login)
        {
        }
    }
}
