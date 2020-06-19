using System.Web.Mvc;

namespace aspnet_mvc_tab_specific_session.Controllers
{
    /// <summary>
    /// Wrapper around the temp data object to support tab specific session.
    /// By overriding, this class can be extended for temp data methods like keep, peek & remove etc as needed by derived controller class.
    /// </summary>
    public class UniqueTempData
    {
        protected internal string _uniqueKey;

        public UniqueTempData(string uniqueKey, TempDataDictionary tempData) : this(tempData)
        {
            _uniqueKey = uniqueKey;
        }

        public UniqueTempData(TempDataDictionary tempDataWrapper)
        {
            TempData = tempDataWrapper;
        }

        public TempDataDictionary TempData
        {
            get;
            private set;
        }

        public object this[string name]
        {
            get
            {
                return this.TempData[this._uniqueKey + name];
            }
            set
            {
                this.TempData[this._uniqueKey + name] = value;
            }
        }
    }
}