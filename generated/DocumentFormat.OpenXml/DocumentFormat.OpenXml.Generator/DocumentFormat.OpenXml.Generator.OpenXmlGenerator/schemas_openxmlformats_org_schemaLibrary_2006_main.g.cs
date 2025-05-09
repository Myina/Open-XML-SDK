﻿// <auto-generated/>

// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#nullable enable

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Framework;
using DocumentFormat.OpenXml.Framework.Metadata;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Validation.Schema;
using DocumentFormat.OpenXml.Validation.Semantic;
using System;
using System.Collections.Generic;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.CustomXmlSchemaReferences
{
    /// <summary>
    /// <para>Embedded Custom XML Schema Supplementary Data.</para>
    /// <para>This class is available in Office 2007 and above.</para>
    /// <para>When the object is serialized out as xml, it's qualified name is sl:schemaLibrary.</para>
    /// </summary>
    /// <remarks>
    /// <para>The following table lists the possible child types:</para>
    /// <list type="bullet">
    ///   <item><description><see cref="DocumentFormat.OpenXml.CustomXmlSchemaReferences.Schema" /> <c>&lt;sl:schema></c></description></item>
    /// </list>
    /// </remarks>
    public partial class SchemaLibrary : OpenXmlCompositeElement
    {
        #pragma warning disable CS0109
        internal static readonly new OpenXmlQualifiedName ElementQName = new("http://schemas.openxmlformats.org/schemaLibrary/2006/main", "schemaLibrary");
        internal static readonly new OpenXmlQualifiedName ElementTypeName = new("http://schemas.openxmlformats.org/schemaLibrary/2006/main", "CT_SchemaLibrary");
        internal static readonly new OpenXmlSchemaType ElementType = new(ElementQName, ElementTypeName);
        #pragma warning restore CS0109

        /// <summary>
        /// Initializes a new instance of the SchemaLibrary class.
        /// </summary>
        public SchemaLibrary() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchemaLibrary class with the specified child elements.
        /// </summary>
        /// <param name="childElements">Specifies the child elements.</param>
        public SchemaLibrary(IEnumerable<OpenXmlElement> childElements) : base(childElements)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchemaLibrary class with the specified child elements.
        /// </summary>
        /// <param name="childElements">Specifies the child elements.</param>
        public SchemaLibrary(params OpenXmlElement[] childElements) : base(childElements)
        {
        }

        /// <summary>
        /// Initializes a new instance of the SchemaLibrary class from outer XML.
        /// </summary>
        /// <param name="outerXml">Specifies the outer XML of the element.</param>
        public SchemaLibrary(string outerXml) : base(outerXml)
        {
        }

        internal override void ConfigureMetadata(ElementMetadata.Builder builder)
        {
            base.ConfigureMetadata(builder);
            builder.SetSchema(ElementType);
            builder.AddChild<DocumentFormat.OpenXml.CustomXmlSchemaReferences.Schema>();
            builder.Particle = new CompositeParticle.Builder(ParticleType.Sequence, 1, 1)
            {
                new ElementParticle(DocumentFormat.OpenXml.CustomXmlSchemaReferences.Schema.ElementType, 0, 0)
            };
        }

        /// <inheritdoc/>
        public override OpenXmlElement CloneNode(bool deep) => CloneImp<SchemaLibrary>(deep);
    }

    /// <summary>
    /// <para>Custom XML Schema Reference.</para>
    /// <para>This class is available in Office 2007 and above.</para>
    /// <para>When the object is serialized out as xml, it's qualified name is sl:schema.</para>
    /// </summary>
    public partial class Schema : OpenXmlLeafElement
    {
        #pragma warning disable CS0109
        internal static readonly new OpenXmlQualifiedName ElementQName = new("http://schemas.openxmlformats.org/schemaLibrary/2006/main", "schema");
        internal static readonly new OpenXmlQualifiedName ElementTypeName = new("http://schemas.openxmlformats.org/schemaLibrary/2006/main", "CT_Schema");
        internal static readonly new OpenXmlSchemaType ElementType = new(ElementQName, ElementTypeName);
        #pragma warning restore CS0109

        /// <summary>
        /// Initializes a new instance of the Schema class.
        /// </summary>
        public Schema() : base()
        {
        }

        /// <summary>
        /// <para>Custom XML Schema Namespace</para>
        /// <para>Represents the following attribute in the schema: sl:uri</para>
        /// </summary>
        /// <remarks>
        /// xmlns:sl=http://schemas.openxmlformats.org/schemaLibrary/2006/main
        /// </remarks>
        public StringValue? Uri
        {
            get => GetAttribute<StringValue>();
            set => SetAttribute(value);
        }

        /// <summary>
        /// <para>Resource File Location</para>
        /// <para>Represents the following attribute in the schema: sl:manifestLocation</para>
        /// </summary>
        /// <remarks>
        /// xmlns:sl=http://schemas.openxmlformats.org/schemaLibrary/2006/main
        /// </remarks>
        public StringValue? ManifestLocation
        {
            get => GetAttribute<StringValue>();
            set => SetAttribute(value);
        }

        /// <summary>
        /// <para>Custom XML Schema Location</para>
        /// <para>Represents the following attribute in the schema: sl:schemaLocation</para>
        /// </summary>
        /// <remarks>
        /// xmlns:sl=http://schemas.openxmlformats.org/schemaLibrary/2006/main
        /// </remarks>
        public StringValue? SchemaLocation
        {
            get => GetAttribute<StringValue>();
            set => SetAttribute(value);
        }

        internal override void ConfigureMetadata(ElementMetadata.Builder builder)
        {
            base.ConfigureMetadata(builder);
            builder.SetSchema(ElementType);
            builder.AddElement<Schema>()
                .AddAttribute("sl:uri", a => a.Uri)
                .AddAttribute("sl:manifestLocation", a => a.ManifestLocation)
                .AddAttribute("sl:schemaLocation", a => a.SchemaLocation);
            builder.AddConstraint(new AttributeValueLengthConstraint(builder.CreateQName("sl:manifestLocation"), 0, 2083) { Application = ApplicationType.Word });
            builder.AddConstraint(new AttributeValueLengthConstraint(builder.CreateQName("sl:schemaLocation"), 0, 2083) { Application = ApplicationType.Word });
            builder.AddConstraint(new AttributeValueLengthConstraint(builder.CreateQName("sl:uri"), 0, 255) { Application = ApplicationType.Word });
        }

        /// <inheritdoc/>
        public override OpenXmlElement CloneNode(bool deep) => CloneImp<Schema>(deep);
    }
}