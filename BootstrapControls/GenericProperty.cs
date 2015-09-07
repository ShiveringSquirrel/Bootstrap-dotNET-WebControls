using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BootstrapControls
{
    [Serializable]
    public class GenericProperty<T>
    {
        private readonly StateBag viewState;
        private T value;
        private readonly string viewStateId;

        public GenericProperty()
        {
            viewStateId = Guid.NewGuid().ToString();
            Page page = (Page)HttpContext.Current.CurrentHandler;
            this.viewState = null;
            PropertyInfo property = page.GetType().GetProperty("ViewState", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                this.viewState = (StateBag)property.GetValue(page, null);
            }
        }

        public T Value
        {
            get
            {
                string vsName = viewStateId;

                if (this.viewState != null && this.viewState[vsName] != null)
                {
                    return (T)this.viewState[vsName];
                }
                return default(T);
            }
            set
            {
                string vsName = viewStateId;

                if (this.viewState != null)
                {
                    this.viewState[vsName] = value;
                }
                this.value = value;
            }
        }

        public static implicit operator T(GenericProperty<T> value)
        {
            return value.Value;
        }

        public static implicit operator GenericProperty<T>(T value)
        {
            return new GenericProperty<T>() { Value = value };
        }

    }
}
