// Copyright 2011 Chris Patterson, Dru Sellers
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Automatonymous.Graphing
{
#if NETSTANDARD
  using Newtonsoft.Json;
#endif
  using System;
  using System.Collections.Generic;
  using System.Linq;


#if NETSTANDARD
  [JsonObject(MemberSerialization.OptIn)]
#else
  [Serializable]
#endif
    public class StateMachineGraph
    {
        readonly Edge[] _edges;
        readonly Vertex[] _vertices;

        public StateMachineGraph(IEnumerable<Vertex> vertices, IEnumerable<Edge> edges)
        {
            _vertices = vertices.ToArray();
            _edges = edges.ToArray();
        }

        public IEnumerable<Vertex> Vertices => _vertices;

        public IEnumerable<Edge> Edges => _edges;
    }
}