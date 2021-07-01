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
    public class StateDataManager:DBHandlers
    {
        public void State(ICallback<StateResponse> callback)
        {
            callback.OnSuccess(new Response<StateResponse> { Obj = new StateResponse(State()) });
        }
    }
}
