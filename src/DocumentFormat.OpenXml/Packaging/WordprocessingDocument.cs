﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DocumentFormat.OpenXml.Builder;
using DocumentFormat.OpenXml.Features;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Packaging;
using System.Reflection;

namespace DocumentFormat.OpenXml.Packaging
{
    /// <summary>
    /// Defines WordprocessingDocument - an OpenXmlPackage represents a Word document.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "Disposable is returned")]
    public partial class WordprocessingDocument : OpenXmlPackage
    {
        internal WordprocessingDocument()
            : base()
        {
        }

        private static readonly IPackageBuilder<WordprocessingDocument> DefaultBuilder = new Builder().UseDefaultBehaviorAndLockBuilder();

        /// <summary>
        /// Creates a default builder for <see cref="WordprocessingDocument"/>
        /// </summary>
        /// <returns>The default builder.</returns>
        [Experimental(ExperimentalApis.PackageBuilder, UrlFormat = ExperimentalApis.UrlFormat)]
        [Obsolete(ExperimentalApis.Message, DiagnosticId = ExperimentalApis.PackageBuilder, UrlFormat = ExperimentalApis.UrlFormat)]
        internal static IPackageBuilder<WordprocessingDocument> CreateBuilder() => new Builder();

        /// <summary>
        /// Creates a builder that has minimal initialization for <see cref="WordprocessingDocument"/>.
        /// </summary>
        /// <returns>A minimal builder.</returns>
        [Experimental(ExperimentalApis.PackageBuilder, UrlFormat = ExperimentalApis.UrlFormat)]
        [Obsolete(ExperimentalApis.Message, DiagnosticId = ExperimentalApis.PackageBuilder, UrlFormat = ExperimentalApis.UrlFormat)]
        internal static IPackageBuilder<WordprocessingDocument> CreateDefaultBuilder() => DefaultBuilder.Clone();

        /// <summary>
        /// Gets the default factory for <see cref="WordprocessingDocument"/>.
        /// </summary>
        [Experimental(ExperimentalApis.PackageBuilder, UrlFormat = ExperimentalApis.UrlFormat)]
        [Obsolete(ExperimentalApis.Message, DiagnosticId = ExperimentalApis.PackageBuilder, UrlFormat = ExperimentalApis.UrlFormat)]
        internal static IPackageFactory<WordprocessingDocument> DefaultFactory { get; } = DefaultBuilder.Build();

        private sealed class Builder : OpenXmlPackageBuilder<WordprocessingDocument>
        {
            public Builder(Builder? builder = null)
                : base(builder)
            {
            }

            public override WordprocessingDocument Create() => new();

            public override IPackageBuilder<WordprocessingDocument> Clone() => new Builder(this);
        }

        /// <summary>
        /// Gets the type of the WordprocessingDocument.
        /// </summary>
        public WordprocessingDocumentType DocumentType
        {
            get
            {
                ThrowIfObjectDisposed();
                return Features.GetRequired<IDocumentTypeFeature<WordprocessingDocumentType>>().Current;
            }

            private set
            {
                ThrowIfObjectDisposed();
                Features.GetRequired<IDocumentTypeFeature<WordprocessingDocumentType>>().Current = value;
            }
        }

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified file.
        /// </summary>
        /// <param name="path">The path and file name of the target WordprocessingDocument.</param>
        /// <param name="type">The type of the WordprocessingDocument.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "path" is null reference.</exception>
        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type)
            => Create(path, type, true);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the IO stream.
        /// </summary>
        /// <param name="stream">The IO stream on which to create the WordprocessingDocument.</param>
        /// <param name="type">The type of the WordprocessingDocument.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "stream" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "stream" is not opened with Write access.</exception>
        public static WordprocessingDocument Create(Stream stream, WordprocessingDocumentType type)
            => Create(stream, type, true);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified package.
        /// </summary>
        /// <param name="package">The specified OpenXml package.</param>
        /// <param name="type">The type of the WordprocessingDocument.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "package" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "package" is not opened with Write access.</exception>
        public static WordprocessingDocument Create(Package package, WordprocessingDocumentType type)
            => Create(package, type, true);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified file.
        /// </summary>
        /// <param name="path">The path and file name of the target WordprocessingDocument.</param>
        /// <param name="type">The type of the WordprocessingDocument.</param>
        /// <param name="autoSave">Whether to auto save the created document.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "path" is null reference.</exception>
        public static WordprocessingDocument Create(string path, WordprocessingDocumentType type, bool autoSave)
            => CreateDefaultBuilder()
                .Use(package =>
                {
                    package.DocumentType = type;
                    package.OpenSettings.AutoSave = autoSave;
                })
                .Build()
                .Open(path, PackageOpenMode.Create);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the IO stream.
        /// </summary>
        /// <param name="stream">The IO stream on which to create the WordprocessingDocument.</param>
        /// <param name="type">The type of the WordprocessingDocument.</param>
        /// <param name="autoSave">Whether to auto save the created document.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "stream" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "stream" is not opened with Write access.</exception>
        public static WordprocessingDocument Create(Stream stream, WordprocessingDocumentType type, bool autoSave)
            => CreateDefaultBuilder()
                .Use(package =>
                {
                    package.DocumentType = type;
                    package.OpenSettings.AutoSave = autoSave;
                })
                .Build()
                .Open(stream, PackageOpenMode.Create);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified package.
        /// </summary>
        /// <param name="package">The specified OpenXml package</param>
        /// <param name="type">The type of the WordprocessingDocument.</param>
        /// <param name="autoSave">Whether to auto save the created document.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "package" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "package" is not opened with Write access.</exception>
        public static WordprocessingDocument Create(Package package, WordprocessingDocumentType type, bool autoSave)
            => CreateDefaultBuilder()
                .Use(package =>
                {
                    package.DocumentType = type;
                    package.OpenSettings.AutoSave = autoSave;
                })
                .Build()
                .Open(package);

