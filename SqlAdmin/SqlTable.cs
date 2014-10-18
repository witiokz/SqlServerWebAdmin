//=====================================================================
//
// THIS CODE AND INFORMATION IS PROVIDED TO YOU FOR YOUR REFERENTIAL
// PURPOSES ONLY, AND IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE,
// AND MAY NOT BE REDISTRIBUTED IN ANY MANNER.
//
// Copyright (C) 2003  Microsoft Corporation.  All rights reserved.
//
//=====================================================================
using System;
using System.Data;
using System.Text;

namespace SqlAdmin
{
    /// <summary>
    /// Represents a SQL table.
    /// </summary>
    public class SqlTable
    {
        internal NativeMethods.ITable dmoTable = null;
        internal SqlDatabase database = null;

        private string name;
        private string owner;
        private SqlObjectType tableType;
        private DateTime createDate;

        internal SqlTable(string name, string owner, SqlObjectType tableType, DateTime createDate)
        {
            this.name = name;
            this.owner = owner;
            this.tableType = tableType;
            this.createDate = createDate;
        }


        /// <summary>
        /// Gets a collection of SqlColumn objects that represent the individual columns in this table.
        /// </summary>
        public SqlColumnCollection Columns
        {
            get
            {
                SqlColumnCollection columnsCollection = new SqlColumnCollection(this);
                columnsCollection.Refresh();
                return columnsCollection;
            }
        }

        /// <summary>
        /// The date and time when this table was created.
        /// </summary>
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
        }

        /// <summary>
        /// The SqlDatabase to which this table belongs.
        /// </summary>
        public SqlDatabase Database
        {
            get
            {
                return database;
            }
        }

