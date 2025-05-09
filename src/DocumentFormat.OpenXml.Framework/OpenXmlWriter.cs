﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if FEATURE_ASYNC_SAX_XML
using System.Threading.Tasks;
#endif

namespace DocumentFormat.OpenXml
{
    /// <summary>
    /// Defines the OpenXmlWriter.
    /// </summary>
    public abstract class OpenXmlWriter : IDisposable
    {
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the OpenXmlWriter.
        /// </summary>
        protected OpenXmlWriter()
        {
        }

        /// <summary>
        /// Create an OpenXmlWriter from the OpenXmlPart.
        /// </summary>
        /// <param name="openXmlPart">The OpenXmlPart to be written.</param>
        /// <returns>The created OpenXmlWriter instance.</returns>
        public static OpenXmlWriter Create(OpenXmlPart openXmlPart) => new OpenXmlPartWriter(openXmlPart);

        /// <summary>
        /// Create an OpenXmlWriter from the OpenXmlPart.
        /// </summary>
        /// <param name="openXmlPart">The OpenXmlPart to be written.</param>
        /// <param name="encoding">The encoding for the XML stream.</param>
        /// <returns>The created OpenXmlWriter instance.</returns>
        public static OpenXmlWriter Create(OpenXmlPart openXmlPart, Encoding encoding) => new OpenXmlPartWriter(openXmlPart, encoding);

        /// <summary>
        /// Create an OpenXmlWriter on a given stream.
        /// </summary>
        /// <param name="partStream">The target stream.</param>
        /// <returns>The created OpenXmlWriter instance.</returns>
        public static OpenXmlWriter Create(Stream partStream) => new OpenXmlPartWriter(partStream);

        /// <summary>
        /// Create an OpenXmlWriter on given stream
        /// </summary>
        /// <param name="partStream">The target stream.</param>
        /// <param name="encoding">The encoding for the XML stream.</param>
        /// <returns>The created OpenXmlWriter instance.</returns>
        public static OpenXmlWriter Create(Stream partStream, Encoding encoding) => new OpenXmlPartWriter(partStream, encoding);

        /// <summary>
        /// Writes the XML declaration with the version "1.0".
        /// </summary>
        public abstract void WriteStartDocument();

        /// <summary>
        /// Writes the XML declaration with the version "1.0" and the standalone attribute.
        /// </summary>
        /// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no". </param>
        public abstract void WriteStartDocument(bool standalone);

        /// <summary>
        /// Writes out a start element tag of the current element of the OpenXmlReader. And write all the attributes of the element.
        /// </summary>
        /// <param name="elementReader">The OpenXmlReader to read from. </param>
        public abstract void WriteStartElement(OpenXmlReader elementReader);

        /// <summary>
        /// Writes out a start element tag of the current element of the OpenXmlReader. And write the attributes in attributes.
        /// </summary>
        /// <param name="elementReader">The OpenXmlReader to read from. </param>
        /// <param name="attributes">The attributes to be written, can be null if no attributes.</param>
        public abstract void WriteStartElement(OpenXmlReader elementReader, IEnumerable<OpenXmlAttribute> attributes);

        /// <summary>
        /// Writes out a start element tag of the current element of the OpenXmlReader. And write the attributes in attributes.
        /// </summary>
        /// <param name="elementReader">The OpenXmlReader to read from. </param>
        /// <param name="attributes">The attributes to be written, can be null if no attributes.</param>
        /// <param name="namespaceDeclarations">The namespace declarations to be written, can be null if no namespace declarations.</param>
        public abstract void WriteStartElement(OpenXmlReader elementReader, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations);

        /// <summary>
        /// Writes out a start tag of the element and all the attributes of the element.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        public abstract void WriteStartElement(OpenXmlElement elementObject);

        /// <summary>
        /// Writes out a start tag of the element. And write the attributes in attributes. The attributes of the element will be omitted.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        /// <param name="attributes">The attributes to be written.</param>
        public abstract void WriteStartElement(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes);

        /// <summary>
        /// Writes out a start tag of the element. And write the attributes in attributes. The attributes of the element will be omitted.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        /// <param name="attributes">The attributes to be written.</param>
        /// <param name="namespaceDeclarations">The namespace declarations to be written, can be null if no namespace declarations.</param>
        public abstract void WriteStartElement(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations);

        /// <summary>
        /// Closes one element.
        /// </summary>
        public abstract void WriteEndElement();

        /// <summary>
        /// Write the OpenXmlElement to the writer.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        public abstract void WriteElement(OpenXmlElement elementObject);

        /// <summary>
        /// When overridden in a derived class, writes the given text content.
        /// </summary>
        /// <param name="text">The text to write. </param>
        public abstract void WriteString(string text);

        /// <summary>
        /// Close the writer.
        /// </summary>
        public abstract void Close();

#if FEATURE_ASYNC_SAX_XML
        /// <summary>
        /// Asynchronously writes the XML declaration with the version "1.0".
        /// </summary>
        public virtual Task WriteStartDocumentAsync()
        {
            WriteStartDocument();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously writes the XML declaration with the version "1.0" and the standalone attribute.
        /// </summary>
        /// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no". </param>
        public virtual Task WriteStartDocumentAsync(bool standalone)
        {
            WriteStartDocument(standalone);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously writes out a start tag of the element and all the attributes of the element.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        public virtual Task WriteStartElementAsync(OpenXmlElement elementObject)
        {
            WriteStartElement(elementObject);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously writes out a start tag of the element. And write the attributes in attributes. The attributes of the element will be omitted.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        /// <param name="attributes">The attributes to be written.</param>
        public virtual Task WriteStartElementAsync(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes)
        {
            WriteStartElement(elementObject, attributes);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously writes out a start tag of the element. And write the attributes in attributes. The attributes of the element will be omitted.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        /// <param name="attributes">The attributes to be written.</param>
        /// <param name="namespaceDeclarations">The namespace declarations to be written, can be null if no namespace declarations.</param>
        public virtual Task WriteStartElementAsync(OpenXmlElement elementObject, IEnumerable<OpenXmlAttribute> attributes, IEnumerable<KeyValuePair<string, string>> namespaceDeclarations)
        {
            WriteStartElement(elementObject, attributes, namespaceDeclarations);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously closes one element.
        /// </summary>
        public virtual Task WriteEndElementAsync()
        {
            WriteEndElement();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Asynchronously write the OpenXmlElement to the writer.
        /// </summary>
        /// <param name="elementObject">The OpenXmlElement object to be written.</param>
        public virtual Task WriteElementAsync(OpenXmlElement elementObject)
        {
            WriteElement(elementObject);

            return Task.CompletedTask;
        }

        /// <summary>
        /// When overridden in a derived class, asynchronously writes the given text content.
        /// </summary>
        /// <param name="text">The text to write. </param>
        public virtual Task WriteStringAsync(string text)
        {
            WriteString(text);

            return Task.CompletedTask;
        }
#endif

        /// <summary>
        /// Throw if object is disposed.
        /// </summary>
        protected virtual void ThrowIfObjectDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        /// <summary>
        /// Closes the reader, and releases all resources.
        /// </summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Close();
                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Closes the writer, and releases all resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
