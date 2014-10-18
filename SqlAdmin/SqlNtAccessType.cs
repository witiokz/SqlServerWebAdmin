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
    /// Summary description for SqlNtAccessType.
    /// </summary>
    public enum SqlNtAccessType
    {
        /// <summary>
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// </summary>
        Grant = 1,
        /// <summary>
        /// </summary>
        Deny = 2,
        /// <summary>
        /// </summary>
        NonNTLogin = 99
    }
}