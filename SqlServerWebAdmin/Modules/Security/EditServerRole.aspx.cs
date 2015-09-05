using Microsoft.SqlServer.Management.Smo;
using SqlServerWebAdmin.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class EditServerRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request["Role"] == null)
                    Response.Redirect("ServerRoles.aspx");

                Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;

                try
                {
                    server.Connect();
                }
                catch (System.Exception ex)
                {
                    Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
                }

                ServerRole role = server.Roles[Request["Role"]];
                if (role == null)
                    Response.Redirect("ServerRoles.aspx");

                ArrayList serverLoginNames = new ArrayList();
                foreach (Microsoft.SqlServer.Management.Smo.Login login in server.Logins)
                {
                    serverLoginNames.Add(login.Name);
                }

                RoleLogins.Items = serverLoginNames;
                RoleLogins.SelectedItems = role.EnumMemberNames();

                server.Disconnect();
            }

        }

        protected void RoleLogins_Changed(object sender, ItemPickerEventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server server = DbExtensions.CurrentServer;
            try
            {
                server.Connect();
            }
            catch (System.Exception ex)
            {
                //Response.Redirect("Error.aspx?errorPassCode=" + 2002);
                Response.Redirect(String.Format("error.aspx?errormsg={0}&stacktrace={1}", Server.UrlEncode(ex.Message), Server.UrlEncode(ex.StackTrace)));
            }
            try
            {
                ServerRole role = server.Roles[Request["Role"]];

                switch (e.Action)
                {
                    case ItemAction.Add:
                        role.AddMember(e.Item.Value);
                        break;
                    case ItemAction.Remove:
                        role.DropMember(e.Item.Value);
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage.Text = ex.Message;
                return;
            }
            finally
            {
                server.Disconnect();
            }
        }
    }
}

namespace SqlServerWebAdmin
{
    /// <summary>
    /// </summary>
    public enum ItemAction
    {
        /// <summary>
        /// </summary>
        Add,
        /// <summary>
        /// </summary>
        Remove
    }

    /// <summary>
    /// </summary>
    public delegate void ItemPickerEventHandler(object sender, ItemPickerEventArgs e);

    /// <summary>
    /// </summary>
    public class ItemPickerEventArgs : EventArgs
    {
        /// <summary>
        /// </summary>
        public ItemPickerEventArgs(ListItem item, ItemAction action)
            : base()
        {
            this.Item = item;
            this.Action = action;
        }
        /// <summary>
        /// </summary>
        public ListItem Item;
        /// <summary>
        /// </summary>
        public ItemAction Action;
    }

    /// <summary>
    /// Abstract control for creating a 1 to 1 "picker" for any collection object
    /// that implements ICollection.
    /// </summary>
    public abstract class ItemPicker : UserControl
    {
        #region Public properties
        /// <summary>
        /// </summary>
        public string DataTextField
        {
            set
            {
                ItemsBox.DataTextField = value;
                SelectedItemsBox.DataTextField = value;
            }
        }
        /// <summary>
        /// </summary>
        public string DataValueField
        {
            set
            {
                ItemsBox.DataValueField = value;
                SelectedItemsBox.DataValueField = value;
            }
        }

        /// <summary>
        /// </summary>
        public ICollection Items
        {
            get
            {
                return ViewState["Items"] as ICollection;
            }
            set
            {
                if (value != null && value.Count > 0)
                {
                    ItemsBox.Items.Clear();
                    ItemsBox.Enabled = true;
                    ViewState["Items"] = value;
                    BindItems();
                }
                else
                {
                    resetItemsBox();
                }
            }
        }

        /// <summary>
        /// </summary>
        public ICollection SelectedItems
        {
            get
            {
                return ViewState["SelectedItems"] as ICollection;
            }
            set
            {
                if (value != null && value.Count > 0)
                {
                    SelectedItemsBox.Items.Clear();
                    SelectedItemsBox.Enabled = true;
                    ViewState["SelectedItems"] = value;
                    BindSelectedItems();
                }
                else
                {
                    resetSelectedItemsBox();
                }
            }
        }
        #endregion

        /// <summary>
        /// </summary>
        public event ItemPickerEventHandler ItemChanged;

        /// <summary>
        /// </summary>
        public void OnItemChanged(ItemPickerEventArgs e)
        {
            ItemChanged(this, e);
        }

        /// <summary>
        /// </summary>
        protected ListBox ItemsBox;
        /// <summary>
        /// </summary>
        protected ListBox SelectedItemsBox;

        /// <summary>
        /// </summary>
        protected override void OnPreRender(EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Items == null || Items.Count == 0)
                    resetItemsBox();
                else
                    BindItems();

                if (SelectedItems == null || SelectedItems.Count == 0)
                    resetSelectedItemsBox();
                else
                    BindSelectedItems();
            }
        }

        /// <summary>
        /// </summary>
        protected void BindItems()
        {
            ItemsBox.DataSource = Items;
            ItemsBox.DataBind();
        }

        /// <summary>
        /// </summary>
        protected void BindSelectedItems()
        {
            SelectedItemsBox.DataSource = SelectedItems;
            SelectedItemsBox.DataBind();
            foreach (ListItem item in SelectedItemsBox.Items)
            {
                ListItem itemToRemove = ItemsBox.Items.FindByValue(item.Value);
                if (itemToRemove != null)
                    ItemsBox.Items.Remove(itemToRemove);
            }
            if (ItemsBox.Items.Count == 0)
                resetItemsBox();
        }

        /// <summary>
        /// </summary>
        protected void AddItem_Click(object sender, EventArgs e)
        {
            if (ItemsBox.SelectedItem != null)
            {
                if (!SelectedItemsBox.Enabled)
                {
                    SelectedItemsBox.Items.Clear();
                    SelectedItemsBox.Enabled = true;
                }
                OnItemChanged(new ItemPickerEventArgs(ItemsBox.SelectedItem, ItemAction.Add));
                SelectedItemsBox.SelectedIndex = -1;
                SelectedItemsBox.Items.Add(ItemsBox.SelectedItem);
                ItemsBox.Items.Remove(ItemsBox.SelectedItem);
                if (ItemsBox.Items.Count == 0)
                    resetItemsBox();
            }
        }

        /// <summary>
        /// </summary>
        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            if (SelectedItemsBox.SelectedItem != null)
            {
                if (!ItemsBox.Enabled)
                {
                    ItemsBox.Items.Clear();
                    ItemsBox.Enabled = true;
                }
                OnItemChanged(new ItemPickerEventArgs(SelectedItemsBox.SelectedItem, ItemAction.Remove));
                ItemsBox.SelectedIndex = -1;
                ItemsBox.Items.Add(SelectedItemsBox.SelectedItem);
                SelectedItemsBox.Items.Remove(SelectedItemsBox.SelectedItem);
                if (SelectedItemsBox.Items.Count == 0)
                    resetSelectedItemsBox();
            }
        }

        private void resetItemsBox()
        {
            ItemsBox.Items.Clear();
            ItemsBox.Items.Add(new ListItem(defaultText, String.Empty));
            ItemsBox.Enabled = false;
        }

        private void resetSelectedItemsBox()
        {
            SelectedItemsBox.Items.Clear();
            SelectedItemsBox.Items.Add(new ListItem(defaultText, String.Empty));
            SelectedItemsBox.Enabled = false;
        }

        private readonly string defaultText = "(No Items)";
    }
}