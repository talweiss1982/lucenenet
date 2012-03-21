/*
 *
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 *
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;
using Lucene.Net.Index;

namespace Lucene.Net.Analyzers.Payloads
{
    /// <summary>
    /// Assigns a payload to a token based on the <see cref="Token.Type()"/>
    /// </summary>
    public class NumericPayloadTokenFilter : TokenFilter
    {
        private String typeMatch;
        private Payload thePayload;

        private PayloadAttribute payloadAtt;
        private TypeAttribute typeAtt;

        public NumericPayloadTokenFilter(TokenStream input, float payload, String typeMatch)
            : base(input)
        {
            //Need to encode the payload
            thePayload = new Payload(PayloadHelper.EncodeFloat(payload));
            this.typeMatch = typeMatch;
            payloadAtt = AddAttribute<PayloadAttribute>();
            typeAtt = AddAttribute<TypeAttribute>();
        }

        public sealed override bool IncrementToken()
        {
            if (input.IncrementToken())
            {
                if (typeAtt.Type.Equals(typeMatch))
                    payloadAtt.Payload = thePayload;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
