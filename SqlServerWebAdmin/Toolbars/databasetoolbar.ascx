<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="databasetoolbar.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.databasetoolbar" %>

<ul class="nav nav-pills nav-stacked">
    <li class="nav-header">Database</li>
    <li>
        <a href="/Modules/Table/Tables.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Tables</a>
    </li>
    <li>
        <a href="/Modules/StoredProcedure/StoredProcedures.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Stored Procedures</a>
    </li>
    <li>
        <a href="<%= ResolveUrl("~/Modules/View/Views.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Views</a>
    </li>
    <li>
        <a href="/Modules/Database/QueryDatabase.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Query</a>
    </li>
    <li>
        <a href="/DatabaseProperties.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Properties</a>
    </li>
    <li>
        <a href="/DatabaseUsers.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Users</a>
    </li>
    <li>
        <a href="/DatabaseRoles.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Roles</a>
    </li> 
    <li>Tasks</li>
    <li>
        <a href="/Modules/Task/BackupDatabase.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Backup Database</a>
    </li>
    <li>
        <a href="/Modules/Task/RestoreDatabase.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Restore Database</a>
    </li>
    <li>
        <a href="<%= ResolveUrl("~/Modules/Task/BackupDatabase.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Backup Database</a>
    </li>
    <li>
         <a href="<%= ResolveUrl("~/Modules/Task/RestoreDatabase.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Restore Database</a>
    </li>
    <li>
         <a href="<%= ResolveUrl("~/Modules/Task/Import.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Import Database</a>
    </li>
    <li>
         <a href="<%= ResolveUrl("~/Modules/Task/Export.aspx?database=" + Server.UrlEncode(Request["database"])) %>">Export Database</a>
    </li>
</ul>