        /// <summary>
        /// Creates an editable WordprocessingDocument from a template, opened on
        /// a MemoryStream with expandable capacity. The template will be attached
        /// to the WordprocessingDocument.
        /// </summary>
        /// <remarks>
        /// Attaching the template has been chosen as the default behavior because
        /// this is also what happens when a document is created from a template
        /// (other than Normal.dotm) using Microsoft Word.
        /// </remarks>
        /// <param name="path">The path and file name of the template.</param>
        /// <returns>The new WordprocessingDocument based on and linked to the template.</returns>
        public static WordprocessingDocument CreateFromTemplate(string path)
            => CreateFromTemplate(path, true);

        /// <summary>
        /// Creates an editable WordprocessingDocument from a template, opened on
        /// a MemoryStream with expandable capacity.
        /// </summary>
        /// <remarks>
        /// This method is provided to offer the choice to not attach the template.
        /// When templates are attached in Microsoft Word, for example, the absolute
        /// path will be used in the relationship. These absolute paths are most
        /// often user-specific, however, so once documents are shared with other
        /// users, the relationship gets "broken" anyhow.
        /// </remarks>
        /// <param name="path">The path and file name of the template.</param>
        /// <param name="isTemplateAttached">True, if the template should be attached to the document.</param>
        /// <returns>The new WordprocessingDocument based on and linked to the template.</returns>
        public static WordprocessingDocument CreateFromTemplate(string path, bool isTemplateAttached)
        {
            if (path is null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            var factory = DefaultBuilder
                .Build()
                .WithTemplate(path, WordprocessingDocumentType.Document);

            if (isTemplateAttached)
            {
                factory.Template += package =>
                {
                    // Create a relative or absolute external relationship to the template.
                    // TODO: Check whether relative URIs are universally supported. They work in Office 2010.
                    var documentSettingsPart = package.MainDocumentPart?.DocumentSettingsPart;

                    if (documentSettingsPart is not null)
                    {
                        var relationship = documentSettingsPart.AddExternalRelationship(
                            "http://schemas.openxmlformats.org/officeDocument/2006/relationships/attachedTemplate",
                            new Uri(path, UriHelper.RelativeOrAbsolute));
                        documentSettingsPart.Settings.AppendChild(new AttachedTemplate { Id = relationship.Id });
                    }
                };
            }

            return factory.Open(new MemoryStream(), PackageOpenMode.Create);
        }

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified file.
        /// </summary>
        /// <param name="path">The path and file name of the target WordprocessingDocument.</param>
        /// <param name="isEditable">In ReadWrite mode. False for Read only mode.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "path" is null reference.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when the package is not valid Open XML WordprocessingDocument.</exception>
        public static WordprocessingDocument Open(string path, bool isEditable)
            => Open(path, isEditable, new OpenSettings());

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the IO stream.
        /// </summary>
        /// <param name="stream">The IO stream on which to open the WordprocessingDocument.</param>
        /// <param name="isEditable">In ReadWrite mode. False for Read only mode.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "stream" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "stream" is not opened with Read (ReadWrite) access.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when the package is not valid Open XML WordprocessingDocument.</exception>
        public static WordprocessingDocument Open(Stream stream, bool isEditable)
            => Open(stream, isEditable, new OpenSettings());

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified file.
        /// </summary>
        /// <param name="path">The path and file name of the target WordprocessingDocument.</param>
        /// <param name="isEditable">In ReadWrite mode. False for Read only mode.</param>
        /// <param name="openSettings">The advanced settings for opening a document.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "path" is null reference.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when the package is not valid Open XML WordprocessingDocument.</exception>
        /// <exception cref="ArgumentException">Thrown when specified to process the markup compatibility but the given target FileFormatVersion is incorrect.</exception>
        public static WordprocessingDocument Open(string path, bool isEditable, OpenSettings openSettings)
            => CreateDefaultBuilder()
                .UseSettings(openSettings)
                .Build()
                .Open(path, isEditable);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the IO stream.
        /// </summary>
        /// <param name="stream">The IO stream on which to open the WordprocessingDocument.</param>
        /// <param name="isEditable">In ReadWrite mode. False for Read only mode.</param>
        /// <param name="openSettings">The advanced settings for opening a document.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "stream" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "stream" is not opened with Read (ReadWrite) access.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when the package is not valid Open XML WordprocessingDocument.</exception>
        /// <exception cref="ArgumentException">Thrown when specified to process the markup compatibility but the given target FileFormatVersion is incorrect.</exception>
        public static WordprocessingDocument Open(Stream stream, bool isEditable, OpenSettings openSettings)
            => CreateDefaultBuilder()
                .UseSettings(openSettings)
                .Build()
                .Open(stream, isEditable);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified package.
        /// </summary>
        /// <param name="package">The specified OpenXml package</param>
        /// <param name="openSettings">The advanced settings for opening a document.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when package is a null reference.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when package is not opened with read access.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when the package is not a valid Open XML document.</exception>
        /// <exception cref="ArgumentException">Thrown when specified to process the markup compatibility but the given target FileFormatVersion is incorrect.</exception>
        public static WordprocessingDocument Open(Package package, OpenSettings openSettings)
            => CreateDefaultBuilder()
                .UseSettings(openSettings)
                .Build()
                .Open(package);

        /// <summary>
        /// Creates a new instance of the WordprocessingDocument class from the specified package.
        /// </summary>
        /// <param name="package">The specified OpenXml package.</param>
        /// <returns>A new instance of WordprocessingDocument.</returns>
        /// <exception cref="ArgumentNullException">Thrown when "package" is null reference.</exception>
        /// <exception cref="IOException">Thrown when "package" is not opened with Read (ReadWrite) access.</exception>
        /// <exception cref="OpenXmlPackageException">Thrown when the package is not valid Open XML WordprocessingDocument.</exception>
        public static WordprocessingDocument Open(Package package)
            => Open(package, new OpenSettings());

        /// <summary>
        /// Changes the document type.
        /// </summary>
        /// <param name="newType">The new type of the document.</param>
        /// <remarks>The MainDocumentPart will be changed.</remarks>
        public void ChangeDocumentType(WordprocessingDocumentType newType)
        {
            ThrowIfObjectDisposed();
            Features.GetRequired<IDocumentTypeFeature<WordprocessingDocumentType>>().ChangeDocumentType(newType);
        }

        /// <summary>
        /// Adds a new part of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The class of the part.</typeparam>
        /// <param name="contentType">The content type of the part. Must match the defined content type if the part is fixed content type.</param>
        /// <param name="id">The relationship id. The id will be automatically generated if this param is null.</param>
        /// <returns>The added part.</returns>
        /// <exception cref="OpenXmlPackageException">When the part is not allowed to be referenced by this part.</exception>
        /// <exception cref="ArgumentOutOfRangeException">When the part is fixed content type and the passed in contentType does not match the defined content type.</exception>
        /// <exception cref="ArgumentNullException">Thrown when "contentType" is null reference.</exception>
        /// <remarks>Mainly used for adding not-fixed content type part - ImagePart, etc</remarks>
        public override T AddNewPart<T>(string contentType, string? id)
        {
            if (contentType is null)
            {
                throw new ArgumentNullException(nameof(contentType));
            }

            if (typeof(MainDocumentPart).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()) && contentType != Features.GetRequired<IMainPartFeature>().ContentType)
            {
                throw new OpenXmlPackageException(ExceptionMessages.ErrorContentType);
            }

            return base.AddNewPart<T>(contentType, id);
        }

