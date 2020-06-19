using System.Web;

namespace aspnet_mvc_tab_specific_session.Controllers
{
    /// <summary>
    /// Wrapper around the session object to support tab specific session.
    /// By overriding, this class can be extended for session methods like add, remove & contains etc as needed by derived controller class.
    /// </summary>
    public class UniqueSession
    {
        protected internal string _uniqueKey;

        public UniqueSession(string uniqueKey) : this(new HttpSessionStateWrapper(HttpContext.Current.Session))
        {
            _uniqueKey = uniqueKey;
        }

        public UniqueSession(HttpSessionStateBase sessionWrapper)
        {
            Session = sessionWrapper;
        }

        public HttpSessionStateBase Session
        {
            get;
            private set;
        }

        public object this[string name]
        {
            get
            {
                return this.Session[this._uniqueKey + name];
            }
            set
            {
                this.Session[this._uniqueKey + name] = value;
            }
        }

        public void Add(string name, object value)
        {
            this.Session.Add(this._uniqueKey + name, value);
        }

        public void Remove(string name)
        {
            this.Session.Remove(this._uniqueKey + name);
        }
    }
}