using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Flurl.Http;

namespace Nop.Web
{
    public class AdWebLoginApi
    {
        public String getAccessToken(string client_id, string redirect_uri, string client_secret, string code)
        {
            string url = "http://idmgt.fushan.fihnbb.com:8069/adweb/oauth2/access_token/v1";
            string postData = "client_id=" + client_id + "&redirect_uri=" + redirect_uri + "&client_secret=" + client_secret + "&code=" + code + "&grant_type=authorization_code";
            var request = (HttpWebRequest)WebRequest.Create(url);

            var data = Encoding.ASCII.GetBytes(postData);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public string getUserInfo(string access_token)
        {
            var responseString = "http://idmgt.fushan.fihnbb.com:8069/adweb/people/me/v1"
                .WithOAuthBearerToken(access_token)
                .GetStringAsync().Result;
            return responseString;
        }


        public string getUserDetail(string access_token)
        {
            var responseString = "http://idmgt.fushan.fihnbb.com:8069/adweb/record/search/v1?model=hr.employee&search_datas=[(\"ad_user_sAMAccountName\",\"ilike\",\"tngthan1\")]&fields=[\"name\",\"id\"]"
                .WithOAuthBearerToken(access_token)
                .GetStringAsync().Result;
            return responseString;
        }

        public string getGroupByUser(string access_token, string fushan_account)
        {
            var responseString = ("http://idmgt.fushan.fihnbb.com/adweb/record/search/v1?model=fih.nbb.group&search_datas=[(\"employee_ids.ad_user_sAMAccountName\",\"in\",['" + fushan_account + "'])]&fields=[\"name\",\"id\"]")
                 .WithOAuthBearerToken(access_token)
                 .GetStringAsync().Result;
            return responseString;
        }
    }
}