        /// <summary>
        /// Creates the MainDocumentPart and add it to this document.
        /// </summary>
        /// <returns>The newly added MainDocumentPart.</returns>
        public MainDocumentPart AddMainDocumentPart()
        {
            MainDocumentPart childPart = new();
            InitPart(childPart, MainPartContentType);
            return childPart;
        }

        /// <summary>
        /// Adds a CoreFilePropertiesPart to the WordprocessingDocument.
        /// </summary>
        /// <returns>The newly added CoreFilePropertiesPart.</returns>
        public CoreFilePropertiesPart AddCoreFilePropertiesPart()
        {
            CoreFilePropertiesPart childPart = new();
            InitPart(childPart, CoreFilePropertiesPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a ExtendedFilePropertiesPart to the WordprocessingDocument.
        /// </summary>
        /// <returns>The newly added ExtendedFilePropertiesPart.</returns>
        public ExtendedFilePropertiesPart AddExtendedFilePropertiesPart()
        {
            ExtendedFilePropertiesPart childPart = new();
            InitPart(childPart, ExtendedFilePropertiesPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a CustomFilePropertiesPart to the WordprocessingDocument.
        /// </summary>
        /// <returns>The newly added CustomFilePropertiesPart.</returns>
        public CustomFilePropertiesPart AddCustomFilePropertiesPart()
        {
            CustomFilePropertiesPart childPart = new();
            InitPart(childPart, CustomFilePropertiesPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a DigitalSignatureOriginPart to the WordprocessingDocument.
        /// </summary>
        /// <returns>The newly added DigitalSignatureOriginPart.</returns>
        public DigitalSignatureOriginPart AddDigitalSignatureOriginPart()
        {
            DigitalSignatureOriginPart childPart = new();
            InitPart(childPart, DigitalSignatureOriginPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a QuickAccessToolbarCustomizationsPart to the WordprocessingDocument.
        /// </summary>
        /// <returns>The newly added QuickAccessToolbarCustomizationsPart.</returns>
        public QuickAccessToolbarCustomizationsPart AddQuickAccessToolbarCustomizationsPart()
        {
            QuickAccessToolbarCustomizationsPart childPart = new QuickAccessToolbarCustomizationsPart();
            InitPart(childPart, QuickAccessToolbarCustomizationsPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a RibbonExtensibilityPart to the WordprocessingDocument.
        /// </summary>
        /// <returns>The newly added RibbonExtensibilityPart.</returns>
        public RibbonExtensibilityPart AddRibbonExtensibilityPart()
        {
            RibbonExtensibilityPart childPart = new();
            InitPart(childPart, RibbonExtensibilityPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a RibbonAndBackstageCustomizationsPart to the WordprocessingDocument, this part is only available in Office2010.
        /// </summary>
        /// <returns>The newly added RibbonExtensibilityPart.</returns>
        public RibbonAndBackstageCustomizationsPart AddRibbonAndBackstageCustomizationsPart()
        {
            RibbonAndBackstageCustomizationsPart childPart = new();
            InitPart(childPart, RibbonAndBackstageCustomizationsPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a WebExTaskpanesPart to the WordprocessingDocument, this part is only available in Office2013.
        /// </summary>
        /// <returns>The newly added WebExTaskpanesPart.</returns>
        public WebExTaskpanesPart AddWebExTaskpanesPart()
        {
            WebExTaskpanesPart childPart = new();
            InitPart(childPart, WebExTaskpanesPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Adds a LabelInfoPart to the WordprocessingDocument, this part is only available in Office2021.
        /// </summary>
        /// <returns>The newly added LabelInfoPart.</returns>
        public LabelInfoPart AddLabelInfoPart()
        {
            LabelInfoPart childPart = new();
            InitPart(childPart, LabelInfoPart.ContentTypeConstant);
            return childPart;
        }

        /// <summary>
        /// Gets the MainDocumentPart of the WordprocessingDocument.
        /// </summary>
        public MainDocumentPart? MainDocumentPart => (MainDocumentPart?)RootPart;

        /// <summary>
        /// Gets the CoreFilePropertiesPart of the WordprocessingDocument.
        /// </summary>
        public CoreFilePropertiesPart? CoreFilePropertiesPart
        {
            get { return GetSubPartOfType<CoreFilePropertiesPart>(); }
        }

        /// <summary>
        /// Gets the ExtendedFilePropertiesPart of the WordprocessingDocument.
        /// </summary>
        public ExtendedFilePropertiesPart? ExtendedFilePropertiesPart
        {
            get { return GetSubPartOfType<ExtendedFilePropertiesPart>(); }
        }

        /// <summary>
        /// Gets the CustomFilePropertiesPart of the WordprocessingDocument.
        /// </summary>
        public CustomFilePropertiesPart? CustomFilePropertiesPart
        {
            get { return GetSubPartOfType<CustomFilePropertiesPart>(); }
        }

        /// <summary>
        /// Gets the ThumbnailPart of the WordprocessingDocument.
        /// </summary>
        public ThumbnailPart? ThumbnailPart
        {
            get { return GetSubPartOfType<ThumbnailPart>(); }
        }

        /// <summary>
        /// Gets the DigitalSignatureOriginPart of the WordprocessingDocument.
        /// </summary>
        public DigitalSignatureOriginPart? DigitalSignatureOriginPart
        {
            get { return GetSubPartOfType<DigitalSignatureOriginPart>(); }
        }

        /// <summary>
        /// Gets the RibbonExtensibilityPart of the WordprocessingDocument.
        /// </summary>
        public RibbonExtensibilityPart? RibbonExtensibilityPart
        {
            get { return GetSubPartOfType<RibbonExtensibilityPart>(); }
        }

        /// <summary>
        /// Gets the QuickAccessToolbarCustomizationsPart of the WordprocessingDocument.
        /// </summary>
        public QuickAccessToolbarCustomizationsPart? QuickAccessToolbarCustomizationsPart
        {
            get { return GetSubPartOfType<QuickAccessToolbarCustomizationsPart>(); }
        }

        /// <summary>
        /// Gets the RibbonAndBackstageCustomizationsPart of the WordprocessingDocument, only available in Office2010.
        /// </summary>
        public RibbonAndBackstageCustomizationsPart? RibbonAndBackstageCustomizationsPart
        {
            get { return GetSubPartOfType<RibbonAndBackstageCustomizationsPart>(); }
        }

        /// <summary>
        /// Gets the WebExTaskpanesPart of the WordprocessingDocument, only available in Office2013.
        /// </summary>
        public WebExTaskpanesPart? WebExTaskpanesPart
        {
            get { return GetSubPartOfType<WebExTaskpanesPart>(); }
        }

        /// <summary>
        /// Gets the LabelInfoPart of the WordprocessingDocument, only available in Office2021.
        /// </summary>
        public LabelInfoPart? LabelInfoPart
        {
            get { return GetSubPartOfType<LabelInfoPart>(); }
        }

        /// <inheritdoc/>
        public override IFeatureCollection Features => _features ??= new WordprocessingDocumentFeatures(this);

        [DocumentFormat.OpenXml.Generator.OpenXmlPackage("WordprocessingDocument")]
        private partial class WordprocessingDocumentFeatures : TypedPackageFeatureCollection<WordprocessingDocumentType, MainDocumentPart>,
            IApplicationTypeFeature,
            IMainPartFeature,
            IProgrammaticIdentifierFeature
        {
            public WordprocessingDocumentFeatures(OpenXmlPackage package)
                : base(package)
            {
            }

            ApplicationType IApplicationTypeFeature.Type => ApplicationType.Word;

            protected override MainDocumentPart CreateMainPart() => new();

            protected override string RelationshipType => MainDocumentPart.RelationshipTypeConstant;

            string IProgrammaticIdentifierFeature.ProgramId => "Word.Document";

            public override string? GetContentType(WordprocessingDocumentType type) => type switch
            {
                WordprocessingDocumentType.Document => "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml",
                WordprocessingDocumentType.Template => "application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml",
                WordprocessingDocumentType.MacroEnabledDocument => "application/vnd.ms-word.document.macroEnabled.main+xml",
                WordprocessingDocumentType.MacroEnabledTemplate => "application/vnd.ms-word.template.macroEnabledTemplate.main+xml",
                _ => default,
            };

            public override WordprocessingDocumentType? GetDocumentType(string contentPart) => contentPart switch
            {
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml" => WordprocessingDocumentType.Document,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.template.main+xml" => WordprocessingDocumentType.Template,
                "application/vnd.ms-word.document.macroEnabled.main+xml" => WordprocessingDocumentType.MacroEnabledDocument,
                "application/vnd.ms-word.template.macroEnabledTemplate.main+xml" => WordprocessingDocumentType.MacroEnabledTemplate,
                _ => default,
            };
        }
    }
}
