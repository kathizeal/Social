using Social.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social
{
   public class CreateHandler
    {

        private static CreateHandler _Instance = null;
        private CreateHandler() {}
        public static CreateHandler GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new CreateHandler();

            }
            return _Instance;
        }
        public delegate void StatusUpdateHandler(object sender, CustomEvent e);
        public event StatusUpdateHandler OnUpdateStatus;
        public void UpdateStatus(Post post) 
        {
            // Make sure someone is listening to event
            if (OnUpdateStatus == null) return;
            CustomEvent args = new CustomEvent(post);
            OnUpdateStatus(this, args);
        }
       
    }
}
