using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.UseCase
{
    public abstract class UseCaseBase<T>
    {
        protected ICallback<T> PresenterCallback { get; private set; }
        protected UseCaseBase(ICallback<T> callback)
        {
            PresenterCallback = callback;

        }
        protected abstract void Action();
        public void Execute()
        {
            Task.Run(delegate ()
            {
                Action();

            });

        }

    }
}
