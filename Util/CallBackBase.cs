using Social.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Util
{
    public interface ICallback<T>
    {
        void OnSuccess(Response<T> response);
        void OnFailed();
    }

    public abstract class CallbackBase<T> : ICallback<T>
    {

        public abstract void OnSuccess(Response<T> response);
        public abstract void OnFailed();
    }

}
