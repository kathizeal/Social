using Social.Data.Handler;
using Social.Domain;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Data
{
    public class CheckExistDataManager:DataManagerBaseClass
    {
        public void CheckExist(CheckExistRequest request, ICallback<CheckExistResponse> callback)
        {
            bool exist = DBHandlers.CheckExist(request.CurrentUser, request.AnotherUser);
            callback.OnSuccess(new Response<CheckExistResponse> { Obj = new CheckExistResponse(exist)});
        }
    }
}
