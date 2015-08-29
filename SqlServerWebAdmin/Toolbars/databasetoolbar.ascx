<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="databasetoolbar.ascx.cs" Inherits="SqlServerWebAdmin.Toolbars.databasetoolbar" %>

<ul class="nav nav-pills nav-stacked">
    <li class="nav-header">Database</li>
    <li>
        <a href="/Tables.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Tables</a>
    </li>
    <li>
        <a href="/StoredProcedures.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Stored Procedures</a>
    </li>
    <li>
        <a href="/Views.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Views</a>
    </li>
    <li>
        <a href="/QueryDatabase.aspx?database=<%=Server.UrlEncode(Request["database"]) %>">Query</a>
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
</ul>
