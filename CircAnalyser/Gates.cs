using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircAnalyser
{
    namespace Gates
    {
        class AND : ComponentBase
        {
            public AND()
            {
                AddPort("IN1", "IN2", "OUT1");
            }

            public override void Update()
            {
                p["OUT1"]["value"] = p["IN1"] && p["IN2"];
            }

            public override string ToString()
            {
                return p["OUT1"].ToString();
            }
        }

        class NOT : ComponentBase
        {
            public NOT()
            {
                this.AddPort("IN1", "OUT1");
            }

            public override void Update()
            {
                p["OUT1"]["value"] = !p["IN1"];
                p["OUT1"].Forward();
            }

            public override string ToString()
            {
                return p["OUT1"].ToString();
            }
        }

        class OR : ComponentBase
        {
            public OR()
            {
                this.AddPort("IN1", "IN2", "OUT1");
            }

            public override void Update()
            {
                p["OUT1"]["value"] = p["IN1"] || p["IN2"];
                p["OUT1"].Forward();
            }

            public override string ToString()
            {
                return p["OUT1"].ToString();
            }
        }

        class NAND : ComponentBase
        {
            public NAND()
            {
                this.AddPort("IN1", "IN2", "OUT1");
            }

            public override void Update()
            {
                p["OUT1"]["value"] = !(p["IN1"] && p["IN2"]);
                p["OUT1"].Forward();
            }

            public override string ToString()
            {
                return p["OUT1"].ToString();
            }
        }
    }
    
}
