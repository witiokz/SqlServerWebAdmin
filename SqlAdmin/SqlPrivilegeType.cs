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

namespace SqlAdmin
{
    /// <summary>
    /// </summary>
    [Flags]
    public enum SqlPrivilegeType
    {
        /// <summary>
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// </summary>
        Select = 1,
        /// <summary>
        /// </summary>
        Insert = 2,
        /// <summary>
        /// </summary>
        Update = 4,
        /// <summary>
        /// </summary>
        Delete = 8,
        /// <summary>
        /// </summary>
        Execute = 16,
        /// <summary>
        /// </summary>
        References = 32,
        /// <summary>
        /// </summary>
        AllObjectPrivs = 63,
        /// <summary>
        /// </summary>
        CreateTable = 128,
        /// <summary>
        /// </summary>
        CreateDatabase = 256,
        /// <summary>
        /// </summary>
        CreateView = 512,
        /// <summary>
        /// </summary>
        CreateProcedure = 1024,
        /// <summary>
        /// </summary>
        DumpDatabase = 2048,
        /// <summary>
        /// </summary>
        CreateDefault = 4096,
        /// <summary>
        /// </summary>
        DumpTransaction = 8192,
        /// <summary>
        /// </summary>
        CreateRule = 16384,
        /// <summary>
        /// </summary>
        DumpTable = 32768,
        /// <summary>
        /// </summary>
        CreateFunction = 0x00010000,
        /// <summary>
        /// </summary>
        AllDatabasePrivs = 0x0001ff80,
    }
}
