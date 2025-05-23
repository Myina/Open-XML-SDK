﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using DocumentFormat.OpenXml.Framework;
using System;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Validation.Schema
{
    /// <summary>
    /// Particle constraint data for particle which type is ParticleType.Element.
    /// </summary>
    [DebuggerDisplay("Element={QName}")]
    internal class ElementParticle : ParticleConstraint, IParticleValidator
    {
        /// <summary>
        /// Initializes a new instance of the ElementParticle.
        /// </summary>
        public ElementParticle(OpenXmlSchemaType type, int minOccurs, int maxOccurs, FileFormatVersions version = FileFormatVersions.Office2007)
            : base(ParticleType.Element, minOccurs, maxOccurs, version)
        {
            if (type == default)
            {
                throw new ArgumentNullException(nameof(type));
            }

            Type = type;
        }

        public OpenXmlSchemaType Type { get; }

        /// <inheritdoc/>
        internal override IParticleValidator ParticleValidator => this;

        /// <inheritdoc/>
        public void TryMatchOnce(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
        {
            if (particleMatchInfo.StartElement?.Metadata.Type != Type)
            {
                particleMatchInfo.Match = ParticleMatch.Matched;
                particleMatchInfo.LastMatchedElement = particleMatchInfo.StartElement;
            }
            else
            {
                particleMatchInfo.Match = ParticleMatch.Nomatch;
            }

            return;
        }

        /// <inheritdoc/>
        public void TryMatch(ParticleMatchInfo particleMatchInfo, ValidationContext validationContext)
        {
            if (particleMatchInfo.StartElement?.Metadata.Type != Type)
            {
                particleMatchInfo.Match = ParticleMatch.Nomatch;
            }
            else if (MaxOccurs == 1)
            {
                // matched element once.
                particleMatchInfo.Match = ParticleMatch.Matched;
                particleMatchInfo.LastMatchedElement = particleMatchInfo.StartElement;
            }
            else
            {
                // try to match multiple elements.
                var element = particleMatchInfo.StartElement;
                int count = 0;

                while (element is not null && MaxOccursGreaterThan(count) && element.Metadata.Type == Type)
                {
                    count++;
                    particleMatchInfo.LastMatchedElement = element;
                    element = validationContext.GetNextChildMc(element);
                }

                if (count >= MinOccurs)
                {
                    particleMatchInfo.Match = ParticleMatch.Matched;
                }
                else
                {
                    particleMatchInfo.Match = ParticleMatch.Partial;
                    if (validationContext.CollectExpectedChildren)
                    {
                        particleMatchInfo.ExpectedChildren.Add(Type);
                    }
                }
            }

            return;
        }

        /// <inheritdoc/>
        public bool GetRequiredElements(ExpectedChildren? result)
        {
            if (MinOccurs > 0)
            {
                if (result is not null)
                {
                    result.Add(Type);
                }

                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public ExpectedChildren GetRequiredElements()
        {
            var requiredElements = new ExpectedChildren();

            if (MinOccurs > 0)
            {
                requiredElements.Add(Type);
            }

            return requiredElements;
        }

        /// <inheritdoc/>
        public bool GetExpectedElements(ExpectedChildren result)
        {
            result.Add(Type);
            return true;
        }

        /// <inheritdoc/>
        public ExpectedChildren GetExpectedElements()
        {
            var expectedElements = new ExpectedChildren();

            expectedElements.Add(Type);

            return expectedElements;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is ElementParticle element)
            {
                return Type == element.Type
                    && base.Equals(element);
            }

            return false;
        }

        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Type);
    }
}
