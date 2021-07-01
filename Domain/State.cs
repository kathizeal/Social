using Social.Data;
using Social.UseCase;
using Social.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social.Domain
{
    public interface IStatePresenterCallback : ICallback<StateResponse>
    {

    }
    public sealed class StateResponse
    {
        public bool State;
        public StateResponse(bool state)
        {
            State = state;
        }
    }
    public  class StateRequest : Request { }
    



    public class State : UseCaseBase<StateResponse>
    {
        StateDataManager stateDataManager;
        public StateRequest Request;
        public State(StateRequest request, IStatePresenterCallback callback) : base(callback)
        {
            Request = request;
            stateDataManager = new StateDataManager();
        }
        protected override void Action()
        {

            stateDataManager.State(new UseCaseCallback(this));

        }
        public class UseCaseCallback : CallbackBase<StateResponse>
        {
            private State UseCase;
            public UseCaseCallback(State useCase)
            {
                UseCase = useCase;
            }

            public override void OnFailed()
            {
                UseCase.PresenterCallback.OnFailed();
            }

            public override void OnSuccess(Response<StateResponse> response)
            {
                UseCase.PresenterCallback.OnSuccess(response);
            }
        }
    }
}
