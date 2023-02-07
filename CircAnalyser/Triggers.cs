using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircAnalyser
{
    namespace Triggers
    {
        class D : ComponentBase
        {
            public D()
            {
                AddPort("CP", "D", "Q", "'Q");
            }

            public override void Tick()
            {
                p["Q"]["value"] = p["D"];
                p["'Q"]["value"] = !p["D"];
                p["Q"].Forward();
                p["'Q"].Forward();
            }

            public override void Update()
            {
                
            }
            public override string ToString()
            {
                return p["Q"].ToString();
            }
        }

        class T : ComponentBase
        {
            public T()
            {
                AddPort("CP", "T", "Q", "'Q");
            }

            public override void Tick()
            {
                bool preQ = p["Q"];
                p["Q"]["logic"]  = !p["T"] && preQ || p["T"] && !preQ;
                p["'Q"]["logic"] = !p["Q"];
                p["Q"].Forward();
                p["'Q"].Forward();
            }

            public override void Update()
            {

            }
            public override string ToString()
            {
                return p["Q"].ToString();
            }
        }

        class JK : ComponentBase
        {
            public JK()
            {
                AddPort("CP", "J", "K", "Q", "'Q");
            }

            public override void Tick()
            {
                bool preQ = p["Q"];
                p["Q"]["logic"] = p["J"] && !preQ || !p["K"] && preQ;
                p["'Q"]["logic"] = !p["Q"];
                p["Q"].Forward();
                p["'Q"].Forward();
            }

            public override void Update()
            {

            }
            public override string ToString()
            {
                return p["Q"].ToString();
            }
        }
    }
}
