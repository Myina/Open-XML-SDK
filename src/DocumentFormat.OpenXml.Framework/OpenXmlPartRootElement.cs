﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DocumentFormat.OpenXml.Features;
using DocumentFormat.OpenXml.Framework;
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace DocumentFormat.OpenXml
{
    /// <summary>
    /// Represents a base class for all root elements.
    /// </summary>
    public abstract class OpenXmlPartRootElement : OpenXmlCompositeElement
    {
        private OpenXmlElementContext? _context;
        private bool? _standaloneDeclaration;

        /// <summary>
        /// Initializes a new instance of the OpenXmlPartRootElement class.
        /// </summary>
        protected OpenXmlPartRootElement()
        {
        }

        /// <summary>
        /// Initializes a new instance of the OpenXmlPartRootElement class using the supplied OpenXmlPart.
        /// </summary>
        /// <param name="openXmlPart">The OpenXmlPart class.</param>
        protected OpenXmlPartRootElement(OpenXmlPart openXmlPart)
        {
            if (openXmlPart is null)
            {
                throw new ArgumentNullException(nameof(openXmlPart));
            }

            LoadFromPart(openXmlPart);
        }

        /// <summary>
        /// Initializes a new instance of the OpenXmlPartRootElement class using the supplied outer XML.
        /// </summary>
        /// <param name="outerXml">The outer XML of the element.</param>
        protected OpenXmlPartRootElement(string outerXml)
            : base(outerXml)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OpenXmlPartRootElement class using the supplied list of child elements.
        /// </summary>
        /// <param name="childElements">All child elements.</param>
        protected OpenXmlPartRootElement(IEnumerable<OpenXmlElement> childElements)
            : base(childElements)
        {
        }

        /// <summary>
        /// Initializes a new instance of the OpenXmlPartRootElement class using the supplied array of child elements.
        /// </summary>
        /// <param name="childElements">All child elements</param>
        protected OpenXmlPartRootElement(params OpenXmlElement[] childElements)
            : base(childElements)
        {
        }

        /// <summary>
        /// Gets the OpenXmlElementContext.
        /// </summary>
        internal override OpenXmlElementContext RootElementContext => _context ??= new(Features.GetNamespaceResolver());

        /// <summary>
        /// Load the DOM tree from the Open XML part.
        /// </summary>
        /// <param name="openXmlPart">The part this root element to be loaded from.</param>
        /// <exception cref="InvalidDataException">Thrown when the part contains an incorrect root element.</exception>
        internal void LoadFromPart(OpenXmlPart openXmlPart)
        {
            if (openXmlPart is null)
            {
                throw new ArgumentNullException(nameof(openXmlPart));
            }

            // Accessed before stream as it may cause the stream to reload
            var strictRelationshipFound = openXmlPart.OpenXmlPackage.StrictRelationshipFound;

            using (Stream partStream = openXmlPart.GetStream(FileMode.Open))
            {
                LoadFromPart(openXmlPart, partStream, strictRelationshipFound);
            }
        }

        /// <summary>
        /// Load the DOM tree from the Open XML part.
        /// </summary>
        /// <param name="openXmlPart">The part this root element to be loaded from.</param>
        /// <param name="partStream">The stream of the part.</param>
        /// <param name="strictRelationshipFound">Whether a strict relationship was found.</param>
        /// <returns>
        /// Returns true when the part stream is loaded successfully into this root element.
        /// Returns false when the part stream does not contain any xml element.
        /// </returns>
        /// <exception cref="InvalidDataException">Thrown when the part stream contains an incorrect root element.</exception>
        internal bool LoadFromPart(OpenXmlPart openXmlPart, Stream partStream, bool strictRelationshipFound)
        {
            if (partStream.Length < 4)
            {
                // The XmlReader.Read() method requires at least four bytes from the data stream in order to begin parsing.
                return false;
            }

            var events = openXmlPart.Features.Get<IPartRootEventsFeature>();
            events?.OnChange(EventType.Reloading, openXmlPart);

            var context = RootElementContext;

            // set MaxCharactersInDocument to limit the part size on loading DOM.
            context.XmlReaderSettings.MaxCharactersInDocument = openXmlPart.MaxCharactersInPart;

#if NET35
            context.XmlReaderSettings.ProhibitDtd = true; // set true explicitly for security fix
#else
            context.XmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit; // set to prohibit explicitly for security fix
#endif

            using (var xmlReader = XmlConvertingReaderFactory.Create(partStream, Features.GetNamespaceResolver(), context.XmlReaderSettings, strictRelationshipFound))
            {
                context.MCSettings = openXmlPart.MCSettings;

                xmlReader.Read();

                if (xmlReader.NodeType == XmlNodeType.XmlDeclaration)
                {
                    var standaloneAttribute = xmlReader.GetAttribute("standalone");
                    if (standaloneAttribute is not null)
                    {
                        _standaloneDeclaration = standaloneAttribute.Equals("yes", StringComparison.OrdinalIgnoreCase);
                    }
                }

                if (!xmlReader.EOF)
                {
                    xmlReader.MoveToContent();
                }

                if (xmlReader.EOF
                    || xmlReader.NodeType != XmlNodeType.Element
                    || !xmlReader.IsStartElement())
                {
                    // the stream does NOT contains any xml element.
                    return false;
                }

                var resolver = Features.GetNamespaceResolver();
                var qname = new OpenXmlQualifiedName(xmlReader.NamespaceURI, xmlReader.LocalName);

                if (!resolver.IsKnown(qname.Namespace) || !QName.Equals(qname))
                {
                    var elementQName = new XmlQualifiedName(xmlReader.LocalName, xmlReader.NamespaceURI).ToString();
                    var msg = SR.Format(ExceptionMessages.Fmt_PartRootIsInvalid, elementQName, XmlQualifiedName.ToString());

                    throw new InvalidDataException(msg);
                }

                // remove all children and clear all attributes
                OuterXml = string.Empty;
                var mcContextPushed = PushMcContext(xmlReader);
                Load(xmlReader, context.LoadMode);
                if (mcContextPushed)
                {
                    PopMcContext();
                }
            }

            events?.OnChange(EventType.Reloaded, openXmlPart);

            return true;
        }

        /// <summary>
        /// Save the DOM into the OpenXML part.
        /// </summary>
        internal void SaveToPart(OpenXmlPart openXmlPart)
        {
            if (openXmlPart is null)
            {
                throw new ArgumentNullException(nameof(openXmlPart));
            }

            // If we're saving the existing root to the the part, we don't need to unload the root as they're already equal
            using var partStream = openXmlPart.GetStream(FileMode.Create, unloadRootOnChange: !ReferenceEquals(this, openXmlPart.PartRootElement));

            Save(partStream);
        }

        /// <summary>
        /// Saves the DOM tree to the specified stream.
        /// </summary>
        /// <param name="stream">
        /// The stream to which to save the XML.
        /// </param>
        public void Save(Stream stream)
        {
            var settings = new XmlWriterSettings
            {
                CloseOutput = true,

                // We use UTF8 with no BOM as some viewers that consume documents cannot handle the BOM
                Encoding = new UTF8Encoding(false),
            };

            var events = Features.Get<IPartRootEventsFeature>();
            events?.OnChange(EventType.Saving, OpenXmlPart);

            using (var xmlWriter = XmlWriter.Create(stream, settings))
            {
                if (_standaloneDeclaration is not null)
                {
                    xmlWriter.WriteStartDocument(_standaloneDeclaration.Value);
                }

                WriteTo(xmlWriter);

                // Do not call WriteEndDocument if this root element is not parsed.
                // In that case, the WriteTo() will just call WriteRaw() with the raw xml,
                // so no WriteStartElement() needs to be called. Since the XmlWriter will
                // still on document start state. Call WriteEndDocument() will cause exception.
                if (XmlParsed)
                {
                    xmlWriter.WriteEndDocument();
                }
            }

            events?.OnChange(EventType.Saved, OpenXmlPart);
        }

        /// <summary>
        /// Gets the part that is associated with the DOM tree.
        /// It returns null when the DOM tree is not associated with a part.
        /// </summary>
        public OpenXmlPart? OpenXmlPart { get; internal set; }

        /// <summary>
        /// Saves the data in the DOM tree back to the part. This method can
        /// be called multiple times and each time it is called, the stream
        /// will be flushed.
        /// </summary>
        /// <remarks>
        /// Call this method explicitly to save the changes in the DOM tree.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Thrown when the tree is not associated with a part.</exception>
        public void Save()
        {
            if (OpenXmlPart is null)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotSaveDomTreeWithoutAssociatedPart);
            }

            SaveToPart(OpenXmlPart);
        }

        /// <summary>
        /// Reloads the part content into an Open XML DOM tree. This method can
        /// be called multiple times and each time it is called, the tree will
        /// be reloaded and previous changes on the tree are abandoned.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when the tree is not associated with a part.</exception>
        public void Reload()
        {
            if (OpenXmlPart is null)
            {
                throw new InvalidOperationException(ExceptionMessages.CannotReloadDomTreeWithoutAssociatedPart);
            }

            LoadFromPart(OpenXmlPart);
        }

        /// <summary>
        /// Saves the current node to the specified XmlWriter.
        /// </summary>
        /// <param name="xmlWriter">
        /// The XmlWriter to which to save the current node.
        /// </param>
        public override void WriteTo(XmlWriter xmlWriter)
        {
            if (xmlWriter is null)
            {
                throw new ArgumentNullException(nameof(xmlWriter));
            }

            if (XmlParsed)
            {
                // check the namespace mapping defined in this node first. because till now xmlWriter don't know the mapping defined in the current node.
                var prefix = LookupNamespaceLocal(NamespaceUri);

                // if not defined in the current node, try the xmlWriter
                if (Parent is not null && prefix.IsNullOrEmpty())
                {
                    prefix = xmlWriter.LookupPrefix(NamespaceUri);
                }

                // if xmlWriter didn't find it, it means the node is constructed by user and is not in the tree yet
                // in this case, we use the predefined prefix
                if (prefix.IsNullOrEmpty())
                {
                    prefix = Features.GetNamespaceResolver().LookupPrefix(QName.Namespace.Uri);
                }

                xmlWriter.WriteStartElement(prefix, LocalName, NamespaceUri);

                // fix bug #225919, write out all namespace into to root
                WriteNamespaceAtributes(xmlWriter);
                WriteAttributesTo(xmlWriter);

                if (HasChildren || !string.IsNullOrEmpty(InnerText))
                {
                    WriteContentTo(xmlWriter);
                    xmlWriter.WriteFullEndElement();
                }
                else
                {
                    xmlWriter.WriteEndElement();
                }
            }
            else
            {
                xmlWriter.WriteRaw(RawOuterXml);
            }
        }

        private void WriteNamespaceAtributes(XmlWriter xmlWrite)
        {
            if (WriteAllNamespaceOnRoot)
            {
                var namespaces = new Dictionary<string, string>();

                foreach (OpenXmlElement element in Descendants())
                {
                    if (element.NamespaceDeclField is not null)
                    {
                        foreach (var item in element.NamespaceDeclField)
                        {
                            if (!namespaces.ContainsKey(item.Key))
                            {
                                namespaces.Add(item.Key, item.Value);
                            }
                        }
                    }
                }

                foreach (var namespacePair in namespaces)
                {
                    if (!namespacePair.Key.IsNullOrEmpty())
                    {
                        if (NamespaceDeclField is not null &&
                            string.IsNullOrEmpty(LookupPrefixLocal(namespacePair.Value)) &&
                            string.IsNullOrEmpty(LookupNamespaceLocal(namespacePair.Key)))
                        {
                            xmlWrite.WriteAttributeString(OpenXmlElementContext.XmlnsPrefix, namespacePair.Key, OpenXmlElementContext.XmlnsUri, namespacePair.Value);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the Save method will try write all namespace declaration on the root element.
        /// </summary>
        internal virtual bool WriteAllNamespaceOnRoot => true;
    }
}
