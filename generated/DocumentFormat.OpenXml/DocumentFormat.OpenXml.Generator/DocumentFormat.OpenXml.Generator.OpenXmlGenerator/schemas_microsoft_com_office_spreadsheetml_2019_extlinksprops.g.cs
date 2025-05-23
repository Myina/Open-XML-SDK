﻿// <auto-generated/>

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable enable

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Framework;
using DocumentFormat.OpenXml.Framework.Metadata;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation.Schema;
using System;
using System.Collections.Generic;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.Office2021.Excel.ExternalLinks
{
    /// <summary>
    /// <para>Defines the ExternalLinksPr Class.</para>
    /// <para>This class is available in Office 2021 and above.</para>
    /// <para>When the object is serialized out as xml, it's qualified name is xxlnp:externalLinksPr.</para>
    /// </summary>
    public partial class ExternalLinksPr : OpenXmlLeafElement
    {
        #pragma warning disable CS0109
        internal static readonly new OpenXmlQualifiedName ElementQName = new("http://schemas.microsoft.com/office/spreadsheetml/2019/extlinksprops", "externalLinksPr");
        internal static readonly new OpenXmlQualifiedName ElementTypeName = new("http://schemas.microsoft.com/office/spreadsheetml/2019/extlinksprops", "CT_ExternalLinksPr");
        internal static readonly new OpenXmlSchemaType ElementType = new(ElementQName, ElementTypeName);
        #pragma warning restore CS0109

        /// <summary>
        /// Initializes a new instance of the ExternalLinksPr class.
        /// </summary>
        public ExternalLinksPr() : base()
        {
        }

        /// <summary>
        /// <para>autoRefresh, this property is only available in Office 2021 and later.</para>
        /// <para>Represents the following attribute in the schema: autoRefresh</para>
        /// </summary>
        public BooleanValue? AutoRefresh
        {
            get => GetAttribute<BooleanValue>();
            set => SetAttribute(value);
        }

        internal override void ConfigureMetadata(ElementMetadata.Builder builder)
        {
            base.ConfigureMetadata(builder);
            builder.SetSchema(ElementType);
            builder.Availability = FileFormatVersions.Office2021;
            builder.AddElement<ExternalLinksPr>()
                .AddAttribute("autoRefresh", a => a.AutoRefresh);
        }

        /// <inheritdoc/>
        public override OpenXmlElement CloneNode(bool deep) => CloneImp<ExternalLinksPr>(deep);
    }
}