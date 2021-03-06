﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SqlServerWebAdmin
{
    public partial class FileProperties : System.Web.UI.UserControl
    {
        public object Properties
        {
            get
            {
                int growth = 0;
                int maximumFileSize = 0;

                try
                {
                    growth = Convert.ToInt32(GrowthTextBox.Text);
                }
                catch
                {
                    throw new Exception("Growth must be an integer");
                }

                try
                {
                    maximumFileSize = Convert.ToInt32(MaximumFileSizeTextBox.Text);
                }
                catch
                {
                    throw new Exception("Maximum file size must be an integer");
                }

                return null;
                //return new FileProperties(
                //    /*(GrowthTypeDropDownList.SelectedIndex == 0) ? SqlFileGrowthType.MB : SqlFileGrowthType.Percent*/0,
                //    AutomaticallyGrowFileCheckBox.Checked ? growth : 0,
                //    UnrestrictedGrowthRadioButton.Checked ? -1 : maximumFileSize);
            }
            set
            {
                //Microsoft.SqlServer.Management.Smo.DatabaseFile
                /*FileProperties props = value;
                if (props.FileGrowth == 0)
                    AutomaticallyGrowFileCheckBox.Checked = false;
                else
                    AutomaticallyGrowFileCheckBox.Checked = true;

                if (props.FileGrowthType == 0) //SqlFileGrowthType.MB
                    GrowthTypeDropDownList.SelectedIndex = 0;
                else
                    GrowthTypeDropDownList.SelectedIndex = 1;

                GrowthTextBox.Text = props.FileGrowth.ToString();
                if (props.MaximumSize == -1)
                {
                    UnrestrictedGrowthRadioButton.Checked = true;
                    RestrictGrowthRadioButton.Checked = false;
                }
                else
                {
                    UnrestrictedGrowthRadioButton.Checked = false;
                    RestrictGrowthRadioButton.Checked = true;
                }

                MaximumFileSizeTextBox.Text = props.MaximumSize.ToString();*/
            }
        }
    }
}