        /// <summary>
        /// The name of the table.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                // Rename both the DMO table and the internal name
                dmoTable.SetName(value);
                name = value;
            }
        }

        /// <summary>
        /// The owner of the table.
        /// </summary>
        public string Owner
        {
            get
            {
                return owner;
            }
        }

        /// <summary>
        /// The number of rows used in this table.
        /// </summary>
        public int Rows
        {
            get
            {
                return dmoTable.GetRows();
            }
        }

        /// <summary>
        /// A SqlObjectType value indicating whether this is a User table or a System table.
        /// </summary>
        public SqlObjectType TableType
        {
            get
            {
                return tableType;
            }
        }


        /// <summary>
        /// Permanently removes this table from the database.
        /// </summary>
        public void Remove()
        {
            // Permanently delete this table
            dmoTable.Remove();
        }

        /// <summary>
        /// Generates a Transact-SQL command batch that can be used to re-create the data in the SQL table.
        /// </summary>
        /// <param name="scriptType">
        /// A SqlScriptType indicating what to include in the script.
        /// </param>
        /// <returns>
        /// A string containing a Transact-SQL command batch that can be used to re-create the data in the SQL table.
        /// </returns>
        /// <remarks>
        /// The valid SqlScriptType values are: Comments.
        /// </remarks>
        public string ScriptData(SqlScriptType scriptType)
        {

            // Grab data from table
            DataTable[] tables = Database.Query("select * from [" + name + "]");

            DataTable table = tables[0];

            StringBuilder sb = new StringBuilder();

            if ((scriptType & SqlScriptType.Comments) == SqlScriptType.Comments)
                sb.Append(String.Format(SR.GetString("SqlTable_ExportComment") + "\r\n", name));

            // If necessary, turn on identity insert
            bool hasIdentity = false;

            string[] columnNamesArray = new string[this.Columns.Count];

            for (int i = 0; i < this.Columns.Count; i++)
            {
                columnNamesArray[i] = "[" + this.Columns[i].ColumnInformation.Name + "]";

                if (this.Columns[i].ColumnInformation.Identity)
                {
                    hasIdentity = true;
                }
            }

            string columnNames = String.Join(", ", columnNamesArray);

            if (hasIdentity)
                sb.Append("SET identity_insert [" + this.Name + "] on\r\n\r\n");

            // Go through each row
            for (int i = 0; i < table.Rows.Count; i++)
            {
                object[] cols = table.Rows[i].ItemArray;
                sb.Append(String.Format("INSERT [{0}] ({1}) VALUES (", name, columnNames));

                // And through each column within the row
                for (int j = 0; j < cols.Length; j++)
                {
                    if (j > 0)
                        sb.Append(", ");

                    System.Type dataType = table.Columns[j].DataType;

                    // If null, print null, otherwise output data based on type
                    if (table.Rows[i].IsNull(j))
                    {
                        // Database Null is just NULL
                        sb.Append("NULL");
                    }
                    else if (dataType == typeof(System.Int32) ||
                             dataType == typeof(System.Int16) ||
                             dataType == typeof(System.Decimal) ||
                             dataType == typeof(System.Single))
                    {
                        // Numeric datatypes we just emit as-is
                        sb.Append(cols[j]);
                    }
                    else if (dataType == typeof(System.DateTime) ||
                             dataType == typeof(System.String))
                    {
                        // Strings and date/time's get quoted

                        // Escape single quotes in strings (replace with two single quotes)
                        sb.Append(String.Format("'{0}'", cols[j].ToString().Replace("'", "''")));
                    }
                    else if (dataType == typeof(System.Boolean))
                    {
                        // Booleans are false=0 and true=1
                        if ((System.Boolean)cols[j])
                            sb.Append("1");
                        else
                            sb.Append("0");
                    }
                    else if (dataType == typeof(System.Byte[]))
                    {
                        // Byte arrays are in the form 0x0123456789ABCDEF
                        System.Byte[] array = (System.Byte[])cols[j];
                        sb.Append("0x");
                        for (int a = 0; a < array.Length; a++)
                            sb.Append(array[a].ToString("X"));
                    }
                    else
                    {
                        // Default is to call ToString() and quote it

                        // Escape single quotes in strings (replace with two single quotes)
                        sb.Append(String.Format("'{0}'", cols[j].ToString().Replace("'", "''")));
                    }
                }

                sb.Append(")\r\n");

                // Stick in a GO statement to make batches smaller
                //if ((i + 1) % 10 == 0)
                //    sb.Append("GO\r\n");
            }

            //sb.Append("GO\r\n\r\n");

            // If we turned on identity insert, turn it off now
            if (hasIdentity)
                sb.Append("SET identity_insert [" + this.Name + "] off\r\nGO\r\n");

            return sb.ToString();
        }

        /// <summary>
        /// Generates a Transact-SQL command batch that can be used to re-create the SQL table.
        /// </summary>
        /// <param name="scriptType">
        /// A SqlScriptType indicating what to include in the script.
        /// </param>
        /// <returns>
        /// A string containing a Transact-SQL command batch that can be used to re-create the SQL table.
        /// </returns>
        /// <remarks>
        /// The valid SqlScriptType values are: Create, Drop, Comments, Defaults, PrimaryKey, ForeignKeys, UniqueKeys, Checks, Indexes.
        /// </remarks>
        public string ScriptSchema(SqlScriptType scriptType)
        {
            int dmoScriptType = 0;

            if ((scriptType & SqlScriptType.Create) == SqlScriptType.Create)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Default | NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_NoDRI;

            if ((scriptType & SqlScriptType.Defaults) == SqlScriptType.Defaults)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRI_Defaults | NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRIWithNoCheck;

            if ((scriptType & SqlScriptType.PrimaryKey) == SqlScriptType.PrimaryKey)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRI_PrimaryKey | NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRIWithNoCheck;

            if ((scriptType & SqlScriptType.Checks) == SqlScriptType.Checks)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRI_Checks | NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRIWithNoCheck;

            if ((scriptType & SqlScriptType.ForeignKeys) == SqlScriptType.ForeignKeys)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRI_ForeignKeys;

            if ((scriptType & SqlScriptType.UniqueKeys) == SqlScriptType.UniqueKeys)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRI_UniqueKeys;

            if ((scriptType & SqlScriptType.Indexes) == SqlScriptType.Indexes)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_DRIIndexes;

            if ((scriptType & SqlScriptType.Drop) == SqlScriptType.Drop)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_Drops;

            if ((scriptType & SqlScriptType.Comments) == SqlScriptType.Comments)
                dmoScriptType |= NativeMethods.SQLDMO_SCRIPT_TYPE.SQLDMOScript_IncludeHeaders;


            return dmoTable.Script(dmoScriptType, null, null, 0);
        }
    }
}
